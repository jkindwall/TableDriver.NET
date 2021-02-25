using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class CustomHeaderRowInTBODYTableCellTests : TableCellTestsBase
    {
        private const string TableId = "custom-header-row-in-tbody";
        private const string HeaderCss = "#custom-header-row-in-tbody tr[name=headerRow]";

        protected override TableCell GetTestTableCell()
        {
            Table table = Table.CreateWithHeaderRow(
                this.Driver.FindElement(By.Id(CustomHeaderRowInTBODYTableCellTests.TableId)),
                this.Driver.FindElement(By.CssSelector(CustomHeaderRowInTBODYTableCellTests.HeaderCss)),
                2);
            return table.FindCell(5, 1);
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableCellPropertiesTest()
        {
            this.TestTableCellProperties();
        }
    }
}
