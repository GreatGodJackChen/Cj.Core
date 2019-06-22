using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CJ.Domain
{
    public class DefaultDbContextResolver 
    {
        public TDbContext Resolve<TDbContext>(string connectionString, DbConnection existingConnection) where TDbContext : DbContext
        {
            var dbContextType = typeof(TDbContext);
            Type concreteType = null;
            var isAbstractDbContext = dbContextType.GetTypeInfo().IsAbstract;
            
            //if (isAbstractDbContext)
            //{
            //    concreteType = _dbContextTypeMatcher.GetConcreteType(dbContextType);
            //}

            //try
            //{
            //    if (isAbstractDbContext)
            //    {
            //        return (TDbContext)_iocResolver.Resolve(concreteType, new
            //        {
            //            options = CreateOptionsForType(concreteType, connectionString, existingConnection)
            //        });
            //    }

            //    return _iocResolver.Resolve<TDbContext>(new
            //    {
            //        options = CreateOptions<TDbContext>(connectionString, existingConnection)
            //    });
            //}
            //catch (Castle.MicroKernel.Resolvers.DependencyResolverException ex)
            //{
            //    var hasOptions = isAbstractDbContext ? HasOptions(concreteType) : HasOptions(dbContextType);
            //    if (!hasOptions)
            //    {
            //        throw new AggregateException($"The parameter name of {dbContextType.Name}'s constructor must be 'options'", ex);
            //    }
                throw new NotImplementedException();
            }
        }
    }
