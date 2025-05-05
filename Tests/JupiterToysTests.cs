using Microsoft.Playwright;
using PlanitAutomation.Data;
using PlanitAutomation.Model;
using PlanitAutomation.Pages;
using System.Globalization;

namespace PlanitAutomation.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class JupiterToysTests: BaseTest
    {
        Toy toyStuffedFrog = new Toy();
        Toy toyFluffyBunny = new Toy();
        Toy toyValentineBear = new Toy();

        [SetUp]
        public override async Task Setup()
        {
            await base.Setup();

            await Page.GotoAsync(BaseUrl);            
        }

        [Test]
        public async Task TestCase_1_Contact_Submit_Async()
        {
            TestContext.WriteLine(TestContext.CurrentContext.Test.Name);

            var homePage = new HomePage(Page);

            // click contact tab and navigate to contact page
            var contactPage = await homePage.ClickContactTabAsync();                        

            await contactPage.ClickSubmitAsync();            

            await AssertErrors(contactPage);
                        
            await contactPage.PopulateMandatoryFieldsAsync();            

            await ValidateNoErrors(contactPage);            
        }

        [Test]
        [Repeat(5)]
        public async Task TestCase_2_Contact_Submit_With_Values_Async()
        {            
            int iteration = TestContext.CurrentContext.CurrentRepeatCount;            
            TestContext.WriteLine($"{TestContext.CurrentContext.Test.Name} {iteration}");

            var homePage = new HomePage(Page);

            // click contact tab and navigate to contact page
            var contactPage = await homePage.ClickContactTabAsync();

            await contactPage.PopulateMandatoryFieldsAsync();

            await ValidateNoErrors(contactPage);            

            string forename = await contactPage.GetForenameTextBoxLocator().InputValueAsync();

            await contactPage.ClickSubmitAsync();

            await AssertSuccessMessage(contactPage, forename);
        }

        [Test]        
        public async Task TestCase_3_Shopping_Cart_Async()
        {
            TestContext.WriteLine(TestContext.CurrentContext.Test.Name);

            var homePage = new HomePage(Page);

            // go to shop page
            var shopPage = await homePage.ClickShopTabAsync();

            // get toy's prices
            var stuffedFrogPrice = shopPage.GetStuffedFrogPrice();
            var fluffyBunnyPrice = shopPage.GetFluffyBynnyPrice();
            var valentineBearPrice = shopPage.GetValentineBearPrice();

            WritePriceInfo(stuffedFrogPrice, fluffyBunnyPrice, valentineBearPrice);

            // click and add to cart
            await shopPage.ClickStuffedFrogMultipleTimesAsync(NumOfStuffedFrog);
            await shopPage.ClickFluffyBunnyMultipleTimesAsync(NumOfFluffyBunny);
            await shopPage.ClickValentineBearMultipleTimesAsync(NumOfValentineBear);

            // save shop page info to Toy objects
            Populate(toyStuffedFrog, "StuffedFrog", NumOfStuffedFrog, stuffedFrogPrice.Result);
            Populate(toyFluffyBunny, "FluffyBunny", NumOfFluffyBunny, fluffyBunnyPrice.Result);
            Populate(toyValentineBear, "ValentineBear", NumOfValentineBear, valentineBearPrice.Result);

            WriteSubtotalInfo();

            // go to Cart Page
            // Validate Sub Totals, Price and Total in Cart Page
            var cartPage = await homePage.ClickCartAsync();

            // Sub Totals
            // stuffed frog
            var subtotalStuffedFrog = cartPage.GetStuffedFrogSubTotalLocator();
            await Expect(subtotalStuffedFrog).ToHaveTextAsync(toyStuffedFrog.SubTotal.ToString("C", CultureInfo.GetCultureInfo("en-US")));
            // fluffy bunny
            var subtotalFluffyBunny = cartPage.GetFluffyBunnySubTotalLocator();
            await Expect(subtotalFluffyBunny).ToHaveTextAsync(toyFluffyBunny.SubTotal.ToString("C", CultureInfo.GetCultureInfo("en-US")));
            // valentine bear
            var subtotalValentineBear = cartPage.GetValentineBearSubTotalLocator();
            await Expect(subtotalValentineBear).ToHaveTextAsync(toyValentineBear.SubTotal.ToString("C", CultureInfo.GetCultureInfo("en-US")));

            // Price
            var stuffedfrogPrice = cartPage.GetStuffedFrogPriceLocator();
            await Expect(stuffedfrogPrice).ToHaveTextAsync(toyStuffedFrog.Price.ToString("C", CultureInfo.GetCultureInfo("en-US")));
            var fluffybunnyPrice = cartPage.GetFluffyBunnyPriceLocator();
            await Expect(fluffybunnyPrice).ToHaveTextAsync(toyFluffyBunny.Price.ToString("C", CultureInfo.GetCultureInfo("en-US")));
            var valentinebearPrice = cartPage.GetValentineBearPriceLocator();
            await Expect(valentinebearPrice).ToHaveTextAsync(toyValentineBear.Price.ToString("C", CultureInfo.GetCultureInfo("en-US")));

            // Total
            var total = String.Format("Total: {0}", (toyStuffedFrog.SubTotal + toyFluffyBunny.SubTotal + toyValentineBear.SubTotal).ToString("0.0"));
            var totalLocator = cartPage.GetTotalLocator();
            await Expect(totalLocator).ToHaveTextAsync(total.ToString());
        }

        private void WriteSubtotalInfo()
        {
            TestContext.WriteLine(Environment.NewLine);
            TestContext.WriteLine($"StuffedFrog Subtotal: {toyStuffedFrog.Price * toyStuffedFrog.Quantity}");
            TestContext.WriteLine($"FluffyBunny Subtotal: {toyFluffyBunny.Price * toyFluffyBunny.Quantity}");
            TestContext.WriteLine($"ValentineBear Subtotal: {toyValentineBear.Price * toyValentineBear.Quantity}");            
        }

        private static void WritePriceInfo(Task<string> stuffedFrogPrice, Task<string> fluffyBunnyPrice, Task<string> valentineBearPrice)
        {
            TestContext.WriteLine(Environment.NewLine);
            TestContext.WriteLine($"Stuffed Frog: {stuffedFrogPrice.Result}");
            TestContext.WriteLine($"Fluffy Bunny : {fluffyBunnyPrice.Result}");
            TestContext.WriteLine($"Valentine Bear: {valentineBearPrice.Result}");            
        }

        private void Populate(Toy toy, string name, int num, string price)
        {            
            toy.Name = name;
            toy.Quantity = num;
            toy.Price = decimal.Parse(price, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"));
            toy.SubTotal = toy.Quantity * toy.Price;
        }

        private async Task AssertSuccessMessage(ContactPage contactPage, string forename)
        {
            var successMessageLocator = contactPage.GetSuccessMessageLocator();            
            string expectedFeedbackMessage = String.Format(Values.FeedbackMessage, forename);

            await Expect(successMessageLocator).ToHaveTextAsync(expectedFeedbackMessage, new LocatorAssertionsToHaveTextOptions
            {
                Timeout = 20000 // in milliseconds
            });
        }

        private async Task AssertErrors(ContactPage contactPage)
        {            
            var errorMessgeLocator = contactPage.GetErrorMessageLocator();
            await Expect(errorMessgeLocator).ToHaveTextAsync(Values.ErrorMessage);
            

            var forenameErrorLocator = contactPage.GetForenameErrorLocator();
            await Expect(forenameErrorLocator).ToHaveTextAsync(Values.ForeNameRequired);

            var emailErrorLocator = contactPage.GetEmailErrorLocator();
            await Expect(emailErrorLocator).ToHaveTextAsync(Values.EmailRequired);

            var messageBoxErrorLocator = contactPage.GetMessageBoxErrorLocator();
            await Expect(messageBoxErrorLocator).ToHaveTextAsync(Values.MessageRequired);
        }

        private async Task ValidateNoErrors(ContactPage contactPage)
        {            
            var infoMessgeLocator = contactPage.GetInfoMessageLocator();            
            await Expect(infoMessgeLocator).ToHaveTextAsync(Values.WelcomeMessage);
            

            var forenameErrorLocator = contactPage.GetForenameErrorLocator();
            await Expect(forenameErrorLocator).ToBeHiddenAsync();

            var emailErrorLocator = contactPage.GetEmailErrorLocator();
            await Expect(emailErrorLocator).ToBeHiddenAsync();

            var messageBoxErrorLocator = contactPage.GetMessageBoxErrorLocator();
            await Expect(messageBoxErrorLocator).ToBeHiddenAsync();
        }
    }
}
    