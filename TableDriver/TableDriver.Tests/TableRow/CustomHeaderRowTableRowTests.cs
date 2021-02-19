using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class CustomHeaderRowTableRowTests : TableRowTestsBase
    {
        private const string TableId = "custom-header-row";
        private const string HeaderCss = "#custom-header-row tr[name=headerRow]";

        protected override TableRow GetTestTableRow()
        {
            Table table = new Table(
                this.Driver.FindElement(By.Id(CustomHeaderRowTableRowTests.TableId)),
                this.Driver.FindElement(By.CssSelector(CustomHeaderRowTableRowTests.HeaderCss)),
                2);
            return table.FindRow(10);
        }

        [TestMethod]
        public void CustomHeaderRowTableRowPropertiesTest()
        {
            this.TestTableRowProperties();
        }

        [TestMethod]
        public void CustomHeaderRowTableRowGetCellsTest()
        {
            this.TestGetCells();
        }

        [TestMethod]
        public void CustomHeaderRowTableRowFindCellTest()
        {
            this.TestFindCell();
        }
    }
}
