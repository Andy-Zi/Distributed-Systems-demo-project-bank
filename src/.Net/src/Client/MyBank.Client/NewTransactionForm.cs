using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace MyBank.Client
{
    public partial class NewTransactionForm : BaseActionForm
    {

        public NewTransactionForm()
        {
            InitializeComponent();
            this.comboBox_from.DropDownStyle = ComboBoxStyle.DropDownList;
            numericUpDown_amount.Minimum = 0;
            numericUpDown_amount.Maximum = int.MaxValue;
        }
        Dictionary<string,string> ownAccounts = new Dictionary<string, string>();

        protected override void OnShown(EventArgs e)
        {
            var accounts = ApplicationEnvironment.ServiceConnector.ListAccounts(ApplicationEnvironment.CurrentToken);
            foreach (var (accountNumber, Description) in accounts)
            {
                ownAccounts.Add(Description, accountNumber);
            }
            this.comboBox_from.Items.AddRange(ownAccounts.Keys.ToArray());
            if(this.comboBox_from.Items.Count > 0)
                this.comboBox_from.SelectedIndex = 0;
            this.CenterToScreen();
            base.OnShown(e);
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            var fromAccount = string.Empty;
            if (this.comboBox_from.SelectedItem != null)
            {
                fromAccount = ownAccounts[this.comboBox_from.SelectedItem.ToString()];
            }
            var toAccount = textBox_to.Text;
            var amount = (float)numericUpDown_amount.Value;
            var comment = richTextBox_Comment.Text;
            var recieverName = textBox_recieverName.Text;
            ApplicationEnvironment.ServiceConnector.Transfere(ApplicationEnvironment.CurrentToken, fromAccount, toAccount, recieverName, amount, comment);
            HandleClose();
        }
    }
}
