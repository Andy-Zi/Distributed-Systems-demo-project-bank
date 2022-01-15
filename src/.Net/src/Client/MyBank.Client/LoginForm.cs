using MyBank.Interfaces;
using MyBank.Nameservice;
using MyBank.Nameservice.Exceptions;
using System;
using System.Windows.Forms;
using Unity;

namespace MyBank.Client
{
    public partial class LoginForm : Form, IDisposable
    {
        [Dependency] public IUnityContainer UnityContainer { get; set; }
        [Dependency] public ApplicationEnvironment ApplicationEnvironment { get; set; }
        public LoginForm()
        {
            InitializeComponent();
            Subscribe();
            this.comboBox_type.DataSource = Enum.GetValues(typeof(ConnectionTypes));
            this.comboBox_type.SelectedIndex = 0;
            this.comboBox_type.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        protected void Subscribe()
        {
            this.comboBox_type.SelectedValueChanged += ComboBox_type_SelectedValueChanged;
        }
        protected void Unsubscribe()
        {
            this.comboBox_type.SelectedValueChanged -= ComboBox_type_SelectedValueChanged;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Unsubscribe();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.CenterToScreen();
            this.Focus();
        }


        private void ComboBox_type_SelectedValueChanged(object sender, EventArgs e)
        {
            this.numericUpDown_port.Enabled = true;
            this.textBox_address.Enabled = true;
            Enum.TryParse<ConnectionTypes>(this.comboBox_type.SelectedValue.ToString(), out var connectionType);
            if(connectionType == ConnectionTypes.COM)
            {
                this.numericUpDown_port.Enabled = false;
            }else if(connectionType == ConnectionTypes.WCF)
            {
                this.numericUpDown_port.Value = 8000;
            }else if(connectionType == ConnectionTypes.REST)
            {
                this.numericUpDown_port.Value = 8080;
            } 
        }

  

        private void button_ok_Click(object sender, EventArgs e)
        {
            this.groupBox_Account.Enabled = false;
            this.groupBox_server.Enabled = false;
            try
            {
                Enum.TryParse<ConnectionTypes>(this.comboBox_type.SelectedValue.ToString(), out var connectionType);
                if (!UnityContainer.IsRegistered<IServiceConnector>(connectionType.ToString()))
                    throw new NotImplementedException($"Client for {connectionType} isn't implemented yet!");

                var service = UnityContainer.Resolve<IServiceConnector>(connectionType.ToString());
                service.Connect(this.textBox_address.Text,(int)this.numericUpDown_port.Value);
                var token = service.Login(this.textBox_username.Text, this.textBox_password.Text);
                ApplicationEnvironment.CurrentConnectionType = connectionType;
                ApplicationEnvironment.CurrentToken = token;
                ApplicationEnvironment.ServiceConnector = new ErrorHandlingServiceConnector(service);
                this.Close();
            }
            catch(Exception ex)
            {
                if(ex is ServerNotReachableException serverNotReachableException)
                    MessageBox.Show(this, 
                        $"The Server is not Reachable!\nError Message:\n{serverNotReachableException.InnerException.Message}", "Error", MessageBoxButtons.OK);
                else
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
            finally
            {
                this.groupBox_Account.Enabled = true;
                this.groupBox_server.Enabled = true;
            }
        }
    }
}
