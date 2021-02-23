using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class ExternalHeadersInTBODYTableCellTests : TableCellTestsBase
    {
        private const string TableId = "with-tbody-and-external-headers";
        private const string HeaderCss = "#table-headers > span";

        protected override TableCell GetTestTableCell()
        {
            Table table = Table.CreateWithExternalHeaders(
                this.Driver.FindElement(By.Id(ExternalHeadersInTBODYTableCellTests.TableId)),
                this.Driver.FindElements(By.CssSelector(ExternalHeadersInTBODYTableCellTests.HeaderCss)),
                0);
            return table.FindCell(5, 1);
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableCellPropertiesTest()
        {
            this.TestTableCellProperties();
        }
    }
}
