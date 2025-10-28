using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace project2.pom
{
    public class PrometheusContactUs
    {
        private readonly IPage page;

        public PrometheusContactUs(IPage page)
        {
            this.page = page;
        }

        public async Task goStraightToContactUsPage()
        {
            await page.GotoAsync("https://www.prometheusgroup.com/contact-us");
        }

        public async Task enterTextIntoFirstNameInput(string text)
        {
            var element = page.Locator("[name='firstname']");
            await element.FillAsync(text);
        }

        public async Task enterTextIntoLastNameInput(string text)
        {
            var element = page.Locator("[name='lastname']");
            await element.FillAsync(text);
        }

        public async Task submitContactUsFormAsync()
        {
            var submitButton = page.Locator("xpath=//input[@type='submit']");
            await submitButton.ClickAsync();
        }

        public async Task<int> checkForAdditionalRequiredFields()
        {
            var alerts = page.Locator("[role='alert']");
            int count = await alerts.CountAsync();
            return count;
        }
    }
}
