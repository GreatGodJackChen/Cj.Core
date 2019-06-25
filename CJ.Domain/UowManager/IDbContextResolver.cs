using Microsoft.EntityFrameworkCore;

namespace CJ.Domain.UowManager
{
    public interface IDbContextResolver
    {
        TDbContext Resolve<TDbContext>() where TDbContext : DbContext;
    }
}