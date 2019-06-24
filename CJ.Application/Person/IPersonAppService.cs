using CJ.Data.FirstModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Application
{
    public interface IPersonAppService
    {
        List<Person> GetPersons();
    }
}
