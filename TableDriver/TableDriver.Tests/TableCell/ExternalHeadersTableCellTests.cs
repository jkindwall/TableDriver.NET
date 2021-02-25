using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class ExternalHeadersTableCellTests : TableCellTestsBase
    {
        private const string TableId = "with-external-headers";
        private const string HeaderCss = "#table-headers > span";

        protected override TableCell GetTestTableCell()
        {
            Table table = Table.CreateWithExternalHeaders(
                this.Driver.FindElement(By.Id(ExternalHeadersTableCellTests.TableId)),
                this.Driver.FindElements(By.CssSelector(ExternalHeadersTableCellTests.HeaderCss)),
                0);
            return table.FindCell(5, 1);
        }

        [TestMethod]
        public void CustomHeaderRowTableCellPropertiesTest()
        {
            this.TestTableCellProperties();
        }
    }
}
