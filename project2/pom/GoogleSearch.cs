using System.Threading.Tasks;
using Microsoft.Playwright;

namespace project2.pom
{
    public class GoogleSearch
    {
        private readonly IPage page;
        private ILocator searchBox => page.Locator("xpath=/html/body/div[2]/div[4]/form/div[1]/div[1]/div[1]/div[1]/div[2]/textarea");

        public GoogleSearch(IPage page)
        {
            this.page = page;
        }

        public async Task goToGoogle()
        {
            await page.GotoAsync("https://www.google.com");
        }

        public async Task enterSearchText(string text)
        {
            await searchBox.FillAsync(text);
        }

        public async Task submitGoogleSearch()
        {
            await page.Keyboard.PressAsync("Enter");
        }

        public async Task<string> getSearchBoxValue()
        {
            return await searchBox.InputValueAsync();
        }
    }
}
