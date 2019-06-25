using Microsoft.EntityFrameworkCore;

namespace CJ.Domain.EntityFrameworkCore
{
    public interface IDbContextProvider<out TDbContext>
        where TDbContext : DbContext
    {
        TDbContext GetDbContext();
    }
}