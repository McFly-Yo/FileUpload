using FIleUpload.Models;
using FIleUpload.Repositories.Interfaces;
using FIleUpload.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FIleUpload.Services.Implementations
{
    public class ProcessFileService : IProcessFileService
    {
        private readonly IDataRepository _dataRepository;

        public ProcessFileService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task<List<Data>> ProcessFile(IFormFile file)
        {
            List<Data> data = new List<Data>();

            if (IsFileValid(file))
            {
                var filePath = await WriteFileToDisk(file);

                var lines = await File.ReadAllLinesAsync(filePath);

                foreach (var line in lines)
                {
                    // Process Data

                    // Done processing, add to data list.
                    data.Add(new Data()
                    {
                        Content = line
                    });
                }

                data = _dataRepository.GetUniqueData(data);
                data = await _dataRepository.CreateData(data);
            }

            return data;
        }

        private bool IsFileValid(IFormFile file)
        {
            var isFileValid = false;

            if (Path.GetExtension(file.FileName).ToLower().Equals(".txt") &&
                file.Length > 0)
            {
                isFileValid = true;
            }

            return isFileValid;
        }

        private async Task<string> WriteFileToDisk(IFormFile file)
        {
            var filePath = $@"{Path.GetTempPath()}\{Path.GetRandomFileName()}";

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}
