using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using static System.Net.WebRequestMethods;

namespace PlanitAutomation.Tests
{
    public class BaseTest: PageTest
    {
        public string BaseUrl { get; set; }
        public int NumOfStuffedFrog { get; set; }
        public int NumOfFluffyBunny { get; set; }
        public int NumOfValentineBear { get; set; }

        private readonly IConfiguration _configuration;

        public BaseTest()
        {            
            // Set up the configuration from appsettings.json file
            _configuration = new ConfigurationBuilder()               
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Bind the configuration to the TestConfiguration class
            var testConfig = _configuration.GetSection("PlaywrightConfig").Get<TestConfiguration>();

            BaseUrl = testConfig?.BaseUrl ?? "https://jupiter.cloud.planittesting.com/#/";
            NumOfStuffedFrog = int.Parse(testConfig?.NumberOfStuffedFrog ?? "2");
            NumOfFluffyBunny = int.Parse(testConfig?.NumberOfFluffyBunny ?? "5");
            NumOfValentineBear = int.Parse(testConfig?.NumberOfValentineBear ?? "3");
        }

        [SetUp]
        public virtual async Task Setup()
        {
            // setup trace
            await Context.Tracing.StartAsync(new()
            {
                Title = TestContext.CurrentContext.Test.ClassName + "." + TestContext.CurrentContext.Test.Name,
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }

        [TearDown]
        public virtual async Task TearDown()
        {
            // produce trace
            await Context.Tracing.StopAsync(new()
            {
                Path = Path.Combine(
                    TestContext.CurrentContext.WorkDirectory,
                    "playwright-traces",
                    $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
                )
            });
        }
    }
}
