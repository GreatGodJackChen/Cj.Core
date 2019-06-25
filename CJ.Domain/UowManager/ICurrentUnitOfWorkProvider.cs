using CJ.Domain.Uow;

namespace CJ.Domain.UowManager
{
    public interface ICurrentUnitOfWorkProvider
    {
        IUnitOfWork Current { get; set; }
    }
}