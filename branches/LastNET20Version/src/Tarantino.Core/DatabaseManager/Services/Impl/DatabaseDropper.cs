using System;
using StructureMap;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	[Pluggable("Drop")]
	public class DatabaseDropper : IDatabaseActionExecutor
	{
		private readonly IDatabaseConnectionDropper _connectionDropper;
		private readonly IQueryExecutor _queryExecutor;

		public DatabaseDropper(IDatabaseConnectionDropper connectionDropper, IQueryExecutor queryExecutor)
		{
			_connectionDropper = connectionDropper;
			_queryExecutor = queryExecutor;
		}

		public void Execute(string scriptFolder, ConnectionSettings settings, ITaskObserver taskObserver)
		{
			_connectionDropper.Drop(settings, taskObserver);
			string sql = string.Format("drop database {0}", settings.Database);
			_queryExecutor.ExecuteNonQuery(settings, sql, false);
		}
	}
}