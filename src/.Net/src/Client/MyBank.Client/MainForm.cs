using MyBank.Client.Controlls;
using MyBank.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace MyBank.Client
{
    public partial class MainForm : Form
    {
        [Unity.Dependency] public ApplicationEnvironment ApplicationEnvironment { get; set; }
        [Unity.Dependency] public Unity.IUnityContainer UnityContainer { get; set; }

        AccountControl CurrentAccountControl { get; set; }
        public MainForm()
        {
            InitializeComponent();
            this.splitContainer.BorderStyle = BorderStyle.Fixed3D;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Subscribe();
        }

        public void Subscribe()
        {
            this.cardsPanel.CardPanelDoubleClicked += CardsPanel_CardPanelDoubleClicked;
        }

        private void CardsPanel_CardPanelDoubleClicked(object sender, CardsPanelClickedEventArgs e)
        {
            var accounts = ApplicationEnvironment.ServiceConnector.Statement(ApplicationEnvironment.CurrentToken,e.ViewModel.AccountNumber,true);
            if(accounts.Count > 0)
                ShowAccountControl(accounts[0]);
        }

        protected void ShowAccountControl(IAccount account)
        {
            this.splitContainer.Panel2.SuspendLayout();
            CurrentAccountControl?.Dispose();
            this.splitContainer.Panel2.Controls.Clear();
            CurrentAccountControl = UnityContainer.Resolve<AccountControl>(new ParameterOverride("account", account));
            this.splitContainer.Panel2.Controls.Add(CurrentAccountControl);
            CurrentAccountControl.Dock = DockStyle.Fill;
            this.splitContainer.Panel2.ResumeLayout();
        }

        protected override void OnShown(EventArgs e)
        {
            if(ApplicationEnvironment.CurrentToken.Last()-'0' == 0)
            {
                //User Mode remove Admin Page
                tabControl.TabPages.RemoveAt(1);
            }
            ApplicationEnvironment.ServiceConnector.RegisterMainForm(this);
            base.OnShown(e);
            this.CenterToScreen();
            this.Focus();
            GetAccounts();
        }

        protected void GetAccounts()
        {
            var accounts = ApplicationEnvironment.ServiceConnector.ListAccounts(ApplicationEnvironment.CurrentToken);
            ObservableCollection<CardViewModel> cards = new ObservableCollection<CardViewModel>();
            foreach(var (accountNumber,Description) in accounts)
            {
                cards.Add(new CardViewModel()
                {
                    AccountNumber = accountNumber,
                    Description = Description
                });
            }
            cardsPanel.Cards = cards;
            cardsPanel.DataBind();
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            GetAccounts();
            this.splitContainer.Panel2.SuspendLayout();
            CurrentAccountControl?.Dispose();
            this.splitContainer.Panel2.Controls.Clear();
            this.splitContainer.Panel2.ResumeLayout();
        }

        private void button_newTransaction_Click(object sender, EventArgs e)
        {
            var newTransactionForm = UnityContainer.Resolve<NewTransactionForm>();
            newTransactionForm.ShowDialog(this);
        }

        private void button_createUser_Click(object sender, EventArgs e)
        {
            var newTransactionForm = UnityContainer.Resolve<NewUserForm>();
            newTransactionForm.ShowDialog(this);
        }

        private void button_createAccount_Click(object sender, EventArgs e)
        {
            var newTransactionForm = UnityContainer.Resolve<NewAccountForm>();
            newTransactionForm.ShowDialog(this);
        }

        private void button_payInto_Click(object sender, EventArgs e)
        {
            var newTransactionForm = UnityContainer.Resolve<PayIntoForm>();
            newTransactionForm.ShowDialog(this);
        }
    }
}
