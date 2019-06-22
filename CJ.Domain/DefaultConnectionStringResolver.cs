using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Domain
{
    public class DefaultConnectionStringResolver : IConnectionStringResolver
    {
        private IConfiguration _configuration;
        public DefaultConnectionStringResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual string GetNameOrConnectionString(Dictionary<string, object> args)
        {
            var connectionString = _configuration.GetSection("ConnectionStrings:Default").ToString();
            if (!string.IsNullOrEmpty(connectionString))
            {
                return connectionString;
            }
            throw new Exception("没有找到默认数据库连接");
        }
    }
}
