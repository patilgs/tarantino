using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;
using Tarantino.Core.DatabaseManager.Services.Impl.Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class DatabaseActionExecutorFactoryTester
	{
		[Test]
		public void Correctly_constructs_action_executors()
		{
			DatabaseAction[] actions = new DatabaseAction[] { DatabaseAction.Create, DatabaseAction.Update };

			MockRepository mocks = new MockRepository();
			IDatabaseActionResolver resolver = mocks.CreateMock<IDatabaseActionResolver>();
			IServiceLocator locator = mocks.CreateMock<IServiceLocator>();

			IDatabaseActionExecutor creator = mocks.CreateMock<IDatabaseActionExecutor>();
			IDatabaseActionExecutor updater = mocks.CreateMock<IDatabaseActionExecutor>();

			using (mocks.Record())
			{
				Expect.Call(resolver.GetActions(RequestedDatabaseAction.Create)).Return(actions);
				Expect.Call(locator.CreateInstance<IDatabaseActionExecutor>("Create")).Return(creator);
				Expect.Call(locator.CreateInstance<IDatabaseActionExecutor>("Update")).Return(updater);
			}

			using (mocks.Playback())
			{
				IDatabaseActionExecutorFactory factory = new DatabaseActionExecutorFactory(resolver, locator);
				IEnumerable<IDatabaseActionExecutor> executors = factory.GetExecutors(RequestedDatabaseAction.Create);
				IList<IDatabaseActionExecutor> executorList = new List<IDatabaseActionExecutor>(executors);

				Assert.That(executorList, Is.EqualTo(new IDatabaseActionExecutor[]{ creator, updater }));		
			}

			mocks.VerifyAll();
		}
	}
}