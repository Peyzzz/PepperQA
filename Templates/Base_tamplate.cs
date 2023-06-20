using Microsoft.Playwright;
using NUnit.Framework;
using PepperQA.Pages;
using PepperQA.Settings;

namespace PepperQA.Templates
{
    public class Base_template
    {
        protected IPlaywright Playwright;
        protected IBrowser Browser;
        protected IBrowserContext Context;
        protected IPage page;
        protected HomePage homepage;

        public string Username { get; set; }

        [SetUp]
        public async Task Setup()
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions() { Headless = true, Channel = "chrome" });
            Context = await Browser.NewContextAsync();

            await Context.Tracing.StartAsync(new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true,
            });

            page = await Context.NewPageAsync();
            IResponse? response = await page.GotoAsync(TestSettings.EnvUrl);
            Assert.That(response.Status, Is.EqualTo(200));
            homepage = new HomePage(page, Username);
            await homepage.AcceptCookies();
        }

        [TearDown]
        public async Task Teardown()
        {
            await Context.Tracing.StopAsync(new TracingStopOptions
            {
                Path = $"Pepper_logs{Guid.NewGuid()}.zip"
            });
            await Browser.CloseAsync();
        }

        public HomePage GetHomePage()
        {
            return homepage;
        }
    }
}
