namespace WinFormsApp3
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;

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
            lblUsername = new Label();
            lblPassword = new Label();
            lblEmail = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            txtEmail = new TextBox();
            btnRegister = new Button();
            SuspendLayout();
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(21, 34);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(89, 15);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(30, 73);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(55, 15);
            lblPassword.TabIndex = 1;
            lblPassword.Text = "Password:";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(48, 112);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(37, 15);
            lblEmail.TabIndex = 5;
            lblEmail.Text = "Email:";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(116, 31);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(148, 23);
            txtUsername.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(116, 70);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(148, 23);
            txtPassword.TabIndex = 3;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(116, 109);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(148, 23);
            txtEmail.TabIndex = 6;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(116, 148);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(66, 22);
            btnRegister.TabIndex = 4;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(290, 193);
            Controls.Add(btnRegister);
            Controls.Add(txtEmail);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblEmail);
            Controls.Add(lblPassword);
            Controls.Add(lblUsername);
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Register";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnRegister;
    }
}
