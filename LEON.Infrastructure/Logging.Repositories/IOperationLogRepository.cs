using System;
using LEON.Repositories;
using PetaPoco;

namespace LEON.Logging.Repositories
{
	public interface IOperationLogRepository : IRepository<OperationLogEntry>
	{
		int Clean(System.DateTime? startDate, System.DateTime? endDate);
        PagingDataSet<OperationLogEntry> GetLogs(OperationLogQuery query, int pageSize, int pageIndex);
	}
}
