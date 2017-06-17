using System;
namespace LEON.Logging
{
	public interface IOperationLogSpecificPartProcesser<TEntity>
	{
		void Process(TEntity entity, string eventOperationType, IOperationLogSpecificPart operationLogSpecificPart);
		void Process(TEntity entity, string eventOperationType, TEntity historyData, IOperationLogSpecificPart operationLogSpecificPart);
	}
}
