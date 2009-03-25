using System;
using Tarantino.Core.Deployer.Model;
using Tarantino.Core.Deployer.Services;
using Tarantino.Core.Deployer.Services.Impl;
using Tarantino.Core.Commons.Services.Environment;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Core.Deployer.Services
{
	[TestFixture]
	public class DeploymentFactoryTester
	{
		[Test]
		public void Constructs_deployment()
		{
			var mocks = new MockRepository();
			var clock = mocks.CreateMock<ISystemClock>();
			var parser = mocks.CreateMock<IRevisionNumberParser>();
			var calculator = mocks.CreateMock<IDeploymentResultCalculator>();

			using (mocks.Record())
			{
				Expect.Call(clock.GetCurrentDateTime()).Return(new DateTime(2007, 4, 15));
				Expect.Call(parser.Parse("Output...")).Return(785);
				Expect.Call(calculator.GetResult("Output...")).Return(DeploymentResult.Failure);
			}

			using (mocks.Playback())
			{
				IDeploymentFactory factory = new DeploymentFactory(clock, parser, calculator);
				Deployment deployment = factory.CreateDeployment("A1", "E1", "jsmith", "Output...");

				Assert.That(deployment.Application, Is.EqualTo("A1"));
				Assert.That(deployment.Environment, Is.EqualTo("E1"));
				Assert.That(deployment.DeployedBy, Is.EqualTo("jsmith"));
				Assert.That(deployment.DeployedOn, Is.EqualTo(new DateTime(2007, 4, 15)));
				Assert.That(deployment.Revision, Is.EqualTo(785));
				Assert.That(deployment.Output.Output, Is.EqualTo("Output..."));
				Assert.That(deployment.Result, Is.SameAs(DeploymentResult.Failure));
				Assert.That(deployment.Output.Deployment, Is.SameAs(deployment));
			}

			mocks.VerifyAll();
		}
	}
}