namespace MyBank.Client
{
    partial class NewTransactionForm
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
            this.label_from = new System.Windows.Forms.Label();
            this.label_to = new System.Windows.Forms.Label();
            this.label_amount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox_Comment = new System.Windows.Forms.RichTextBox();
            this.numericUpDown_amount = new System.Windows.Forms.NumericUpDown();
            this.textBox_to = new System.Windows.Forms.TextBox();
            this.comboBox_from = new System.Windows.Forms.ComboBox();
            this.textBox_recieverName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_amount)).BeginInit();
            this.SuspendLayout();
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(386, 289);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 0;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // label_from
            // 
            this.label_from.AutoSize = true;
            this.label_from.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_from.Location = new System.Drawing.Point(12, 9);
            this.label_from.Name = "label_from";
            this.label_from.Size = new System.Drawing.Size(41, 16);
            this.label_from.TabIndex = 1;
            this.label_from.Text = "From:";
            // 
            // label_to
            // 
            this.label_to.AutoSize = true;
            this.label_to.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_to.Location = new System.Drawing.Point(12, 40);
            this.label_to.Name = "label_to";
            this.label_to.Size = new System.Drawing.Size(27, 16);
            this.label_to.TabIndex = 2;
            this.label_to.Text = "To:";
            // 
            // label_amount
            // 
            this.label_amount.AutoSize = true;
            this.label_amount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_amount.Location = new System.Drawing.Point(12, 96);
            this.label_amount.Name = "label_amount";
            this.label_amount.Size = new System.Drawing.Size(55, 16);
            this.label_amount.TabIndex = 3;
            this.label_amount.Text = "Amount:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Comment:";
            // 
            // richTextBox_Comment
            // 
            this.richTextBox_Comment.Location = new System.Drawing.Point(86, 122);
            this.richTextBox_Comment.Name = "richTextBox_Comment";
            this.richTextBox_Comment.Size = new System.Drawing.Size(375, 165);
            this.richTextBox_Comment.TabIndex = 5;
            this.richTextBox_Comment.Text = "";
            // 
            // numericUpDown_amount
            // 
            this.numericUpDown_amount.Location = new System.Drawing.Point(86, 96);
            this.numericUpDown_amount.Name = "numericUpDown_amount";
            this.numericUpDown_amount.Size = new System.Drawing.Size(375, 20);
            this.numericUpDown_amount.TabIndex = 6;
            // 
            // textBox_to
            // 
            this.textBox_to.Location = new System.Drawing.Point(86, 40);
            this.textBox_to.Name = "textBox_to";
            this.textBox_to.Size = new System.Drawing.Size(375, 20);
            this.textBox_to.TabIndex = 7;
            // 
            // comboBox_from
            // 
            this.comboBox_from.FormattingEnabled = true;
            this.comboBox_from.Location = new System.Drawing.Point(86, 9);
            this.comboBox_from.Name = "comboBox_from";
            this.comboBox_from.Size = new System.Drawing.Size(375, 21);
            this.comboBox_from.TabIndex = 8;
            // 
            // textBox_recieverName
            // 
            this.textBox_recieverName.Location = new System.Drawing.Point(120, 69);
            this.textBox_recieverName.Name = "textBox_recieverName";
            this.textBox_recieverName.Size = new System.Drawing.Size(341, 20);
            this.textBox_recieverName.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "RecieverName:";
            // 
            // NewTransactionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 324);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_recieverName);
            this.Controls.Add(this.comboBox_from);
            this.Controls.Add(this.textBox_to);
            this.Controls.Add(this.numericUpDown_amount);
            this.Controls.Add(this.richTextBox_Comment);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_amount);
            this.Controls.Add(this.label_to);
            this.Controls.Add(this.label_from);
            this.Controls.Add(this.button_ok);
            this.Name = "NewTransactionForm";
            this.Text = "New Transaction";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_amount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Label label_from;
        private System.Windows.Forms.Label label_to;
        private System.Windows.Forms.Label label_amount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox_Comment;
        private System.Windows.Forms.NumericUpDown numericUpDown_amount;
        private System.Windows.Forms.TextBox textBox_to;
        private System.Windows.Forms.ComboBox comboBox_from;
        private System.Windows.Forms.TextBox textBox_recieverName;
        private System.Windows.Forms.Label label2;
    }
}