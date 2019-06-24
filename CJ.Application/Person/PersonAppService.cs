using CJ.Data.FirstModels;
using CJ.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Application
{
    public class PersonAppService:IPersonAppService
    {
        private readonly IRepository<Person> _repository;
        public PersonAppService(IRepository<Person> repository)
        {
            _repository = repository;
        }
        public List<Person> GetPersons()
        {
           var persons=  _repository.GetAllList();
            return persons;
        }
    }
}
