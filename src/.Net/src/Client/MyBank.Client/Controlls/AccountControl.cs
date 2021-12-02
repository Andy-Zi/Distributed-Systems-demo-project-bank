using MyBank.Interfaces;
using System.Windows.Forms;
using Unity;

namespace MyBank.Client.Controlls
{
    public partial class AccountControl : UserControl
    {
        [Dependency] public ApplicationEnvironment ApplicationEnvironment { get; set; }
        public AccountControl()
        {
            InitializeComponent();
            InitializeAccountGrid();
        }

        public IAccount Account { get; protected set; }

        [InjectionConstructor]
        public AccountControl(IAccount account)
        {
            InitializeComponent();
            InitializeAccountGrid();
            Account = account;
            BindAccount();

        }

        void BindAccount()
        {
            this.textBox_accountNumber.Text = Account.AccountNumber;
            this.textBox_Owner.Text = Account.Owner;
            this.textBox_Description.Text = Account.Description;
            this.label_value_text.Text = $"{Account.Value} €";

            this.dataGridView_Transactions.DataSource = Account.Transactions;
            this.dataGridView_Transactions.Refresh();
        }

        protected void InitializeAccountGrid()
        {
            this.dataGridView_Transactions.AutoGenerateColumns = false;
            this.dataGridView_Transactions.RowTemplate.MinimumHeight = 40;
            this.dataGridView_Transactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            var dateColumn = new DataGridViewColumn()
            {
                HeaderText = "Date",
                Name = "Date",
                CellTemplate = new DataGridViewTextBoxCell(),
                Visible = true,
                Width = 130,
                DataPropertyName = "Date",
                ReadOnly = true

            };

            this.dataGridView_Transactions.Columns.Add(dateColumn);

            var senderColumn = new DataGridViewColumn()
            {
                HeaderText = "Sender",
                Name = "Sender",
                CellTemplate = new DataGridViewTextBoxCell(),
                Visible = true,
                Width = 270,
                DataPropertyName = "SenderAccount",
                ReadOnly = true

            };

            this.dataGridView_Transactions.Columns.Add(senderColumn);

            var recieverColumn = new DataGridViewColumn()
            {
                HeaderText = "Reciever",
                Name = "Reciever",
                CellTemplate = new DataGridViewTextBoxCell(),
                Visible = true,
                Width = 270,
                DataPropertyName = "RecieverAccount",
                ReadOnly = true

            };

            this.dataGridView_Transactions.Columns.Add(recieverColumn);

            var valueColumn = new DataGridViewColumn()
            {
                HeaderText = "Amount",
                Name = "Amount",
                CellTemplate = new DataGridViewTextBoxCell(),
                Visible = true,
                Width = 130,
                DataPropertyName = "Amount",
                ReadOnly = true

            };

            this.dataGridView_Transactions.Columns.Add(valueColumn);

            var commentColumn = new DataGridViewColumn()
            {
                HeaderText = "Comment",
                Name = "Comment",
                CellTemplate = new DataGridViewTextBoxCell(),
                Visible = true,
                DataPropertyName = "Comment",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            };

            this.dataGridView_Transactions.Columns.Add(commentColumn);
        }

        private void button_refresh_Click(object sender, System.EventArgs e)
        {
            var accounts = ApplicationEnvironment.ServiceConnector.Statement(ApplicationEnvironment.CurrentToken,Account.AccountNumber,true);
            if(accounts?.Count > 0)
            {
                Account = accounts[0];
                BindAccount();
                this.Refresh();
            }
        }
    }
}
