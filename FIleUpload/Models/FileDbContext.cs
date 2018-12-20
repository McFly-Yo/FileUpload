using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FIleUpload.Models
{
    public class FileDbContext : DbContext
    {
        public DbSet<Data> Data { get; set; }

        public FileDbContext(DbContextOptions<FileDbContext> options)
            : base(options)
        { }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(entry => entry.Entity is Base &&
                                (entry.State == EntityState.Added ||
                                entry.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Base)entity.Entity).CreatedOn = DateTime.UtcNow;
                }

                ((Base)entity.Entity).LastModifiedOn = DateTime.UtcNow;
            }
        }
    }
}
