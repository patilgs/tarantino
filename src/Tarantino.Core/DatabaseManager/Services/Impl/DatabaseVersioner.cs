﻿using StructureMap;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class DatabaseVersioner : IDatabaseVersioner
	{
		private IQueryExecutor _executor;
		private IResourceFileLocator _fileLocator;

		public DatabaseVersioner(IResourceFileLocator fileLocator, IQueryExecutor executor)
		{
			_fileLocator = fileLocator;
			_executor = executor;
		}

		public void VersionDatabase(ConnectionSettings settings, ITaskObserver taskObserver, string databaseVersionPropertyName)
		{
			string assembly = DatabaseUpgrader.SQL_FILE_ASSEMBLY;
			string sqlFile = string.Format(DatabaseUpgrader.SQL_FILE_TEMPLATE, "VersionDatabase");

			string sql = _fileLocator.ReadTextFile(assembly, sqlFile);
			string version = _executor.ExecuteScalarInteger(settings, sql).ToString();
			taskObserver.SetVariable(databaseVersionPropertyName, version);
		}
	}
}