using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;

namespace TableDriver.Tests
{
    public abstract class WebDriverTestsBase
    {
        protected IWebDriver Driver { get; private set; }

        [TestInitialize]
        public void InitWebDriver()
        {
            string browser = Environment.GetEnvironmentVariable("TEST_BROWSER")?.ToUpper() ?? Browser.ChromeBrowser;
            switch (browser)
            {
                case Browser.EdgeBrowser:
                    EdgeOptions edgeOptions = new EdgeOptions
                    {
                        PageLoadStrategy = PageLoadStrategy.Normal,
                        UseChromium = true
                    };
                    this.Driver = new EdgeDriver(edgeOptions);
                    break;

                case Browser.ChromeBrowser:
                    ChromeOptions chromeOptions = new ChromeOptions
                    {
                        PageLoadStrategy = PageLoadStrategy.Normal
                    };
                    this.Driver = new ChromeDriver(chromeOptions);
                    break;
                case Browser.FirefoxBrowser:
                    FirefoxOptions firefoxOptions = new FirefoxOptions
                    {
                        PageLoadStrategy = PageLoadStrategy.Normal
                    };
                    this.Driver = new FirefoxDriver(firefoxOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid browser type specified: '{browser}'");
            }

            this.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(200);
        }

        [TestCleanup]
        public void DriverShutdown()
        {
            this.Driver.Quit();
        }
    }
}
