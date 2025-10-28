using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace project2.pom
{
    public class GoogleSearchResults
    {
        private readonly IPage page;

        public GoogleSearchResults(IPage page)
        {
            this.page = page;
        }

        public async Task goStraightToResults()
        {
            await page.GotoAsync("https://www.google.com/search?q=Prometheus+Group");
        }

        public async Task<bool> checkResultsContainsText(string text)
        {
            var resultsContainer = page.Locator("xpath=//h3");
            await resultsContainer.First.WaitForAsync(new LocatorWaitForOptions { Timeout = 5000 });

            var texts = await resultsContainer.AllInnerTextsAsync();
            return texts.Any(t => t.Contains(text, StringComparison.OrdinalIgnoreCase));
        }


        public async Task clickOnContactUsLink()
        {
            var contactUsLink = page.Locator("a[href='https://www.prometheusgroup.com/contact-us']");
            await contactUsLink.WaitForAsync();
            await contactUsLink.ClickAsync();
        }
    }
}
