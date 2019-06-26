using CJ.Data.FirstModels;
using CJ.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Application.Test
{
    public class TestAppService : ITestAppService
    {
        //private IRepository<Person> _repository;
        //public TestAppService(IRepository<Person> repository)
        //{
        //    _repository = repository;
        //}
        public string Test()
        {
            return "Test Suesses";
        }
    }
}
