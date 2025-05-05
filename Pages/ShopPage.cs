using Microsoft.Playwright;
using PlanitAutomation.Tests;

namespace PlanitAutomation.Pages
{
    public class ShopPage: BaseTest
    {
        private readonly IPage _page;

        private ILocator _stuffedFrogBuyLocator => _page.Locator("#product-2").Locator("a:has-text(\"Buy\")");
        private ILocator _fluffyBunnyBuyLocator => _page.Locator("#product-4").Locator("a:has-text(\"Buy\")");
        private ILocator _valentineBearBuyLocator => _page.Locator("#product-7").Locator("a:has-text(\"Buy\")");
        private ILocator _stuffedFrogPriceLocator => _page.Locator("li#product-2 >> span.product-price");
        private ILocator _fluffuBunnyPriceLocator => _page.Locator("li#product-4 >> span.product-price");
        private ILocator _valentineBearPriceLocator => _page.Locator("li#product-7 >> span.product-price");

        public ShopPage(IPage page)
        {
            _page = page;
        }

        public ILocator GetStuffedFrogBuyLocator()
        {
            return _stuffedFrogBuyLocator;
        }

        public ILocator GetFluffyBunnyBuyLocator()
        {
            return _fluffyBunnyBuyLocator;
        }

        public ILocator GetValentineBearBuyLocator()
        {
            return _valentineBearBuyLocator;
        }

        public async Task<string> GetStuffedFrogPrice()
        {
            // var locator = _page.Locator("li#product-2 >> span.product-price");
            string priceText = await _stuffedFrogPriceLocator.InnerTextAsync();
            // TestContext.WriteLine(priceText); 

            return priceText;
        }

        public async Task<string> GetFluffyBynnyPrice()
        {
            // var locator = _page.Locator("li#product-4 >> span.product-price");
            string priceText = await _fluffuBunnyPriceLocator.InnerTextAsync();
            // TestContext.WriteLine(priceText); 

            return priceText;
        }

        public async Task<string> GetValentineBearPrice()
        {
            // var locator = _page.Locator("li#product-7 >> span.product-price");
            string priceText = await _valentineBearPriceLocator.InnerTextAsync();
            // TestContext.WriteLine(priceText); 

            return priceText;
        }

        public async Task ClickStuffedFrogMultipleTimesAsync(int times)
        {
            for (int i = 0; i < times; i++)
            {
                await _stuffedFrogBuyLocator.ClickAsync();
            }
        }

        public async Task ClickFluffyBunnyMultipleTimesAsync(int times)
        {
            for (int i = 0; i < times; i++)
            {
                await _fluffyBunnyBuyLocator.ClickAsync();
            }
        }

        public async Task ClickValentineBearMultipleTimesAsync(int times)
        {
            for (int i = 0; i < times; i++)
            {
                await _valentineBearBuyLocator.ClickAsync();
            }
        }

    }
}
