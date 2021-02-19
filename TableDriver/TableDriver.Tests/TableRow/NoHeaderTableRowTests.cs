using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class NoHeaderTableRowTests : NoHeaderTableRowTestsBase
    {
        private const string TableId = "no-headers";

        protected override TableRow GetTestTableRow()
        {
            Table table = new Table(
                this.Driver.FindElement(By.Id(NoHeaderTableRowTests.TableId)),
                null,
                0);
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
