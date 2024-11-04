namespace cryptDefender
{
    partial class FileCryption
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
            buttonEncryptFile = new Button();
            buttonDecryptFile = new Button();
            label1 = new Label();
            buttonGetPrivateKey = new Button();
            buttonImportPublicKey = new Button();
            buttonCreateAsmKeys = new Button();
            buttonExportPublicKey = new Button();
            _encryptOpenFileDialog = new OpenFileDialog();
            _decryptOpenFileDialog = new OpenFileDialog();
            EncrypttextBox = new TextBox();
            DecrypttextBox = new TextBox();
            radioButtonTriDES = new RadioButton();
            radioButtonAES = new RadioButton();
            label2 = new Label();
            SuspendLayout();
            // 
            // buttonEncryptFile
            // 
            buttonEncryptFile.Location = new Point(891, 164);
            buttonEncryptFile.Name = "buttonEncryptFile";
            buttonEncryptFile.Size = new Size(138, 55);
            buttonEncryptFile.TabIndex = 0;
            buttonEncryptFile.Text = "Encrypt File";
            buttonEncryptFile.UseVisualStyleBackColor = true;
            buttonEncryptFile.Click += buttonEncryptFile_Click;
            // 
            // buttonDecryptFile
            // 
            buttonDecryptFile.Location = new Point(891, 262);
            buttonDecryptFile.Name = "buttonDecryptFile";
            buttonDecryptFile.Size = new Size(138, 55);
            buttonDecryptFile.TabIndex = 1;
            buttonDecryptFile.Text = "Decrypt File";
            buttonDecryptFile.UseVisualStyleBackColor = true;
            buttonDecryptFile.Click += buttonDecryptFile_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(65, 356);
            label1.Name = "label1";
            label1.Size = new Size(100, 25);
            label1.TabIndex = 2;
            label1.Text = "Key not set";
            // 
            // buttonGetPrivateKey
            // 
            buttonGetPrivateKey.Location = new Point(337, 356);
            buttonGetPrivateKey.Name = "buttonGetPrivateKey";
            buttonGetPrivateKey.Size = new Size(185, 50);
            buttonGetPrivateKey.TabIndex = 3;
            buttonGetPrivateKey.Text = "Get Private Key";
            buttonGetPrivateKey.UseVisualStyleBackColor = true;
            buttonGetPrivateKey.Click += buttonGetPrivateKey_Click;
            // 
            // buttonImportPublicKey
            // 
            buttonImportPublicKey.Location = new Point(581, 356);
            buttonImportPublicKey.Name = "buttonImportPublicKey";
            buttonImportPublicKey.Size = new Size(185, 50);
            buttonImportPublicKey.TabIndex = 4;
            buttonImportPublicKey.Text = "Import Public Key";
            buttonImportPublicKey.UseVisualStyleBackColor = true;
            buttonImportPublicKey.Click += buttonImportPublicKey_Click;
            // 
            // buttonCreateAsmKeys
            // 
            buttonCreateAsmKeys.Location = new Point(337, 445);
            buttonCreateAsmKeys.Name = "buttonCreateAsmKeys";
            buttonCreateAsmKeys.Size = new Size(185, 50);
            buttonCreateAsmKeys.TabIndex = 5;
            buttonCreateAsmKeys.Text = "Create Keys";
            buttonCreateAsmKeys.UseVisualStyleBackColor = true;
            buttonCreateAsmKeys.Click += buttonCreateAsmKeys_Click;
            // 
            // buttonExportPublicKey
            // 
            buttonExportPublicKey.Location = new Point(581, 445);
            buttonExportPublicKey.Name = "buttonExportPublicKey";
            buttonExportPublicKey.Size = new Size(185, 50);
            buttonExportPublicKey.TabIndex = 6;
            buttonExportPublicKey.Text = "Export Public Key";
            buttonExportPublicKey.UseVisualStyleBackColor = true;
            buttonExportPublicKey.Click += buttonExportPublicKey_Click;
            // 
            // _encryptOpenFileDialog
            // 
            _encryptOpenFileDialog.FileName = "openFileDialog1";
            // 
            // _decryptOpenFileDialog
            // 
            _decryptOpenFileDialog.FileName = "openFileDialog1";
            // 
            // EncrypttextBox
            // 
            EncrypttextBox.Location = new Point(65, 176);
            EncrypttextBox.Name = "EncrypttextBox";
            EncrypttextBox.Size = new Size(774, 31);
            EncrypttextBox.TabIndex = 7;
            // 
            // DecrypttextBox
            // 
            DecrypttextBox.Location = new Point(65, 274);
            DecrypttextBox.Name = "DecrypttextBox";
            DecrypttextBox.Size = new Size(774, 31);
            DecrypttextBox.TabIndex = 8;
            // 
            // radioButtonTriDES
            // 
            radioButtonTriDES.AutoSize = true;
            radioButtonTriDES.Location = new Point(891, 100);
            radioButtonTriDES.Name = "radioButtonTriDES";
            radioButtonTriDES.Size = new Size(115, 29);
            radioButtonTriDES.TabIndex = 9;
            radioButtonTriDES.TabStop = true;
            radioButtonTriDES.Text = "Triple DES";
            radioButtonTriDES.UseVisualStyleBackColor = true;
            radioButtonTriDES.CheckedChanged += radioButtonTriDES_CheckedChanged;
            // 
            // radioButtonAES
            // 
            radioButtonAES.AutoSize = true;
            radioButtonAES.Location = new Point(771, 98);
            radioButtonAES.Name = "radioButtonAES";
            radioButtonAES.Size = new Size(68, 29);
            radioButtonAES.TabIndex = 10;
            radioButtonAES.TabStop = true;
            radioButtonAES.Text = "AES";
            radioButtonAES.UseVisualStyleBackColor = true;
            radioButtonAES.CheckedChanged += radioButtonAES_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(581, 98);
            label2.Name = "label2";
            label2.Size = new Size(154, 25);
            label2.TabIndex = 11;
            label2.Text = "Choose algorithm";
            // 
            // FileCryption
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1098, 544);
            Controls.Add(label2);
            Controls.Add(radioButtonAES);
            Controls.Add(radioButtonTriDES);
            Controls.Add(DecrypttextBox);
            Controls.Add(EncrypttextBox);
            Controls.Add(buttonExportPublicKey);
            Controls.Add(buttonCreateAsmKeys);
            Controls.Add(buttonImportPublicKey);
            Controls.Add(buttonGetPrivateKey);
            Controls.Add(label1);
            Controls.Add(buttonDecryptFile);
            Controls.Add(buttonEncryptFile);
            ForeColor = SystemColors.ControlText;
            ImeMode = ImeMode.On;
            MinimumSize = new Size(1120, 600);
            Name = "FileCryption";
            RightToLeft = RightToLeft.No;
            Text = "FileCryption";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonEncryptFile;
        private Button buttonDecryptFile;
        private Label label1;
        private Button buttonGetPrivateKey;
        private Button buttonImportPublicKey;
        private Button buttonCreateAsmKeys;
        private Button buttonExportPublicKey;
        private OpenFileDialog _encryptOpenFileDialog;
        private OpenFileDialog _decryptOpenFileDialog;
        private TextBox EncrypttextBox;
        private TextBox DecrypttextBox;
        private RadioButton radioButtonTriDES;
        private RadioButton radioButtonAES;
        private Label label2;
    }
}