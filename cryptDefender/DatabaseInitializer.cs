using System.Data.SQLite;

namespace WinFormsApp3
{
    public class DatabaseInitializer
    {
        public static void Initialize(string databaseName)
        {
            string connectionString = $"Data Source={databaseName};Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                CreateTableUsers(connection);
                CreateTablePasswordHashes(connection);
                CreateTableFileEncryption(connection);

                connection.Close();
            }
        }

        private static void CreateTableUsers(SQLiteConnection connection)
        {
            string createUsersTableQuery = @"
                CREATE TABLE IF NOT EXISTS Users (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    username TEXT NOT NULL UNIQUE,
                    password TEXT NOT NULL,
                    email TEXT NOT NULL UNIQUE,
                    login_attempts INTEGER DEFAULT 0
                )";
            using (var command = new SQLiteCommand(createUsersTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private static void CreateTablePasswordHashes(SQLiteConnection connection)
        {
            string createPasswordHashesTableQuery = @"
                CREATE TABLE IF NOT EXISTS PasswordHashes (
                    id INTEGER PRIMARY KEY,
                    hashed_password TEXT NOT NULL
                )";
            using (var command = new SQLiteCommand(createPasswordHashesTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private static void CreateTableFileEncryption(SQLiteConnection connection)
        {
            string createFileEncryptionTableQuery = @"
                CREATE TABLE IF NOT EXISTS FileEncryption (
                    encrypted_file_name TEXT NOT NULL,
                    algorithm TEXT NOT NULL,
                    FOREIGN KEY(encrypted_file_name) REFERENCES Users(encrypted_file_name)
                )";
            using (var command = new SQLiteCommand(createFileEncryptionTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}