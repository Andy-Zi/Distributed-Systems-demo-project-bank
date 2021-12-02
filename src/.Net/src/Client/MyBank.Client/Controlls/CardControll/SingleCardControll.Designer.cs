namespace MyBank.Client.Controlls
{
    partial class SingleCardControll
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
            this.label_description = new System.Windows.Forms.Label();
            this.label_accountNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_description
            // 
            this.label_description.AutoSize = true;
            this.label_description.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label_description.Location = new System.Drawing.Point(1, 13);
            this.label_description.Name = "label_description";
            this.label_description.Size = new System.Drawing.Size(161, 25);
            this.label_description.TabIndex = 0;
            this.label_description.Text = "label_Description";
            // 
            // label_accountNumber
            // 
            this.label_accountNumber.AutoSize = true;
            this.label_accountNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label_accountNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_accountNumber.Location = new System.Drawing.Point(3, 38);
            this.label_accountNumber.Name = "label_accountNumber";
            this.label_accountNumber.Size = new System.Drawing.Size(153, 18);
            this.label_accountNumber.TabIndex = 1;
            this.label_accountNumber.Text = "label_AccountNumber";
            // 
            // SingleCardControll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_accountNumber);
            this.Controls.Add(this.label_description);
            this.Name = "SingleCardControll";
            this.Size = new System.Drawing.Size(174, 70);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_description;
        private System.Windows.Forms.Label label_accountNumber;
    }
}
