using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace VeterinaryClinic.Web.JorgePinto.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);

    }
}
