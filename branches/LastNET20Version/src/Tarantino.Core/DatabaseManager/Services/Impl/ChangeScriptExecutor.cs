﻿using System.IO;
using StructureMap;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class ChangeScriptExecutor : IChangeScriptExecutor
	{
		private IScriptExecutionTracker _executionTracker;
		private IQueryExecutor _executor;
		private IFileSystem _fileSystem;

		public ChangeScriptExecutor(IScriptExecutionTracker executionTracker, IQueryExecutor executor, IFileSystem fileSystem)
		{
			_executionTracker = executionTracker;
			_executor = executor;
			_fileSystem = fileSystem;
		}

		public void Execute(string fullFilename, ConnectionSettings settings, ITaskObserver taskObserver)
		{
			string scriptFilename = getFilename(fullFilename);

			if (_executionTracker.ScriptAlreadyExecuted(settings, scriptFilename))
			{
				taskObserver.Log(string.Format("Skipping (already executed): {0}", scriptFilename));
			}
			else
			{
				taskObserver.Log(string.Format("Executing: {0}", scriptFilename));
				string sql = _fileSystem.ReadTextFile(fullFilename);
				_executor.ExecuteNonQuery(settings, sql, true);
				_executionTracker.MarkScriptAsExecuted(settings, scriptFilename, taskObserver);
			}
		}

		private string getFilename(string fullFilename)
		{
			return Path.GetFileName(fullFilename);
		}
	}
}