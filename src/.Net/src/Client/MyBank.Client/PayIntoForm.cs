using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBank.Client
{
    public partial class PayIntoForm : BaseActionForm
    {
        public PayIntoForm()
        {
            InitializeComponent();
            numericUpDown.Minimum = 0;
            numericUpDown.Maximum = int.MaxValue;
        }

        Dictionary<string, string> ownAccounts = new Dictionary<string, string>();

        protected override void OnShown(EventArgs e)
        {
            var accounts = ApplicationEnvironment.ServiceConnector.ListAccounts(ApplicationEnvironment.CurrentToken);
            foreach (var (accountNumber, Description) in accounts)
            {
                ownAccounts.Add(Description, accountNumber);
            }
            this.comboBox.Items.AddRange(ownAccounts.Keys.ToArray());
            this.comboBox.SelectedIndex = 0;
            base.OnShown(e);
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            var account = ownAccounts[this.comboBox.SelectedItem.ToString()];
            var amount = (float)numericUpDown.Value;

            ApplicationEnvironment.ServiceConnector.PayInto(ApplicationEnvironment.CurrentToken, account, amount);
            HandleClose();
        }
    }
}
