using Microsoft.EntityFrameworkCore;

namespace CJ.Domain.Uow
{
    public interface IDbContextProvider<out TDbContext> where TDbContext : DbContext
    {
        TDbContext GetDbContext();
    }
}
