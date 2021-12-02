namespace MyBank.Client.Controlls
{
    partial class AccountControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox_Account = new System.Windows.Forms.GroupBox();
            this.label_value_text = new System.Windows.Forms.Label();
            this.label_Value = new System.Windows.Forms.Label();
            this.textBox_Owner = new System.Windows.Forms.TextBox();
            this.textBox_accountNumber = new System.Windows.Forms.TextBox();
            this.textBox_Description = new System.Windows.Forms.TextBox();
            this.label_Owner = new System.Windows.Forms.Label();
            this.label_accountNumber = new System.Windows.Forms.Label();
            this.label_description = new System.Windows.Forms.Label();
            this.dataGridView_Transactions = new System.Windows.Forms.DataGridView();
            this.groupBox_Transactions = new System.Windows.Forms.GroupBox();
            this.button_refresh = new System.Windows.Forms.Button();
            this.groupBox_Account.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Transactions)).BeginInit();
            this.groupBox_Transactions.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_Account
            // 
            this.groupBox_Account.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Account.Controls.Add(this.button_refresh);
            this.groupBox_Account.Controls.Add(this.label_value_text);
            this.groupBox_Account.Controls.Add(this.label_Value);
            this.groupBox_Account.Controls.Add(this.textBox_Owner);
            this.groupBox_Account.Controls.Add(this.textBox_accountNumber);
            this.groupBox_Account.Controls.Add(this.textBox_Description);
            this.groupBox_Account.Controls.Add(this.label_Owner);
            this.groupBox_Account.Controls.Add(this.label_accountNumber);
            this.groupBox_Account.Controls.Add(this.label_description);
            this.groupBox_Account.Location = new System.Drawing.Point(3, 3);
            this.groupBox_Account.Name = "groupBox_Account";
            this.groupBox_Account.Size = new System.Drawing.Size(706, 130);
            this.groupBox_Account.TabIndex = 0;
            this.groupBox_Account.TabStop = false;
            this.groupBox_Account.Text = "Account Info";
            // 
            // label_value_text
            // 
            this.label_value_text.AutoSize = true;
            this.label_value_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label_value_text.Location = new System.Drawing.Point(560, 29);
            this.label_value_text.Name = "label_value_text";
            this.label_value_text.Size = new System.Drawing.Size(30, 22);
            this.label_value_text.TabIndex = 7;
            this.label_value_text.Text = "0$";
            // 
            // label_Value
            // 
            this.label_Value.AutoSize = true;
            this.label_Value.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Value.Location = new System.Drawing.Point(510, 30);
            this.label_Value.Name = "label_Value";
            this.label_Value.Size = new System.Drawing.Size(44, 18);
            this.label_Value.TabIndex = 6;
            this.label_Value.Text = "Value";
            // 
            // textBox_Owner
            // 
            this.textBox_Owner.Location = new System.Drawing.Point(126, 81);
            this.textBox_Owner.Name = "textBox_Owner";
            this.textBox_Owner.ReadOnly = true;
            this.textBox_Owner.Size = new System.Drawing.Size(293, 20);
            this.textBox_Owner.TabIndex = 5;
            // 
            // textBox_accountNumber
            // 
            this.textBox_accountNumber.Location = new System.Drawing.Point(126, 53);
            this.textBox_accountNumber.Name = "textBox_accountNumber";
            this.textBox_accountNumber.ReadOnly = true;
            this.textBox_accountNumber.Size = new System.Drawing.Size(293, 20);
            this.textBox_accountNumber.TabIndex = 4;
            // 
            // textBox_Description
            // 
            this.textBox_Description.Location = new System.Drawing.Point(126, 26);
            this.textBox_Description.Name = "textBox_Description";
            this.textBox_Description.ReadOnly = true;
            this.textBox_Description.Size = new System.Drawing.Size(293, 20);
            this.textBox_Description.TabIndex = 3;
            // 
            // label_Owner
            // 
            this.label_Owner.AutoSize = true;
            this.label_Owner.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Owner.Location = new System.Drawing.Point(6, 80);
            this.label_Owner.Name = "label_Owner";
            this.label_Owner.Size = new System.Drawing.Size(56, 18);
            this.label_Owner.TabIndex = 2;
            this.label_Owner.Text = "Owner:";
            // 
            // label_accountNumber
            // 
            this.label_accountNumber.AutoSize = true;
            this.label_accountNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_accountNumber.Location = new System.Drawing.Point(6, 52);
            this.label_accountNumber.Name = "label_accountNumber";
            this.label_accountNumber.Size = new System.Drawing.Size(116, 18);
            this.label_accountNumber.TabIndex = 1;
            this.label_accountNumber.Text = "Accountnumber:";
            // 
            // label_description
            // 
            this.label_description.AutoSize = true;
            this.label_description.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_description.Location = new System.Drawing.Point(6, 25);
            this.label_description.Name = "label_description";
            this.label_description.Size = new System.Drawing.Size(87, 18);
            this.label_description.TabIndex = 0;
            this.label_description.Text = "Description:";
            // 
            // dataGridView_Transactions
            // 
            this.dataGridView_Transactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Transactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Transactions.Location = new System.Drawing.Point(3, 16);
            this.dataGridView_Transactions.Name = "dataGridView_Transactions";
            this.dataGridView_Transactions.Size = new System.Drawing.Size(703, 352);
            this.dataGridView_Transactions.TabIndex = 1;
            // 
            // groupBox_Transactions
            // 
            this.groupBox_Transactions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Transactions.Controls.Add(this.dataGridView_Transactions);
            this.groupBox_Transactions.Location = new System.Drawing.Point(3, 133);
            this.groupBox_Transactions.Name = "groupBox_Transactions";
            this.groupBox_Transactions.Size = new System.Drawing.Size(709, 371);
            this.groupBox_Transactions.TabIndex = 2;
            this.groupBox_Transactions.TabStop = false;
            this.groupBox_Transactions.Text = "Transaction History";
            // 
            // button_refresh
            // 
            this.button_refresh.BackgroundImage = global::MyBank.Client.Resource.refresh;
            this.button_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_refresh.Location = new System.Drawing.Point(549, 80);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(41, 34);
            this.button_refresh.TabIndex = 8;
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // AccountControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_Transactions);
            this.Controls.Add(this.groupBox_Account);
            this.Name = "AccountControl";
            this.Size = new System.Drawing.Size(715, 507);
            this.groupBox_Account.ResumeLayout(false);
            this.groupBox_Account.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Transactions)).EndInit();
            this.groupBox_Transactions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_Account;
        private System.Windows.Forms.DataGridView dataGridView_Transactions;
        private System.Windows.Forms.Label label_description;
        private System.Windows.Forms.Label label_Owner;
        private System.Windows.Forms.Label label_accountNumber;
        private System.Windows.Forms.TextBox textBox_Owner;
        private System.Windows.Forms.TextBox textBox_accountNumber;
        private System.Windows.Forms.TextBox textBox_Description;
        private System.Windows.Forms.Label label_value_text;
        private System.Windows.Forms.Label label_Value;
        private System.Windows.Forms.GroupBox groupBox_Transactions;
        private System.Windows.Forms.Button button_refresh;
    }
}
