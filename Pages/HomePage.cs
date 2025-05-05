using Microsoft.Playwright;
using PlanitAutomation.Tests;

namespace PlanitAutomation.Pages
{
    public class HomePage: BaseTest
    {
        private readonly IPage _page;

        private ILocator _homeTab => _page.Locator("role=link[name='Home']");
        private ILocator _shopTab => _page.Locator("role=link[name='Shop']");
        private ILocator _contactTab => _page.Locator("role=link[name='Contact']");        
        private ILocator _cart => _page.Locator("a[href='#/cart']:has(span.cart-count.ng-binding)");

        public HomePage(IPage page) 
        { 
            _page = page;            
        }

        public async Task<ShopPage> ClickShopTabAsync()
        {
            await _shopTab.ClickAsync();

            return new ShopPage(_page);
        }

        public async Task<ContactPage> ClickContactTabAsync()
        {
            await _contactTab.ClickAsync();

            return new ContactPage(_page);
        }

        public async Task<CartPage> ClickCartAsync()
        {
            await _cart.ClickAsync();

            return new CartPage(_page);
        }

        public async Task ClickHomeTabAsync()
        {
            await _homeTab.ClickAsync();            
        }
    }
}
    