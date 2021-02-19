using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class THEADTableCellTests : TableCellTestsBase
    {
        private const string TableId = "with-thead-tbody";

        protected override TableCell GetTestTableCell()
        {
            Table table = new Table(this.Driver.FindElement(By.Id(THEADTableCellTests.TableId)));
            return table.FindCell(5, 1);
        }

        [TestMethod]
        public void THEADTableCellPropertiesTest()
        {
            this.TestTableCellProperties();
        }
    }
}
