using System;
using System.Collections.Generic;

namespace CJ.Core.Infrastructure
{
    public interface ITypeFinder
    {
        IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);
    }
}