using System.Collections.Generic;

namespace CJ.Domain.EntityFrameworkCore
{
    public interface IConnectionStringResolver
    {
        string GetNameOrConnectionString(Dictionary<string, object> arg);
    }
}