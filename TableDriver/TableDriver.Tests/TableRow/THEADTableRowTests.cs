using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class THEADTableRowTests : TableRowTestsBase
    {
        private const string TableId = "with-thead-tbody";

        protected override TableRow GetTestTableRow()
        {
            Table table = Table.Create(this.Driver.FindElement(By.Id(THEADTableRowTests.TableId)));
            return table.FindRow(10);
        }

        [TestMethod]
        public void THEADTableRowPropertiesTest()
        {
            this.TestTableRowProperties();
        }

        [TestMethod]
        public void THEADTableRowGetCellsTest()
        {
            this.TestGetCells();
        }

        [TestMethod]
        public void THEADTableRowFindCellTest()
        {
            this.TestFindCell();
        }
    }
}
