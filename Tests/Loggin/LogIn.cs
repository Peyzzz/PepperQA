using NUnit.Framework;
using PepperQA.Pages;
using PepperQA.Settings;
using PepperQA.Templates;

namespace PepperQA.Tests.Loggin
{
    [TestFixture]
    public class LogIn : Base_tamplate
    {
        private string username = "Temp1";

        [TestCase("Log to existing account")]
        public async Task TryLogIn(string name)
        {
            var page = await Context.NewPageAsync();
            await page.GotoAsync(TestSettings.EnvUrl);
            HomePage homePage = new HomePage(page);
            await homePage.AcceptCookies();
            await homePage.ClickSignUp();
            await homePage.SignUp(username, "temptemp1");
            var isExist = await homePage.AvatarExist(username);
            Assert.IsTrue(isExist);
        }
    }
}
