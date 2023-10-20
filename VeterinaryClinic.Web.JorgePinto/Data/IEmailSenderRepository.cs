using System.Threading.Tasks;

namespace VeterinaryClinic.Web.JorgePinto.Data
{
    public interface IEmailSenderRepository
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
