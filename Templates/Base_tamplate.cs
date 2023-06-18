﻿using Microsoft.Playwright;
using NUnit.Framework;
using PepperQA.Pages;
using PepperQA.Settings;

namespace PepperQA.Templates
{
    public class Base_tamplate
    {
        protected IPlaywright Playwright;
        protected IBrowser Browser;
        protected IBrowserContext Context;
        protected IPage Page;

        [SetUp]
        public async Task Setup()
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions() { Headless = false, Channel = "chrome" });
            Context = await Browser.NewContextAsync();

            await Context.Tracing.StartAsync(new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true,
            });

            Page = await Context.NewPageAsync();
            await Page.GotoAsync(TestSettings.EnvUrl);
            HomePage homePage = new HomePage(Page);
            await homePage.AcceptCookies();
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
    }
}
