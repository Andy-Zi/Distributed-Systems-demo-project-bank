namespace MyBank.Client
{
    partial class MainForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_user = new System.Windows.Forms.TabPage();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer_left = new System.Windows.Forms.SplitContainer();
            this.button_newTransaction = new System.Windows.Forms.Button();
            this.tabPage_admin = new System.Windows.Forms.TabPage();
            this.groupBox_admin = new System.Windows.Forms.GroupBox();
            this.button_createUser = new System.Windows.Forms.Button();
            this.button_createAccount = new System.Windows.Forms.Button();
            this.button_payInto = new System.Windows.Forms.Button();
            this.button_refresh = new System.Windows.Forms.Button();
            this.cardsPanel = new MyBank.Client.Controlls.CardsPanel();
            this.tabControl.SuspendLayout();
            this.tabPage_user.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_left)).BeginInit();
            this.splitContainer_left.Panel1.SuspendLayout();
            this.splitContainer_left.Panel2.SuspendLayout();
            this.splitContainer_left.SuspendLayout();
            this.tabPage_admin.SuspendLayout();
            this.groupBox_admin.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_user);
            this.tabControl.Controls.Add(this.tabPage_admin);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1584, 761);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage_user
            // 
            this.tabPage_user.Controls.Add(this.splitContainer);
            this.tabPage_user.Location = new System.Drawing.Point(4, 25);
            this.tabPage_user.Name = "tabPage_user";
            this.tabPage_user.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_user.Size = new System.Drawing.Size(1576, 732);
            this.tabPage_user.TabIndex = 0;
            this.tabPage_user.Text = "Banking";
            this.tabPage_user.UseVisualStyleBackColor = true;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(3, 3);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.splitContainer_left);
            this.splitContainer.Size = new System.Drawing.Size(1570, 726);
            this.splitContainer.SplitterDistance = 318;
            this.splitContainer.TabIndex = 1;
            // 
            // splitContainer_left
            // 
            this.splitContainer_left.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer_left.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_left.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_left.Name = "splitContainer_left";
            this.splitContainer_left.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_left.Panel1
            // 
            this.splitContainer_left.Panel1.Controls.Add(this.cardsPanel);
            // 
            // splitContainer_left.Panel2
            // 
            this.splitContainer_left.Panel2.Controls.Add(this.button_newTransaction);
            this.splitContainer_left.Panel2.Controls.Add(this.button_refresh);
            this.splitContainer_left.Size = new System.Drawing.Size(318, 726);
            this.splitContainer_left.SplitterDistance = 664;
            this.splitContainer_left.TabIndex = 0;
            // 
            // button_newTransaction
            // 
            this.button_newTransaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_newTransaction.Location = new System.Drawing.Point(58, 5);
            this.button_newTransaction.Name = "button_newTransaction";
            this.button_newTransaction.Size = new System.Drawing.Size(253, 45);
            this.button_newTransaction.TabIndex = 1;
            this.button_newTransaction.Text = "New Transaction";
            this.button_newTransaction.UseVisualStyleBackColor = true;
            this.button_newTransaction.Click += new System.EventHandler(this.button_newTransaction_Click);
            // 
            // tabPage_admin
            // 
            this.tabPage_admin.Controls.Add(this.groupBox_admin);
            this.tabPage_admin.Location = new System.Drawing.Point(4, 25);
            this.tabPage_admin.Name = "tabPage_admin";
            this.tabPage_admin.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_admin.Size = new System.Drawing.Size(1576, 732);
            this.tabPage_admin.TabIndex = 1;
            this.tabPage_admin.Text = "Admin";
            this.tabPage_admin.UseVisualStyleBackColor = true;
            // 
            // groupBox_admin
            // 
            this.groupBox_admin.Controls.Add(this.button_payInto);
            this.groupBox_admin.Controls.Add(this.button_createAccount);
            this.groupBox_admin.Controls.Add(this.button_createUser);
            this.groupBox_admin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_admin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_admin.Location = new System.Drawing.Point(3, 3);
            this.groupBox_admin.Name = "groupBox_admin";
            this.groupBox_admin.Size = new System.Drawing.Size(1570, 726);
            this.groupBox_admin.TabIndex = 0;
            this.groupBox_admin.TabStop = false;
            this.groupBox_admin.Text = "Admin specific actions";
            // 
            // button_createUser
            // 
            this.button_createUser.Location = new System.Drawing.Point(6, 37);
            this.button_createUser.Name = "button_createUser";
            this.button_createUser.Size = new System.Drawing.Size(196, 54);
            this.button_createUser.TabIndex = 0;
            this.button_createUser.Text = "Create User";
            this.button_createUser.UseVisualStyleBackColor = true;
            this.button_createUser.Click += new System.EventHandler(this.button_createUser_Click);
            // 
            // button_createAccount
            // 
            this.button_createAccount.Location = new System.Drawing.Point(6, 97);
            this.button_createAccount.Name = "button_createAccount";
            this.button_createAccount.Size = new System.Drawing.Size(196, 54);
            this.button_createAccount.TabIndex = 1;
            this.button_createAccount.Text = "Create Account";
            this.button_createAccount.UseVisualStyleBackColor = true;
            this.button_createAccount.Click += new System.EventHandler(this.button_createAccount_Click);
            // 
            // button_payInto
            // 
            this.button_payInto.Location = new System.Drawing.Point(6, 157);
            this.button_payInto.Name = "button_payInto";
            this.button_payInto.Size = new System.Drawing.Size(196, 54);
            this.button_payInto.TabIndex = 2;
            this.button_payInto.Text = "Pay Into";
            this.button_payInto.UseVisualStyleBackColor = true;
            this.button_payInto.Click += new System.EventHandler(this.button_payInto_Click);
            // 
            // button_refresh
            // 
            this.button_refresh.BackgroundImage = global::MyBank.Client.Resource.refresh;
            this.button_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_refresh.Location = new System.Drawing.Point(3, 3);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(49, 47);
            this.button_refresh.TabIndex = 0;
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // cardsPanel
            // 
            this.cardsPanel.AutoScroll = true;
            this.cardsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardsPanel.Location = new System.Drawing.Point(0, 0);
            this.cardsPanel.Name = "cardsPanel";
            this.cardsPanel.Size = new System.Drawing.Size(314, 660);
            this.cardsPanel.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 761);
            this.Controls.Add(this.tabControl);
            this.Name = "MainForm";
            this.Text = "MyBank Client";
            this.tabControl.ResumeLayout(false);
            this.tabPage_user.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.splitContainer_left.Panel1.ResumeLayout(false);
            this.splitContainer_left.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_left)).EndInit();
            this.splitContainer_left.ResumeLayout(false);
            this.tabPage_admin.ResumeLayout(false);
            this.groupBox_admin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_user;
        private System.Windows.Forms.TabPage tabPage_admin;
        private Controlls.CardsPanel cardsPanel;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.SplitContainer splitContainer_left;
        private System.Windows.Forms.Button button_refresh;
        private System.Windows.Forms.Button button_newTransaction;
        private System.Windows.Forms.GroupBox groupBox_admin;
        private System.Windows.Forms.Button button_payInto;
        private System.Windows.Forms.Button button_createAccount;
        private System.Windows.Forms.Button button_createUser;
    }
}

