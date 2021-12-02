using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyBank.Client
{
    public partial class NewUserForm : BaseActionForm
    {
        public NewUserForm()
        {
            InitializeComponent();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            ApplicationEnvironment.ServiceConnector.NewUser(ApplicationEnvironment.CurrentToken,textBox_username.Text,textBox_password.Text);
            HandleClose();
        }
    }
}
