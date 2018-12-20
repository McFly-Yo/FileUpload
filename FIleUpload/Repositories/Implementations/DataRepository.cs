using FIleUpload.Models;
using FIleUpload.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIleUpload.Repositories.Implementations
{
    public class DataRepository : IDataRepository
    {
        private readonly FileDbContext _context;

        public DataRepository(FileDbContext context)
        {
            _context = context;
        }

        public async Task<List<Data>> CreateData(List<Data> data)
        {
            await _context.Data.AddRangeAsync(data);
            await _context.SaveChangesAsync();

            return data;
        }

        public List<Data> GetUniqueData(List<Data> data)
        {
            return data.Where(d => !_context.Data.Select(p => p.Content).Contains(d.Content)).ToList();
            //return _context.Data.Where(d => !data.Select(column => column.Content).Equals(d.Content)).ToList();
        }
    }
}
