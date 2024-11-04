using System;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using cryptDefender;

namespace WinFormsApp3
{
    public partial class LoginForm : Form
    {
        private string _databaseName;
        private string connectionString;
        private string _confirmationCode;

        public LoginForm(string databaseName)
        {
            InitializeComponent();
            _databaseName = databaseName;
            connectionString = $"Data Source={_databaseName};Version=3;";

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please provide a username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (AuthenticateUser(username, HashPassword(password)))
            {
                int loginCount = GetLoginCount(username);
                if (loginCount == 0 || loginCount % 5 == 0)
                {
                    _confirmationCode = GenerateConfirmationCode();
                    SendConfirmationCodeByEmail(GetEmailByUsername(username), _confirmationCode);

                    ConfirmationForm confirmationForm = new ConfirmationForm(_confirmationCode);
                    DialogResult result = confirmationForm.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        UpdateLoginCount(username);
                        OpenMainForm();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect confirmation code. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    UpdateLoginCount(username);
                    OpenMainForm();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm(_databaseName);
            registerForm.ShowDialog();
        }

        private void OpenMainForm()
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Hide();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE username = @username AND password = @password";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private string GetEmailByUsername(string username)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT email FROM Users WHERE username = @username";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    return command.ExecuteScalar() as string;
                }
            }
        }

        private void SendConfirmationCodeByEmail(string email, string confirmationCode)
        {
            EmailService.SendConfirmationEmail(email, confirmationCode);
        }

        private string GenerateConfirmationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private void UpdateLoginCount(string username)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE Users SET login_attempts = login_attempts + 1 WHERE username = @username";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.ExecuteNonQuery();
                }
            }
        }

        private int GetLoginCount(string username)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT login_attempts FROM Users WHERE username = @username";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    return 0;
                }
            }
        }
    }
}
