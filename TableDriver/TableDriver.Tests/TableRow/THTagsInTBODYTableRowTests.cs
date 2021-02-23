using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class THTagsInTBODYTableRowTests : TableRowTestsBase
    {
        private const string TableId = "with-th-tags-in-tbody";

        protected override TableRow GetTestTableRow()
        {
            Table table = Table.Create(this.Driver.FindElement(By.Id(THTagsInTBODYTableRowTests.TableId)));
            return table.FindRow(10);
        }

        [TestMethod]
        public void THTagsInTBODYTableRowPropertiesTest()
        {
            this.TestTableRowProperties();
        }

        [TestMethod]
        public void THTagsInTBODYTableRowGetCellsTest()
        {
            this.TestGetCells();
        }

        [TestMethod]
        public void THTagsInTBODYTableRowFindCellTest()
        {
            this.TestFindCell();
        }
    }
}
