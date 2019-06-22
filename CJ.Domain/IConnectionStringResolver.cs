using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Domain
{
    public interface IConnectionStringResolver
    {
        string GetNameOrConnectionString(Dictionary<string, object> args);
    }
}
