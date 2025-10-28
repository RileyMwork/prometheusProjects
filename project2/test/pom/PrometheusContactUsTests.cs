using Microsoft.Playwright;
using project2.pom;
using System.Threading.Tasks;
using Xunit;

namespace project2.test.pom
{
    public class PrometheusContactUsTests : IAsyncLifetime
    {
        private IPlaywright playwright;
        private IBrowser browser;
        private IBrowserContext context;
        private IPage page;
        private GoogleSearch googleSearch;
        private GoogleSearchResults googleSearchResults;
        private PrometheusContactUs prometheusContactUs;

        public async Task InitializeAsync()
        {
            playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, SlowMo = 50 });
            context = await browser.NewContextAsync();
            page = await context.NewPageAsync();
            googleSearch = new GoogleSearch(page);
            googleSearchResults = new GoogleSearchResults(page);
            prometheusContactUs = new PrometheusContactUs(page);
        }

        [Fact]
        public async Task CheckForAdditionalRequiredFields()
        {
            // string searchText = "Prometheus Group";
            // await googleSearch.goToGoogle();
            // await googleSearch.enterSearchText(searchText);
            // await googleSearch.submitGoogleSearch();
            // await googleSearchResults.clickOnContactUsLink();
            await prometheusContactUs.goStraightToContactUsPage();
            await prometheusContactUs.enterTextIntoFirstNameInput("Regan");
            await prometheusContactUs.enterTextIntoLastNameInput("Martin");
            await prometheusContactUs.submitContactUsFormAsync();
            int alertCount = await prometheusContactUs.checkForAdditionalRequiredFields();
            Assert.Equal(6, alertCount);
        }


        public async Task DisposeAsync()
        {
            await context.CloseAsync();
            await browser.CloseAsync();
            playwright.Dispose();
        }
    }
}
