using Microsoft.Playwright;
using PlanitAutomation.Tests;

namespace PlanitAutomation.Pages
{
    public class CartPage: BaseTest
    {
        private readonly IPage _page;

        public CartPage(IPage page)
        {
            _page = page;
        }

        public ILocator GetStuffedFrogSubTotalLocator()
        {
            // Find the row containing "Stuffed Frog"
            var frogRow = _page.Locator("tr:has(td:has-text(\"Stuffed Frog\"))");

            // From that row, get the 4th td
            var subtotalStuffedFrog = frogRow.Locator("td:nth-of-type(4)");

            return subtotalStuffedFrog;
        }

        public ILocator GetFluffyBunnySubTotalLocator()
        {
            // Find the row containing "Fluffy Bunny"
            var bunnyRow = _page.Locator("tr:has(td:has-text(\"Fluffy Bunny\"))");

            // From that row, get the 4th td
            var subtotalFluffyBunny = bunnyRow.Locator("td:nth-of-type(4)");

            return subtotalFluffyBunny;
        }

        public ILocator GetValentineBearSubTotalLocator()
        {
            // Find the row containing "Valentine Bear"
            var bearRow = _page.Locator("tr:has(td:has-text(\"Valentine Bear\"))");

            // From that row, get the 4th td
            var subtotalValentineBear = bearRow.Locator("td:nth-of-type(4)");

            return subtotalValentineBear;
        }

        public ILocator GetStuffedFrogPriceLocator()
        {
            // Find the row containing "Stuffed Frog"
            var frogRow = _page.Locator("tr:has(td:has-text(\"Stuffed Frog\"))");

            // From that row, get the 2nd td
            var forgPrice = frogRow.Locator("td:nth-of-type(2)");

            return forgPrice;
        }

        public ILocator GetFluffyBunnyPriceLocator()
        {
            // Find the row containing "Fluffy Bunny"
            var bunnyRow = _page.Locator("tr:has(td:has-text(\"Fluffy Bunny\"))");

            // From that row, get the 2nd td
            var bunnyPrice = bunnyRow.Locator("td:nth-of-type(2)");

            return bunnyPrice;
        }

        public ILocator GetValentineBearPriceLocator()
        {
            // Find the row containing "Valentine Bear"
            var bearRow = _page.Locator("tr:has(td:has-text(\"Valentine Bear\"))");

            // From that row, get the 2nd td
            var bearPrice = bearRow.Locator("td:nth-of-type(2)");

            return bearPrice;
        }

        public ILocator GetTotalLocator()
        {
            var totalLocator = _page.Locator("strong.total");

            return totalLocator;
        }

    }
}
