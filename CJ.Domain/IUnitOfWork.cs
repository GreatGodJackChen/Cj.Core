using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Domain
{
    public interface IUnitOfWork
    {
        int SaveChanges();
    }
}
