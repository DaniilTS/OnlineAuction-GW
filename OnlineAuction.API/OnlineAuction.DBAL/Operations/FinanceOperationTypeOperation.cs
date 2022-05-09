using OnlineAuction.Common.Domain.Constants;
using OnlineAuction.DBAL.Models;
using OnlineAuction.DBAL.Repositories;
using System.Collections.Generic;

namespace OnlineAuction.DBAL.Operations
{
    public class FinanceOperationTypeOperation
    {
        private readonly FinanceOperationTypeRepository _repo;
        public FinanceOperationTypeOperation(FinanceOperationTypeRepository repo)
        {
            _repo = repo;
        }

        public FinanceOperationType Add => _repo.GetObject(FinanceOperationTypes.Add).Result;
        public FinanceOperationType Withdrawal => _repo.GetObject(FinanceOperationTypes.Withdrawal).Result;
        public FinanceOperationType Notice => _repo.GetObject(FinanceOperationTypes.Notice).Result;
        public List<FinanceOperationType> Types => new() { Add, Withdrawal, Notice };
    }
}
