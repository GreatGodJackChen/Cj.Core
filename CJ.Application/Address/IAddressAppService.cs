using System.Collections.Generic;
using CJ.Data.FirstModels;

namespace CJ.Application.Address
{
    public interface IAddressAppService
    {
        List<Data.SecondTestModel.Address> GetAddresses();
    }
}