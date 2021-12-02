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
    public partial class NewAccountForm : BaseActionForm
    {
        public NewAccountForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApplicationEnvironment.ServiceConnector.NewAccount(ApplicationEnvironment.CurrentToken, textBox_owner.Text, textBox_description.Text);
            HandleClose();
        }
    }
}
