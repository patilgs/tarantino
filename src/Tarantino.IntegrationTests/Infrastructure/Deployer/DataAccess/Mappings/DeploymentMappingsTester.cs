using System;
using NUnit.Framework;
using Tarantino.Core.Deployer.Model;

namespace Tarantino.IntegrationTests.Infrastructure.Deployer.DataAccess.Mappings
{
	[TestFixture]
	public class DeploymentMappingTester : DeployerDatabaseTester
	{
		[Test]
		public void Can_persist_deployment()
		{
			var deployment = new Deployment
			                 	{
			                 		Application = "SampleApp1",
			                 		Environment = "Development",
			                 		CertifiedBy = "Certifer",
			                 		DeployedBy = "Deployer",
			                 		DeployedOn = new DateTime(2007, 3, 15),
			                 		CertifiedOn = new DateTime(2007, 4, 15),
			                 		Revision = 250,
			                 		Output = "Output text",
			                 		Result = DeploymentResult.Failure
			                 	};

			AssertObjectCanBePersisted(deployment);
		}
	}
}