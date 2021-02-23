using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class NoHeaderTableCellTests : TableCellTestsBase
    {
        private const string TableId = "no-headers-in-tbody";

        protected override TableCell GetTestTableCell()
        {
            Table table = Table.CreateWithNoHeaders(this.Driver.FindElement(By.Id(NoHeaderTableCellTests.TableId)), 0);
            return table.FindCell(5, 1);
        }

        [TestMethod]
        public void NoHeaderTableCellPropertiesTest()
        {
            this.TestTableCellProperties();
        }
    }
}
