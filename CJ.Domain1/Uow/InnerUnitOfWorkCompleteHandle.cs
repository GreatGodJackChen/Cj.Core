using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CJ.Domain.Uow
{
    public class InnerUnitOfWorkCompleteHandle: IUnitOfWorkCompleteHandle
    {
        public const string DidNotCallCompleteMethodExceptionMessage = "Did not call Complete method of a unit of work.";

        private volatile bool _isCompleteCalled;
        private volatile bool _isDisposed;

        public void Complete()
        {
            _isCompleteCalled = true;
        }

        public Task CompleteAsync()
        {
            _isCompleteCalled = true;
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            if (!_isCompleteCalled)
            {
                if (HasException())
                {
                    return;
                }

                throw new Exception(DidNotCallCompleteMethodExceptionMessage);
            }
        }

        private static bool HasException()
        {
            try
            {
#pragma warning disable 618
                return Marshal.GetExceptionCode() != 0;
#pragma warning restore 618
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}