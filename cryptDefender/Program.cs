using System;
using System.Windows.Forms;

namespace WinFormsApp3
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            string databaseName = "mydatabase.db";

            DatabaseInitializer.Initialize(databaseName);

            Application.Run(new LoginForm(databaseName));
        }
    }
}