using Tarantino.Commons.Core.Model.Repositories;
using Tarantino.Commons.Core.Services.Security;
using Tarantino.Commons.Core.Services.Security.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Commons.Core.Services.Security
{
	[TestFixture]
	public class LoginCheckerTester
	{
		[Test]
		public void Can_handle_successful_login()
		{
			MockRepository mocks = new MockRepository();
			ISystemUserRepository repository = mocks.CreateMock<ISystemUserRepository>();
			IEncryptionEngine encryptionEngine = mocks.CreateMock<IEncryptionEngine>();

			using (mocks.Record())
			{
				Expect.Call(encryptionEngine.Encrypt("pass")).Return("encryptedPass");
				Expect.Call(repository.IsValidLogin("khurwitz@hotmail.com", "encryptedPass")).Return(true);
			}

			using (mocks.Playback())
			{
				ILoginChecker loginChecker = new LoginChecker(repository, encryptionEngine);
				bool isValid = loginChecker.IsValidUser("khurwitz@hotmail.com", "pass");
				
				Assert.That(isValid, Is.True);
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Can_handle_unsuccessful_login()
		{
			MockRepository mocks = new MockRepository();
			ISystemUserRepository repository = mocks.CreateMock<ISystemUserRepository>();
			IEncryptionEngine encryptionEngine = mocks.CreateMock<IEncryptionEngine>();

			using (mocks.Record())
			{
				Expect.Call(encryptionEngine.Encrypt("pass")).Return("encryptedPass");
				Expect.Call(repository.IsValidLogin("khurwitz@hotmail.com", "encryptedPass")).Return(false);
			}

			using (mocks.Playback())
			{
				ILoginChecker loginChecker = new LoginChecker(repository, encryptionEngine);
				bool isValid = loginChecker.IsValidUser("khurwitz@hotmail.com", "pass");

				Assert.That(isValid, Is.False);
			}

			mocks.VerifyAll();
		}
	}
}