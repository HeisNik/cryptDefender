using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace cryptDefender
{
    partial class keylogger
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
            durationTextBox = new TextBox();
            logPathTextBox = new TextBox();
            logPathButton = new Button();
            startButton = new Button();
            emailTextBox = new TextBox();
            sendEmailCheckBox = new CheckBox();
            emailLabel = new Label();
            loggingStatusLabel = new Label();
            durationLabel = new Label();
            SuspendLayout();

            durationTextBox.Location = new Point(139, 30);
            durationTextBox.Margin = new Padding(4, 3, 4, 3);
            durationTextBox.Name = "durationTextBox";
            durationTextBox.Size = new Size(47, 23);
            durationTextBox.TabIndex = 0;

            logPathTextBox.Location = new Point(139, 70);
            logPathTextBox.Margin = new Padding(4, 3, 4, 3);
            logPathTextBox.Name = "logPathTextBox";
            logPathTextBox.Size = new Size(174, 23);
            logPathTextBox.TabIndex = 1;

            logPathButton.Location = new Point(14, 70);
            logPathButton.Margin = new Padding(4, 3, 4, 3);
            logPathButton.Name = "logPathButton";
            logPathButton.Size = new Size(117, 23);
            logPathButton.TabIndex = 2;
            logPathButton.Text = "Select Path";
            logPathButton.UseVisualStyleBackColor = true;
            logPathButton.Click += logPathButton_Click;

            startButton.Location = new Point(13, 181);
            startButton.Margin = new Padding(4, 3, 4, 3);
            startButton.Name = "startButton";
            startButton.Size = new Size(117, 23);
            startButton.TabIndex = 3;
            startButton.Text = "Start Logging";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += StartButton_Click;

            emailTextBox.Location = new Point(139, 116);
            emailTextBox.Margin = new Padding(4, 3, 4, 3);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.Size = new Size(174, 23);
            emailTextBox.TabIndex = 4;

            sendEmailCheckBox.AutoSize = true;
            sendEmailCheckBox.Location = new Point(320, 120);
            sendEmailCheckBox.Name = "sendEmailCheckBox";
            sendEmailCheckBox.Size = new Size(84, 19);
            sendEmailCheckBox.TabIndex = 5;
            sendEmailCheckBox.Text = "Send Email";
            sendEmailCheckBox.UseVisualStyleBackColor = true;

            emailLabel.AutoSize = true;
            emailLabel.Location = new Point(14, 119);
            emailLabel.Name = "emailLabel";
            emailLabel.Size = new Size(84, 15);
            emailLabel.TabIndex = 1;
            emailLabel.Text = "Email Address:";

            loggingStatusLabel.AutoSize = true;
            loggingStatusLabel.Location = new Point(180, 185);
            loggingStatusLabel.Name = "loggingStatusLabel";
            loggingStatusLabel.Size = new Size(111, 15);
            loggingStatusLabel.TabIndex = 0;
            loggingStatusLabel.Text = "Logging status here";

            durationLabel.AutoSize = true;
            durationLabel.Location = new Point(13, 33);
            durationLabel.Name = "durationLabel";
            durationLabel.Size = new Size(110, 15);
            durationLabel.TabIndex = 0;
            durationLabel.Text = "Duration (minutes):";

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 227);
            Controls.Add(durationTextBox);
            Controls.Add(durationLabel);
            Controls.Add(logPathTextBox);
            Controls.Add(logPathButton);
            Controls.Add(startButton);
            Controls.Add(emailTextBox);
            Controls.Add(sendEmailCheckBox);
            Controls.Add(emailLabel);
            Controls.Add(loggingStatusLabel);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
#endregion