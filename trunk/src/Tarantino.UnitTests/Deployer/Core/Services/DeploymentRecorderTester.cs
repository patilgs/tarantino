using Tarantino.Deployer.Core.Model;
using Tarantino.Deployer.Core.Services;
using Tarantino.Deployer.Core.Services.Impl;
using Tarantino.Commons.Core;
using Tarantino.Commons.Core.Services.Security;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Deployer.Core.Services
{
	[TestFixture]
	public class DeploymentRecorderTester
	{
		[Test]
		public void Records_deployment()
		{
			Deployment deployment = new Deployment();

			MockRepository mocks = new MockRepository();
			IDeploymentFactory factory = mocks.CreateMock<IDeploymentFactory>();
			IPersistentObjectRepository repository = mocks.CreateMock<IPersistentObjectRepository>();
			ISecurityContext context = mocks.CreateMock<ISecurityContext>();

			using (mocks.Record())
			{
				Expect.Call(context.GetCurrentUsername()).Return("jsmith");
				Expect.Call(factory.CreateDeployment("application", "environment", "jsmith", "Output...")).Return(deployment);
				repository.Save(deployment);
			}

			using (mocks.Playback())
			{
				IDeploymentRecorder recorder = new DeploymentRecorder(context, factory, repository);
				recorder.RecordDeployment("application", "environment", "Output...");
			}

			mocks.VerifyAll();
		}
	}
}