using System;
using Microsoft.EntityFrameworkCore;

namespace CJ.Domain.Uow
{
    public static class UnitOfWorkExtensions
    {
        public static TDbContext GetDbContext<TDbContext>(this IActiveUnitOfWork unitOfWork, string name = null)
            where TDbContext : DbContext
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            if (!(unitOfWork is UnitOfWork))
            {
                throw new ArgumentException("unitOfWork is not type of " + typeof(UnitOfWork).FullName, "unitOfWork");
            }

            return (unitOfWork as UnitOfWork).GetOrCreateDbContext<TDbContext>();
        }
    }
}