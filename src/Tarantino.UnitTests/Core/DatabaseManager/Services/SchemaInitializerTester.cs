using System;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.DatabaseManager.Model;
using Tarantino.DatabaseManager.Services;
using Tarantino.DatabaseManager.Services.Impl;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class SchemaInitializerTester
	{
		[Test]
		public void CorrectlyInitializesSchema()
		{
			ConnectionSettings settings =
				new ConnectionSettings(String.Empty, String.Empty, false, String.Empty, String.Empty);
			string sqlScript = "SQL script...";

			MockRepository mocks = new MockRepository();
			IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
			IQueryExecutor queryExecutor = mocks.CreateMock<IQueryExecutor>();

			Expect.Call(fileLocator.ReadTextFile("Tarantino.DatabaseManager.NAntTasks", "Tarantino.DatabaseManager.NAntTasks.Files.CreateSchema.sql")).Return(sqlScript);
			queryExecutor.ExecuteNonQuery(settings, sqlScript);

			mocks.ReplayAll();

			ISchemaInitializer versioner = new SchemaInitializer(fileLocator, queryExecutor);
			versioner.EnsureSchemaCreated(settings);

			mocks.VerifyAll();
		}
	}
}