using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StackRestApi.Models
{
    public class StackContext : DbContext, IStackContext
    {

        public StackContext(DbContextOptions<StackContext> options)
            : base(options)
        {
        }

        public DbSet<StackItem> StackItems { get; set; }

    }

    public interface IStackContext
    {
        DbSet<StackItem> StackItems { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
