using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class ExternalHeadersInTBODYTableRowTests : TableRowTestsBase
    {
        private const string TableId = "with-tbody-and-external-headers";
        private const string HeaderCss = "#table-headers > span";

        protected override TableRow GetTestTableRow()
        {
            Table table = Table.CreateWithExternalHeaders(
                this.Driver.FindElement(By.Id(ExternalHeadersInTBODYTableRowTests.TableId)),
                this.Driver.FindElements(By.CssSelector(ExternalHeadersInTBODYTableRowTests.HeaderCss)),
                0);
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
