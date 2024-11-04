namespace WinFormsApp3
{
    partial class MainForm
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
            PortScannerButton = new Button();
            PasswordHashButton = new Button();
            FileEncryptionButton = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // PortScannerButton
            // 
            PortScannerButton.Location = new Point(129, 445);
            PortScannerButton.Margin = new Padding(9, 10, 9, 10);
            PortScannerButton.Name = "PortScannerButton";
            PortScannerButton.Size = new Size(580, 394);
            PortScannerButton.TabIndex = 0;
            PortScannerButton.Text = "Port scanner";
            PortScannerButton.UseVisualStyleBackColor = true;
            PortScannerButton.Click += button1_Click;
            // 
            // PasswordHashButton
            // 
            PasswordHashButton.Location = new Point(826, 445);
            PasswordHashButton.Margin = new Padding(9, 10, 9, 10);
            PasswordHashButton.Name = "PasswordHashButton";
            PasswordHashButton.Size = new Size(517, 394);
            PasswordHashButton.TabIndex = 1;
            PasswordHashButton.Text = "Password hasher";
            PasswordHashButton.UseVisualStyleBackColor = true;
            PasswordHashButton.Click += button2_Click;
            // 
            // FileEncryptionButton
            // 
            FileEncryptionButton.Location = new Point(1474, 445);
            FileEncryptionButton.Margin = new Padding(9, 10, 9, 10);
            FileEncryptionButton.Name = "FileEncryptionButton";
            FileEncryptionButton.Size = new Size(557, 394);
            FileEncryptionButton.TabIndex = 2;
            FileEncryptionButton.Text = "File encryption";
            FileEncryptionButton.UseVisualStyleBackColor = true;
            FileEncryptionButton.Click += FileEncryptionButton_Click;
            // 
            // button3
            // 
            button3.Location = new Point(2162, 445);
            button3.Name = "button3";
            button3.Size = new Size(482, 394);
            button3.TabIndex = 3;
            button3.Text = "keylogger";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(20F, 48F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2798, 1430);
            Controls.Add(button3);
            Controls.Add(FileEncryptionButton);
            Controls.Add(PasswordHashButton);
            Controls.Add(PortScannerButton);
            Margin = new Padding(9, 10, 9, 10);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
        }

        #endregion

        private Button PortScannerButton;
        private Button PasswordHashButton;
        private Button FileEncryptionButton;
        private Button button3;
    }
}