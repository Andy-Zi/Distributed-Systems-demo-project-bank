namespace MyBank.Client
{
    partial class LoginForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_ok = new System.Windows.Forms.Button();
            this.groupBox_Account = new System.Windows.Forms.GroupBox();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.groupBox_server = new System.Windows.Forms.GroupBox();
            this.label_port = new System.Windows.Forms.Label();
            this.label_type = new System.Windows.Forms.Label();
            this.label_address = new System.Windows.Forms.Label();
            this.numericUpDown_port = new System.Windows.Forms.NumericUpDown();
            this.comboBox_type = new System.Windows.Forms.ComboBox();
            this.textBox_address = new System.Windows.Forms.TextBox();
            this.label_password = new System.Windows.Forms.Label();
            this.label_username = new System.Windows.Forms.Label();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.groupBox_Account.SuspendLayout();
            this.groupBox_server.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_port)).BeginInit();
            this.SuspendLayout();
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(201, 283);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 0;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // groupBox_Account
            // 
            this.groupBox_Account.Controls.Add(this.textBox_password);
            this.groupBox_Account.Controls.Add(this.groupBox_server);
            this.groupBox_Account.Controls.Add(this.label_password);
            this.groupBox_Account.Controls.Add(this.label_username);
            this.groupBox_Account.Controls.Add(this.textBox_username);
            this.groupBox_Account.Location = new System.Drawing.Point(12, 12);
            this.groupBox_Account.Name = "groupBox_Account";
            this.groupBox_Account.Size = new System.Drawing.Size(260, 265);
            this.groupBox_Account.TabIndex = 1;
            this.groupBox_Account.TabStop = false;
            this.groupBox_Account.Text = "Account";
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(67, 60);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.PasswordChar = '*';
            this.textBox_password.Size = new System.Drawing.Size(184, 20);
            this.textBox_password.TabIndex = 5;
            // 
            // groupBox_server
            // 
            this.groupBox_server.Controls.Add(this.label_port);
            this.groupBox_server.Controls.Add(this.label_type);
            this.groupBox_server.Controls.Add(this.label_address);
            this.groupBox_server.Controls.Add(this.numericUpDown_port);
            this.groupBox_server.Controls.Add(this.comboBox_type);
            this.groupBox_server.Controls.Add(this.textBox_address);
            this.groupBox_server.Location = new System.Drawing.Point(9, 103);
            this.groupBox_server.Name = "groupBox_server";
            this.groupBox_server.Size = new System.Drawing.Size(245, 147);
            this.groupBox_server.TabIndex = 4;
            this.groupBox_server.TabStop = false;
            this.groupBox_server.Text = "Server";
            // 
            // label_port
            // 
            this.label_port.AutoSize = true;
            this.label_port.Location = new System.Drawing.Point(7, 112);
            this.label_port.Name = "label_port";
            this.label_port.Size = new System.Drawing.Size(26, 13);
            this.label_port.TabIndex = 5;
            this.label_port.Text = "Port";
            // 
            // label_type
            // 
            this.label_type.AutoSize = true;
            this.label_type.Location = new System.Drawing.Point(7, 73);
            this.label_type.Name = "label_type";
            this.label_type.Size = new System.Drawing.Size(31, 13);
            this.label_type.TabIndex = 4;
            this.label_type.Text = "Type";
            // 
            // label_address
            // 
            this.label_address.AutoSize = true;
            this.label_address.Location = new System.Drawing.Point(7, 34);
            this.label_address.Name = "label_address";
            this.label_address.Size = new System.Drawing.Size(45, 13);
            this.label_address.TabIndex = 3;
            this.label_address.Text = "Address";
            // 
            // numericUpDown_port
            // 
            this.numericUpDown_port.Location = new System.Drawing.Point(58, 110);
            this.numericUpDown_port.Name = "numericUpDown_port";
            this.numericUpDown_port.Size = new System.Drawing.Size(181, 20);
            this.numericUpDown_port.TabIndex = 2;
            // 
            // comboBox_type
            // 
            this.comboBox_type.FormattingEnabled = true;
            this.comboBox_type.Location = new System.Drawing.Point(58, 70);
            this.comboBox_type.Name = "comboBox_type";
            this.comboBox_type.Size = new System.Drawing.Size(181, 21);
            this.comboBox_type.TabIndex = 1;
            // 
            // textBox_address
            // 
            this.textBox_address.Location = new System.Drawing.Point(58, 31);
            this.textBox_address.Name = "textBox_address";
            this.textBox_address.Size = new System.Drawing.Size(181, 20);
            this.textBox_address.TabIndex = 0;
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Location = new System.Drawing.Point(6, 63);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(53, 13);
            this.label_password.TabIndex = 3;
            this.label_password.Text = "Password";
            // 
            // label_username
            // 
            this.label_username.AutoSize = true;
            this.label_username.Location = new System.Drawing.Point(6, 26);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(55, 13);
            this.label_username.TabIndex = 2;
            this.label_username.Text = "Username";
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(67, 23);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(184, 20);
            this.textBox_username.TabIndex = 0;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 311);
            this.Controls.Add(this.groupBox_Account);
            this.Controls.Add(this.button_ok);
            this.Name = "LoginForm";
            this.Text = "Bank Login";
            this.groupBox_Account.ResumeLayout(false);
            this.groupBox_Account.PerformLayout();
            this.groupBox_server.ResumeLayout(false);
            this.groupBox_server.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_port)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.GroupBox groupBox_Account;
        private System.Windows.Forms.Label label_username;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.GroupBox groupBox_server;
        private System.Windows.Forms.NumericUpDown numericUpDown_port;
        private System.Windows.Forms.ComboBox comboBox_type;
        private System.Windows.Forms.TextBox textBox_address;
        private System.Windows.Forms.Label label_port;
        private System.Windows.Forms.Label label_type;
        private System.Windows.Forms.Label label_address;
        private System.Windows.Forms.TextBox textBox_password;
    }
}