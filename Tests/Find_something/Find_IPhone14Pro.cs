using PepperQA.Pages;
using PepperQA.Settings;
using PepperQA.Templates;

namespace PepperQA.Tests.Find_something
{
    [TestFixture]
    public class Find_IPhone14Pro : Base_tamplate
    {
        [TestCase ("Find IPhone 14 pro")]
        public async Task GotoPepper_Find_IPhone14Pro(string name)
        {
            var page = await Context.NewPageAsync();
            var homePage = new HomePage(page);
            await page.GotoAsync(TestSettings.EnvUrl);


        }
    }
}
