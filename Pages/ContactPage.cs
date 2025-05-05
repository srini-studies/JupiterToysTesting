using Microsoft.Playwright;
using PlanitAutomation.Tests;
using System.Text.RegularExpressions;

namespace PlanitAutomation.Pages
{
    public class ContactPage : BaseTest
    {
        private readonly IPage _page;

        private ILocator _textboxForeName => _page.Locator("role=textbox[name='Forename *']");
        private ILocator _textboxSurname => _page.Locator("role=textbox[name='Surname']");
        private ILocator _textboxEmail => _page.Locator("role=textbox[name='Email *']");
        private ILocator _textboxTelephone => _page.Locator("role=textbox[name='Telephone']");
        private ILocator _textboxMessage => _page.Locator("role=textbox[name='Message *']");
        private ILocator _linkSubmit => _page.Locator("role=link[name='Submit']");
        private ILocator _divInfo => _page.Locator("div.alert.alert-info.ng-scope", new PageLocatorOptions { HasTextRegex = new Regex("We welcome your feedback.*", RegexOptions.IgnoreCase) });
        private ILocator _divError => _page.Locator("div.alert.alert-error.ng-scope", new PageLocatorOptions { HasTextRegex = new Regex("We welcome your feedback.*", RegexOptions.IgnoreCase)});
        private ILocator _divSuccess => _page.Locator("div.alert.alert-success", new PageLocatorOptions { HasTextRegex = new Regex("Thanks*", RegexOptions.IgnoreCase) });
        private ILocator _spanForenameError => _page.Locator("#forename-err");
        private ILocator _spanEmailError => _page.Locator("#email-err");
        private ILocator _spanMessageBoxError => _page.Locator("#message-err");

        public ContactPage(IPage page)
        {
            _page = page;
        }

        public async Task InitialiseFields()
        {
            await _textboxForeName.FillAsync("");
            await _textboxSurname.FillAsync("");
            await _textboxEmail.FillAsync("");
            await _textboxTelephone.FillAsync("");
            await _textboxMessage.FillAsync("");
        }

        public async Task ClickSubmitAsync()
        {
            await _linkSubmit.ClickAsync();
        }

        public ILocator GetInfoMessageLocator()
        {
            return _divInfo;
        }
        public ILocator GetErrorMessageLocator()
        {
            return _divError;
        }

        public ILocator GetSuccessMessageLocator()
        {
            return _divSuccess;
        }

        public ILocator GetForenameErrorLocator()
        {
            return _spanForenameError;
        }

        public ILocator GetEmailErrorLocator()
        {
            return _spanEmailError;
        }

        public ILocator GetMessageBoxErrorLocator()
        {
            return _spanMessageBoxError;
        }

        public ILocator GetForenameTextBoxLocator()
        {
            return _textboxForeName;            
        }

        public async Task PopulateMandatoryFieldsAsync()
        {
            await InitialiseFields();
            await _textboxForeName.FillAsync("John");
            await _textboxEmail.FillAsync("john.smith@email.com");
            await _textboxMessage.FillAsync("This is John Smith");
        }

        [SetUp]
        public override async Task Setup()
        {
            await base.Setup();
            await InitialiseFields();
        }

        [TearDown]
        public override async Task TearDown()
        {
            await base.TearDown();
            await InitialiseFields();
        }
    }       
}           
            