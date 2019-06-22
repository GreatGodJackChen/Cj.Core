using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Domain
{
    public class DbContextProvider<TDbContext> : IDbContextProvider<TDbContext> where TDbContext : DbContext
    {

        public TDbContext DbContext { get; }

        public DbContextProvider(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public TDbContext GetDbContext()
        {
            return DbContext;
        }
    }
}
