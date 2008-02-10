using Tarantino.Commons.Core.Services.Environment;
using Tarantino.Commons.Core.Services.Environment.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tarantino.IntegrationTests.Commons.Core.Services.Environment
{
	[TestFixture]
	public class ApplicationDomainTester
	{
		[Test]
		public void Correctly_Determines_Base_Folder()
		{
			IApplicationDomain domain = new ApplicationDomain();

			Assert.That(domain.GetBaseFolder().Length, Is.GreaterThan(5));
		}
	}
}