using System;
using System.Collections.Generic;
using CJ.Data.FirstModels;
using CJ.Repositories.BaseRepositories;

namespace CJ.Application.Address
{
    public class AddressAppService:IAddressAppService
    {
        private readonly IRepository<Data.SecondTestModel.Address> _repository;
        private readonly IPersonAppService _personAppService;

        public AddressAppService(IRepository<Data.SecondTestModel.Address> repository, IPersonAppService personAppService)
        {
            _repository = repository;
            _personAppService = personAppService;
        }

        public List<Data.SecondTestModel.Address> GetAddresses()
        {
            var persons=_personAppService.GetPersons();
            var address = _repository.GetAllList();
            return address;
        }
    }
}
