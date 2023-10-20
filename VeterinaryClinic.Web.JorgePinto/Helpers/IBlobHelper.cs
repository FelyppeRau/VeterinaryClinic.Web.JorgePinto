﻿using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace VeterinaryClinic.Web.JorgePinto.Helpers
{
    public interface IBlobHelper
    {

        Task<Guid> UploadBlobAsync(IFormFile file, string containerName);

        Task<Guid> UploadBlobAsync(byte[] file, string containerName);  // PASSAGEM DE IMAGEM ATRAVÉS DO TELEMÓVEL - MOBILE

        Task<Guid> UploadBlobAsync(string image, string containerName);

    }
}
