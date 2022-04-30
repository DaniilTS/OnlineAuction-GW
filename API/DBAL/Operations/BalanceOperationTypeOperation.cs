using DBAL.Constants;
using DBAL.Models;
using DBAL.Repositories;

namespace DBAL.Operations
{
	public class BalanceOperationTypeOperation
	{
    private readonly BalanceOperationTypeRepository _repo;
    public BalanceOperationTypeOperation(BalanceOperationTypeRepository repo)
    {
      _repo = repo;
    }

    public BalanceOperationType Positive => _repo.GetObject(BalanceOperationTypes.Positive).Result;
    public BalanceOperationType Negative => _repo.GetObject(BalanceOperationTypes.Negative).Result;
  }
}
