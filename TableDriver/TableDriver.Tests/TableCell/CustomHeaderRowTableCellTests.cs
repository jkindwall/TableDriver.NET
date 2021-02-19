using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class CustomHeaderRowTableCellTests : TableCellTestsBase
    {
        private const string TableId = "custom-header-row";
        private const string HeaderCss = "#custom-header-row tr[name=headerRow]";

        protected override TableCell GetTestTableCell()
        {
            Table table = new Table(
                this.Driver.FindElement(By.Id(CustomHeaderRowTableCellTests.TableId)),
                this.Driver.FindElement(By.CssSelector(CustomHeaderRowTableCellTests.HeaderCss)),
                2);
            return table.FindCell(5, 1);
        }

        [TestMethod]
        public void CustomHeaderRowTableCellPropertiesTest()
        {
            this.TestTableCellProperties();
        }
    }
}
