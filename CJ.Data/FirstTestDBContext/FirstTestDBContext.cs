using System;
using CJ.Data.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace CJ.Data.FirstModels
{
    public partial class FirstTestDBContext : DbContext
    {
        public FirstTestDBContext()
        {
        }

        public FirstTestDBContext(DbContextOptions<FirstTestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Person> Person { get; set; }
        //private static readonly LoggerFactory LoggerFactory = new LoggerFactory(new[] { new MyFilteredLoggerProvider((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information) });

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseLoggerFactory(GetLoggerFactory.LoggerFactory);
        //    }

        //    base.OnConfiguring(optionsBuilder);
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=.;User Id=sa;Password=sa_112212;Database=FirstTestDB");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
