using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services
{
	[TestFixture]
	public class ApplicationInstanceContextTester
	{
		[Test]
		public void Retrieves_application_instance_from_cache()
		{
			ApplicationInstance instance = new ApplicationInstance();

			MockRepository mocks = new MockRepository();
			IApplicationInstanceCache cache = mocks.CreateMock<IApplicationInstanceCache>();

			using (mocks.Record())
			{
				Expect.Call(cache.GetCurrent()).Return(instance);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceContext instanceContext = new ApplicationInstanceContext(cache, null, null);
				Assert.That(instanceContext.GetCurrent(), Is.SameAs(instance));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Retrieves_application_instance_when_not_found_in_cache()
		{
			ApplicationInstance instance = new ApplicationInstance();

			MockRepository mocks = new MockRepository();
			IApplicationInstanceCache cache = mocks.CreateMock<IApplicationInstanceCache>();
			ICurrentApplicationInstanceRetriever retriever = mocks.CreateMock<ICurrentApplicationInstanceRetriever>();

			using (mocks.Record())
			{
				Expect.Call(cache.GetCurrent()).Return(null);
				Expect.Call(retriever.GetApplicationInstance()).Return(instance);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceContext instanceContext = new ApplicationInstanceContext(cache, retriever, null);
				Assert.That(instanceContext.GetCurrent(), Is.SameAs(instance));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Creates_new_application_instance_when_error_occurs_retrieving_instance_from_database()
		{
			ApplicationInstance instance = new ApplicationInstance();

			MockRepository mocks = new MockRepository();
			IApplicationInstanceCache cache = mocks.CreateMock<IApplicationInstanceCache>();
			ICurrentApplicationInstanceRetriever retriever = mocks.CreateMock<ICurrentApplicationInstanceRetriever>();
			IApplicationInstanceFactory factory = mocks.CreateMock<IApplicationInstanceFactory>();

			using (mocks.Record())
			{
				Expect.Call(cache.GetCurrent()).Return(null);
				Expect.Call(retriever.GetApplicationInstance()).Throw(new Exception());
				Expect.Call(factory.Create()).Return(instance);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceContext instanceContext = new ApplicationInstanceContext(cache, retriever, factory);
				Assert.That(instanceContext.GetCurrent(), Is.SameAs(instance));
			}

			mocks.VerifyAll();
		}
	}
}