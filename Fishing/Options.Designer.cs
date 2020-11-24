namespace Fishing
{
    partial class Options
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
            this.sendmail = new System.Windows.Forms.CheckBox();
            this.mail = new System.Windows.Forms.TextBox();
            this.mailLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.myRegionLabel = new System.Windows.Forms.Label();
            this.myregion = new System.Windows.Forms.MaskedTextBox();
            this.pass = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // sendmail
            // 
            this.sendmail.AutoSize = true;
            this.sendmail.Location = new System.Drawing.Point(9, 32);
            this.sendmail.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.sendmail.Name = "sendmail";
            this.sendmail.Size = new System.Drawing.Size(164, 17);
            this.sendmail.TabIndex = 2;
            this.sendmail.Text = "Отправлять бэкап на e-mail";
            this.sendmail.UseVisualStyleBackColor = true;
            // 
            // mail
            // 
            this.mail.Enabled = false;
            this.mail.Location = new System.Drawing.Point(94, 54);
            this.mail.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mail.Name = "mail";
            this.mail.Size = new System.Drawing.Size(88, 20);
            this.mail.TabIndex = 3;
            // 
            // mailLabel
            // 
            this.mailLabel.AutoSize = true;
            this.mailLabel.Location = new System.Drawing.Point(29, 56);
            this.mailLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mailLabel.Name = "mailLabel";
            this.mailLabel.Size = new System.Drawing.Size(34, 13);
            this.mailLabel.TabIndex = 3;
            this.mailLabel.Text = "e-mail";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(29, 81);
            this.passwordLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(52, 13);
            this.passwordLabel.TabIndex = 4;
            this.passwordLabel.Text = "password";
            // 
            // myRegionLabel
            // 
            this.myRegionLabel.AutoSize = true;
            this.myRegionLabel.Location = new System.Drawing.Point(9, 11);
            this.myRegionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.myRegionLabel.Name = "myRegionLabel";
            this.myRegionLabel.Size = new System.Drawing.Size(66, 13);
            this.myRegionLabel.TabIndex = 6;
            this.myRegionLabel.Text = "Мой регион";
            // 
            // myregion
            // 
            this.myregion.Location = new System.Drawing.Point(94, 11);
            this.myregion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.myregion.Mask = "00";
            this.myregion.Name = "myregion";
            this.myregion.PromptChar = ' ';
            this.myregion.Size = new System.Drawing.Size(30, 20);
            this.myregion.TabIndex = 1;
            // 
            // pass
            // 
            this.pass.Enabled = false;
            this.pass.Location = new System.Drawing.Point(94, 79);
            this.pass.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pass.Name = "pass";
            this.pass.PasswordChar = '*';
            this.pass.PromptChar = ' ';
            this.pass.Size = new System.Drawing.Size(88, 20);
            this.pass.TabIndex = 4;
            // 
            // options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 124);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.myregion);
            this.Controls.Add(this.myRegionLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.mailLabel);
            this.Controls.Add(this.mail);
            this.Controls.Add(this.sendmail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "options";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox sendmail;
        private System.Windows.Forms.TextBox mail;
        private System.Windows.Forms.Label mailLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label myRegionLabel;
        private System.Windows.Forms.MaskedTextBox myregion;
        private System.Windows.Forms.MaskedTextBox pass;
    }
}