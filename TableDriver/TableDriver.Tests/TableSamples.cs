using OpenQA.Selenium;
using System.IO;
using System.Reflection;

namespace TableDriver.Tests
{
    public static class TableSamples
    {
        public const string TestPage = "TableSamples.html";

        private static object testPageUriLock = new object();
        private static string testPageUri;
        public static string TestPageUri
        {
            get
            {
                if (TableSamples.testPageUri == null)
                {
                    lock (TableSamples.testPageUriLock)
                    {
                        if (TableSamples.testPageUri == null)
                        {
                            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                            string testPagePath =
                                Path.Combine(directory, TableSamples.TestPage).Replace(Path.DirectorySeparatorChar, '/');
                            TableSamples.testPageUri = $"file:///{testPagePath}";
                        }
                    }
                }

                return TableSamples.testPageUri;
            }
        }

        public static void GoToTestPage(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(TableSamples.TestPageUri);
        }
    }
}
