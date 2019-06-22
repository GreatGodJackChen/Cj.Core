using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Domain
{
    interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}
