using NUnit.Framework;
using PepperQA.Pages;
using PepperQA.Settings;
using PepperQA.Templates;
using System.Text.RegularExpressions;

namespace PepperQA.Tests.Loggin
{
    [TestFixture]
    public class LogIn : Base_tamplate
    {
        private string username = "Temp1";

        [TestCase("Log in to an existing account.")]
        public async Task LogInToExistingAccount(string name)
        {
            Assert.That(Page.Url.ToString(), Is.EqualTo(TestSettings.EnvUrl));
            HomePage homePage = new HomePage(Page);
            await homePage.ClickSignUp();
            await homePage.SignUp(username, "temptemp1");
            var isExist = await homePage.AvatarExist(username);
            Assert.IsTrue(isExist);
        }

        [TestCase("Log in to a non-existent account.")]
        public async Task LogInToNonExistentAccount(string name)
        {
            HomePage homePage = new HomePage(Page);
            await homePage.ClickSignUp();
            await homePage.SignUp("shouldnotexist", "123123456");
            Assert.IsTrue(await Page.GetByText("Nieprawidłowe hasło Wygląda na to że wpisałeś złe hasło. Spróbuj ponownie.").IsVisibleAsync());
        }
    }
}
