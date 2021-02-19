using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class NoHeaderInTBODYTableRowTests : NoHeaderTableRowTestsBase
    {
        private const string TableId = "no-headers-in-tbody";

        protected override TableRow GetTestTableRow()
        {
            Table table = new Table(
                this.Driver.FindElement(By.Id(NoHeaderInTBODYTableRowTests.TableId)),
                null,
                0);
            return table.FindRow(10);
        }

        [TestMethod]
        public void NoHeaderInTBODYTableRowPropertiesTest()
        {
            this.TestTableRowProperties();
        }

        [TestMethod]
        public void NoHeaderInTBODYTableRowGetCellsTest()
        {
            this.TestGetCells();
        }

        [TestMethod]
        public void NoHeaderInTBODYTableRowFindCellTest()
        {
            this.TestFindCell();
        }
    }
}
