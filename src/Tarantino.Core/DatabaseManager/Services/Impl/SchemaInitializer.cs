﻿using Tarantino.Core.Commons.Services.Environment;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.DatabaseManager.Services.Impl
{
	public class SchemaInitializer : ISchemaInitializer
	{
		private readonly IQueryExecutor _executor;
		private readonly IResourceFileLocator _locator;

		public SchemaInitializer(IResourceFileLocator locator, IQueryExecutor executor)
		{
			_locator = locator;
			_executor = executor;
		}

		public void EnsureSchemaCreated(ConnectionSettings settings)
		{
			string sql = _locator.ReadTextFile("Tarantino.DatabaseManager.NAntTasks", "Tarantino.DatabaseManager.NAntTasks.Files.CreateSchema.sql");

			_executor.ExecuteNonQuery(settings, sql);
		}
	}
}