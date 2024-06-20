using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Context
{
    public class ByteGamingContext : DbContext
    {
        public ByteGamingContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=tcp:bytegamingdbdbserver.database.windows.net,1433;Database=ByteGamingDb;User ID=ByteGamingAdmin@bytegamingdbdbserver;Password=Rv5No%88c&zrc9&;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;",
                sqlOptions => sqlOptions.EnableRetryOnFailure()
            );
        }

        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}