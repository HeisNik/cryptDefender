using System;
using System.Net;
using System.Net.Mail;

public class EmailService
{
    private const string SMTPServer = "smtp.gmail.com";
    private const int SMTPPort = 587;
    private const string SMTPUsername = "cryptdefendertest@gmail.com";
    private const string SMTPPassword = "muljpccommdkomcp";
    public static void SendConfirmationEmail(string toEmail, string confirmationCode)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(SMTPServer);

            mail.From = new MailAddress(SMTPUsername);
            mail.To.Add(toEmail);
            mail.Subject = "Account Confirmation";
            mail.Body = "Your confirmation code is: " + confirmationCode;

            SmtpServer.Port = SMTPPort;
            SmtpServer.Credentials = new NetworkCredential(SMTPUsername, SMTPPassword);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            Console.WriteLine("Confirmation email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending confirmation email: " + ex.Message);
        }
    }
}
