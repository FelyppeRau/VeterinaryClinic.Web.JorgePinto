using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;



namespace VeterinaryClinic.Web.JorgePinto.Data
{
    public class EmailSenderRepository : IEmailSenderRepository
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "emailwebprovidercodingwooo@gmail.com";
            var password = "xgmuryodfglgnfky";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, password)
            };

            return client.SendMailAsync(
                new MailMessage(from: mail,
                                to: email,
                                subject,
                                message
                                ));
        }
    }
}
