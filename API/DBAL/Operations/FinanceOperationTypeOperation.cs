using DBAL.Constants;
using DBAL.Models;
using DBAL.Repositories;

namespace DBAL.Operations
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
  }
}
