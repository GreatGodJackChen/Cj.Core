namespace CJ.Domain.EntityFrameworkCore
{
    public interface IConnectionStringResolver
    {
        string GetNameOrConnectionString();
    }
}