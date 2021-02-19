using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class CustomHeaderRowInTBODYTableRowTests : TableRowTestsBase
    {
        private const string TableId = "custom-header-row-in-tbody";
        private const string HeaderCss = "#custom-header-row-in-tbody tr[name=headerRow]";

        protected override TableRow GetTestTableRow()
        {
            Table table = new Table(
                this.Driver.FindElement(By.Id(CustomHeaderRowInTBODYTableRowTests.TableId)),
                this.Driver.FindElement(By.CssSelector(CustomHeaderRowInTBODYTableRowTests.HeaderCss)),
                2);
            return table.FindRow(10);
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableRowPropertiesTest()
        {
            this.TestTableRowProperties();
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableRowGetCellsTest()
        {
            this.TestGetCells();
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableRowFindCellTest()
        {
            this.TestFindCell();
        }
    }
}
