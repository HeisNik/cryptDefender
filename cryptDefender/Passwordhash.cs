using System;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;
using BCrypt;

namespace test3
{
    public partial class Passwordhash : Form
    {
        private bool passwordHashed = false;
        private string _databaseName;
        private string connectionString;

        public Passwordhash(string databaseName)
        {
            InitializeComponent();
            txtPassword.TextChanged += txtPassword_TextChanged;
            _databaseName = databaseName;
            connectionString = $"Data Source={_databaseName};Version=3;";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (passwordHashed)
            {
                MessageBox.Show("Password has already been hashed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter a password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
            txtHashedPassword.Text = hashedPassword;

            SavePasswordToDatabase(hashedPassword);
            button1.Enabled = false;
            passwordHashed = true;
        }
        private void SavePasswordToDatabase(string hashedPassword)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertPasswordQuery = "INSERT INTO PasswordHashes (hashed_password) VALUES (@hashedPassword)";

                using (SQLiteCommand command = new SQLiteCommand(insertPasswordQuery, connection))
                {
                    command.Parameters.AddWithValue("@hashedPassword", hashedPassword);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            string strength = EvaluatePasswordStrength(password);
            lblPasswordStrength.Text = "Password Strength: " + strength;
        }

        private string EvaluatePasswordStrength(string password)
        {
            if (password.Length < 8)
            {
                return "Weak";
            }

            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            string specialCharacters = @"%!@#$%^&*()?/>.<,~`[]{}'""";
            bool hasSpecialCharacters = password.Any(specialCharacters.Contains);

            if (hasUpperCase && hasLowerCase && hasSpecialCharacters)
            {
                return "Strong";
            }
            else if ((hasUpperCase && hasLowerCase) || (hasUpperCase && hasSpecialCharacters) || (hasLowerCase && hasSpecialCharacters))
            {
                return "Medium";
            }
            else
            {
                return "Weak";
            }
        }
    }
}
