using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Domain
{
     public interface IDbContextTypeMatcher
    {
        Type GetConcreteType(Type sourceDbContextType);
    }
}
