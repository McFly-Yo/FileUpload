using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FIleUpload.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FIleUpload.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IProcessFileService _processFileService;

        public FileUploadController(IProcessFileService processFileService)
        {
            _processFileService = processFileService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                //return Ok(_processFileService.ProcessFile(file));
                return View("Data", await _processFileService.ProcessFile(file));
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e);
                return View("Index");
            }
        }
    }
}