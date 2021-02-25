using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class ExternalHeadersTableRowTests : TableRowTestsBase
    {
        private const string TableId = "with-external-headers";
        private const string HeaderCss = "#table-headers > span";

        protected override TableRow GetTestTableRow()
        {
            Table table = Table.CreateWithExternalHeaders(
                this.Driver.FindElement(By.Id(ExternalHeadersTableRowTests.TableId)),
                this.Driver.FindElements(By.CssSelector(ExternalHeadersTableRowTests.HeaderCss)),
                0);
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
