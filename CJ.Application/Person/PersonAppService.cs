using CJ.Data.FirstModels;
using CJ.Domain.UowManager;
using CJ.Repositories.BaseRepositories;
using System.Collections.Generic;

namespace CJ.Application
{
    public class PersonAppService : IPersonAppService
    {
        private readonly IRepository<Person> _repository;
        public PersonAppService(IRepository<Person> repository)
        {
            _repository = repository;
        }
        public List<Person> GetPersons()
        {
                var persons = _repository.GetAllList();
                return persons;
        }
    }
}
