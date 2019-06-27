using System;
using System.Collections.Generic;
using System.Transactions;

namespace CJ.Domain.UowManager
{
    public class UnitOfWorkDefaultOptions:IUnitOfWorkDefaultOptions
    {
        public TransactionScopeOption Scope { get; set; }

        public bool IsTransactional { get; set; }

        public TimeSpan? Timeout { get; set; }

        public bool IsTransactionScopeAvailable { get; set; }

        public IsolationLevel? IsolationLevel { get; set; }

        public List<Func<Type, bool>> ConventionalUowSelectors { get; }

        public UnitOfWorkDefaultOptions()
        {
            IsTransactional = true;
            Scope = TransactionScopeOption.Required;

            IsTransactionScopeAvailable = true;

            ConventionalUowSelectors = new List<Func<Type, bool>>
            {
                //type => typeof(IRepository).IsAssignableFrom(type) ||
                       // typeof(IApplicationService).IsAssignableFrom(type)
            };
        }
    }
}