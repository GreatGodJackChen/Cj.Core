using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace CJ.Domain.EntityFrameworkCore
{
    public class DefaultConnectionStringResolver: IConnectionStringResolver
    {
        private readonly IConfiguration _configuration;

        public DefaultConnectionStringResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual string GetNameOrConnectionString(Dictionary<string, object> arg)
        {
            return _configuration["ConnectionStrings:Default"];
        }
    }
}