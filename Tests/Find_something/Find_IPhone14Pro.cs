using Microsoft.Playwright;
using NUnit.Framework;
using PepperQA.Pages;
using PepperQA.Settings;
using PepperQA.Templates;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace PepperQA.Tests.Find_something
{
    [TestFixture]
    public class Find_IPhone14Pro : Base_template
    {
        [TestCase ("Find IPhone 14 pro")]
        [Ignore ("Temporary disabled")]
        public async Task GotoPepper_Find_IPhone14Pro(string name)
        {
            
            Playwright.Selectors.SetTestIdAttribute("data-t");

            var page = await Context.NewPageAsync();
            var homePage = new HomePage(page, Username);
            await page.GotoAsync(TestSettings.EnvUrl);
            await homePage.SelectDefineProduct("Rower");

            await page.GetByRole(AriaRole.Button, new() { Name = "Kontynuuj bez akceptacji" }).ClickAsync();

            await page.GetByRole(AriaRole.Link, new() { Name = "Dla Ciebie" }).ClickAsync();

            await page.GetByRole(AriaRole.Button, new() { Name = "Akceptuj wszystkie" }).ClickAsync();

            int i = 0;

            while (i < 1)
            {
                await page.GotoAsync($"https://www.pepper.pl/dlaciebie?page={i}");

                var locator = page.GetByTitle(new Regex("KREATYNA"));
                await Assertions.Expect(locator).ToContainTextAsync(new Regex("KREATYNA"));

                await locator.ClickAsync();

                i++;
            }

            
        }
    }
}
