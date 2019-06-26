using CJ.Domain.UowManager;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Domain.EntityFrameworkCore
{
    public class DbContextTypeMatcher : DbContextTypeMatcher<DbContext>
    {
        public DbContextTypeMatcher(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
            : base(currentUnitOfWorkProvider)
        {
        }
    }
}
