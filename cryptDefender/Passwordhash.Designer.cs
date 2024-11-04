using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace test3
{
    partial class Passwordhash
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            Password = new Label();
            label2 = new Label();
            button1 = new Button();
            txtHashedPassword = new TextBox();
            txtPassword = new TextBox();
            lblPasswordStrength = new Label();
            SuspendLayout();
            // 
            // Password
            // 
            Password.AutoSize = true;
            Password.Location = new Point(12, 45);
            Password.Name = "Password";
            Password.Size = new Size(57, 15);
            Password.TabIndex = 0;
            Password.Text = "Password";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 83);
            label2.Name = "label2";
            label2.Size = new Size(34, 15);
            label2.TabIndex = 1;
            label2.Text = "Hash";
            // 
            // button1
            // 
            button1.Location = new Point(85, 123);
            button1.Name = "button1";
            button1.Size = new Size(240, 23);
            button1.TabIndex = 2;
            button1.Text = "Password to hash";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // txtHashedPassword
            // 
            txtHashedPassword.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtHashedPassword.Location = new Point(85, 83);
            txtHashedPassword.Name = "txtHashedPassword";
            txtHashedPassword.Size = new Size(419, 23);
            txtHashedPassword.TabIndex = 3;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Location = new Point(85, 42);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(419, 23);
            txtPassword.TabIndex = 4;
            // 
            // lblPasswordStrength
            // 
            lblPasswordStrength.AutoSize = true;
            lblPasswordStrength.Location = new Point(112, 15);
            lblPasswordStrength.Name = "lblPasswordStrength";
            lblPasswordStrength.Size = new Size(108, 15);
            lblPasswordStrength.TabIndex = 5;
            lblPasswordStrength.Text = "Password Strength:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(516, 255);
            Controls.Add(lblPasswordStrength);
            Controls.Add(txtPassword);
            Controls.Add(txtHashedPassword);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(Password);
            Name = "Form1";
            Text = "Password hasher";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Password;
        private Label label2;
        private Button button1;
        private TextBox txtHashedPassword;
        private TextBox txtPassword;
        private Label lblPasswordStrength;
    }
}
