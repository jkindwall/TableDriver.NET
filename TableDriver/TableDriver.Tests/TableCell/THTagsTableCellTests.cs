using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class THTagsTableCellTests : TableCellTestsBase
    {
        private const string TableId = "with-th-tags";

        protected override TableCell GetTestTableCell()
        {
            Table table = Table.Create(this.Driver.FindElement(By.Id(THTagsTableCellTests.TableId)));
            return table.FindCell(5, 1);
        }

        [TestMethod]
        public void THTagsTableCellPropertiesTest()
        {
            this.TestTableCellProperties();
        }
    }
}
