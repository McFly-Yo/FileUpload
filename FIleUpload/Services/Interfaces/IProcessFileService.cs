using FIleUpload.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIleUpload.Services.Interfaces
{
    public interface IProcessFileService
    {
        Task<List<Data>> ProcessFile(IFormFile file);
    }
}
