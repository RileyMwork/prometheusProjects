using Microsoft.Playwright;
using project2.pom;
using System.Threading.Tasks;
using Xunit;

namespace project2.test.pom
{
    public class GoogleSearchResultsTests : IAsyncLifetime
    {
        private IPlaywright playwright;
        private IBrowser browser;
        private IBrowserContext context;
        private IPage page;
        private GoogleSearch googleSearch;
        private GoogleSearchResults googleSearchResults;
        private Captcha captcha;
        public async Task InitializeAsync()
        {
            playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, SlowMo = 50 });
            context = await browser.NewContextAsync();
            page = await context.NewPageAsync();
            googleSearch = new GoogleSearch(page);
            googleSearchResults = new GoogleSearchResults(page);
            captcha = new Captcha(page);
        }

        [Fact]
        public async Task searchResultsContainPrometheusGroup()
        {
            await googleSearch.goToGoogle();
            await googleSearch.enterSearchText("Prometheus Group");
            await googleSearch.submitGoogleSearch();
            bool result = false;
            try
            {
                result = await googleSearchResults.checkResultsContainsText("Prometheus Group");
            }
            catch (TimeoutException e)
            {
                if (captcha.isCaptchaPresent().Result)
                {
                    Assert.True(false, "Captcha detected on Google search page.");
                }
            }
            Assert.True(result);
        }

        public async Task DisposeAsync()
        {
            await context.CloseAsync();
            await browser.CloseAsync();
            playwright.Dispose();
        }
    }
}
