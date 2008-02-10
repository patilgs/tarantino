using Tarantino.Commons.Core.Model.Enumerations;

namespace Tarantino.Deployer.Core.Model.Enumerations
{
	public class DeploymentResult : Enumeration
	{
		public static readonly DeploymentResult Success = new DeploymentResult(1, "Success");
		public static readonly DeploymentResult Failure = new DeploymentResult(2, "Failure");

		public DeploymentResult()
		{
		}

		public DeploymentResult(int value, string displayName) : base(value, displayName)
		{
		}
	}
}