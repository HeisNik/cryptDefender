using cryptDefender;
using System;
using System.Windows.Forms;
using test3;

namespace WinFormsApp3
{
    public partial class MainForm : Form
    {
        private string _databaseName = "mydatabase.db";
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Passwordhash PasswordhashForm = new Passwordhash(_databaseName);

            PasswordhashForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PortSkanner portSkannerForm = new PortSkanner();

            portSkannerForm.Show();
        }

        private void FileEncryptionButton_Click(object sender, EventArgs e)
        {

            string databaseName = "mydatabase.db";

            DatabaseInitializer.Initialize(databaseName);
            FileCryption fileCryptionForm = new FileCryption(databaseName);

            fileCryptionForm.Show();
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            keylogger keyloggerForm = new keylogger ();

            keyloggerForm.Show();
        }
    }
}