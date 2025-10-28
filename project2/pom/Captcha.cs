using System.Threading.Tasks;
using Microsoft.Playwright;

namespace project2.pom
{
    public class Captcha
    {
        private readonly IPage page;

        public Captcha(IPage page)
        {
            this.page = page;
        }

        public async Task<bool> isCaptchaPresent()
        {
            var captchaElement = page.Locator("xpath=//div[@class='g-recaptcha']");
            return await captchaElement.IsVisibleAsync();
        }
    }
}
