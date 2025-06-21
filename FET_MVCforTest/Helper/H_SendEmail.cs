using FET_MVCforTest.Security.Entities;
using System.Net;
using System.Net.Mail;

namespace FET_MVCforTest.Helper
{
	public class H_SendEmail
	{
        public static Task SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("mhmdmahmod91@gmail.com", "cesx wmgm aqei amtm");

            client.SendAsync("mhmdmahmod91@gmail.com", email.To, email.Subject, email.Body,null);
            return Task.CompletedTask;
        }
    }
}
