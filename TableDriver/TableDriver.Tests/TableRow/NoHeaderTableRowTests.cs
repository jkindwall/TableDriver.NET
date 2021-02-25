using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class NoHeaderTableRowTests : NoHeaderTableRowTestsBase
    {
        private const string TableId = "no-headers";

        protected override TableRow GetTestTableRow()
        {
            Table table = Table.CreateWithNoHeaders(this.Driver.FindElement(By.Id(NoHeaderTableRowTests.TableId)), 0);
            return table.FindRow(10);
        }

        [TestMethod]
        public void NoHeaderTableRowPropertiesTest()
        {
            this.TestTableRowProperties();
        }

        [TestMethod]
        public void NoHeaderTableRowGetCellsTest()
        {
            this.TestGetCells();
        }

        [TestMethod]
        public void NoHeaderTableRowFindCellTest()
        {
            this.TestFindCell();
        }
    }
}
