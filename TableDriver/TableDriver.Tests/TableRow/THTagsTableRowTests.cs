using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class THTagsTableRowTests : TableRowTestsBase
    {
        private const string TableId = "with-th-tags";

        protected override TableRow GetTestTableRow()
        {
            Table table = Table.Create(this.Driver.FindElement(By.Id(THTagsTableRowTests.TableId)));
            return table.FindRow(10);
        }

        [TestMethod]
        public void THTagsTableRowPropertiesTest()
        {
            this.TestTableRowProperties();
        }

        [TestMethod]
        public void THTagsTableRowGetCellsTest()
        {
            this.TestGetCells();
        }

        [TestMethod]
        public void THTagsTableRowFindCellTest()
        {
            this.TestFindCell();
        }
    }
}
