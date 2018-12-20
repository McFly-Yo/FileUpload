using FIleUpload.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIleUpload.Repositories.Interfaces
{
    public interface IDataRepository
    {
        Task<List<Data>> CreateData(List<Data> data);
        List<Data> GetUniqueData(List<Data> data);
    }
}
