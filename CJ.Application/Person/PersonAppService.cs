using CJ.Data.FirstModels;
using CJ.Domain.UowManager;
using CJ.Repositories.BaseRepositories;
using System.Collections.Generic;

namespace CJ.Application
{
    public class PersonAppService : IPersonAppService
    {
        private readonly IRepository<Person> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public PersonAppService(IRepository<Person> repository, IUnitOfWorkManager unitOfWorkManager)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public List<Person> GetPersons()
        {
            using (var unito = _unitOfWorkManager.Begin())
            {
                var persons = _repository.GetAllList();
                return persons;
            }
        }
    }
}
