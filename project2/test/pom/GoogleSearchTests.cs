using Microsoft.Playwright;
using project2.pom;
using System;
using System.Threading.Tasks;
using Xunit;

namespace project2.test.pom
{
    public class GoogleSearchTests : IAsyncLifetime
    {
        private IPlaywright playwright;
        private IBrowser browser;
        private IPage page;
        private IBrowserContext context;
        private GoogleSearch googleSearch;

        public async Task InitializeAsync()
        {
            playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false, SlowMo = 50
            });
            context = await browser.NewContextAsync();
            page = await context.NewPageAsync();
            googleSearch = new GoogleSearch(page);
        }

        [Fact]
        public async Task OnGoogleHomepage()
        {
            await googleSearch.goToGoogle();
            Assert.Contains("google.com", page.Url);
        }

        [Fact]
        public async Task searchStringIsEnteredIntoSearchBox()
        {
            string searchTerm = "Prometheus Group";
            await googleSearch.goToGoogle();
            await googleSearch.enterSearchText(searchTerm);
            string actualValue = await googleSearch.getSearchBoxValue();
            Assert.Equal(searchTerm, actualValue);
        }

        public async Task DisposeAsync()
        {
            await context.CloseAsync();
            await browser.CloseAsync();
            playwright.Dispose();
        }
    }
}
