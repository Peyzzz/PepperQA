using NUnit.Framework;
using PepperQA.Pages;
using PepperQA.Settings;
using PepperQA.Templates;

namespace PepperQA.Tests.Loggin
{
    [TestFixture]
    public class LogIn : Base_template
    {
        public LogIn()
        {
            Username = "Temp1";
        }

        [TestCase("Log in to an existing account.")]
        public async Task LogInToExistingAccount(string name)
        {
            Assert.That(page.Url.ToString(), Is.EqualTo(TestSettings.EnvUrl));
            await homepage.ClickSignUp();
            await homepage.SignUp(Username, "temptemp1");
            var isExist = await homepage.AvatarExist(Username);
            Assert.IsTrue(isExist);
        }

        [TestCase("Log in to an existing account without password.")]
        public async Task LogInToExistingAccountWithoutPassword(string name)
        {
            Assert.That(page.Url.ToString(), Is.EqualTo(TestSettings.EnvUrl));
            await homepage.ClickSignUp();
            await homepage.SignUp(Username, string.Empty);
            Assert.IsTrue(await page.GetByText("To pole jest wymagane.").IsVisibleAsync());
        }

        [TestCase("Log in to a non-existent account.")]
        public async Task LogInToNonExistentAccount(string name)
        {
            await homepage.ClickSignUp();
            await homepage.SignUp("shouldnotexist", "123123456");
            Assert.IsTrue(await page.GetByText("Nieprawidłowe hasło Wygląda na to że wpisałeś złe hasło. Spróbuj ponownie.").IsVisibleAsync());
        }
    }
}
