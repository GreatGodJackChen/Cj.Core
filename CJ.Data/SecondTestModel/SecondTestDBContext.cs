using System;
using CJ.Data.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace CJ.Data.SecondTestModel
{
    public partial class SecondTestDBContext : DbContext
    {
        public SecondTestDBContext()
        {
        }

        public SecondTestDBContext(DbContextOptions<SecondTestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }

        //private static readonly LoggerFactory LoggerFactory = new LoggerFactory(new[] { new MyFilteredLoggerProvider((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information) });

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseLoggerFactory(GetLoggerFactory.LoggerFactory);
        //    }

        //    base.OnConfiguring(optionsBuilder);
        //}
        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SecondTestDbB;User ID=sa;Password=sa_112212;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
