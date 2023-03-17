using Microsoft.Playwright;
using NUnit.Framework;
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
            Playwright.Selectors.SetTestIdAttribute("data-t");

            var page = await Context.NewPageAsync();
            var homePage = new HomePage(page);
            await page.GotoAsync(TestSettings.EnvUrl);

            await page.GetByRole(AriaRole.Button, new() { Name = "Kontynuuj bez akceptacji" }).ClickAsync();

            await page.GetByRole(AriaRole.Link, new() { Name = "Nowe" }).ClickAsync();

            int i = 0;

            while (i < 3)
            {
                await page.GotoAsync($"https://www.pepper.pl/nowe?page={i}");
            }
        }
    }
}
