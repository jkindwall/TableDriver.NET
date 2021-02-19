using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class FirstRowHeaderTableCellTests : TableCellTestsBase
    {
        private const string TableId = "first-row-headers";

        protected override TableCell GetTestTableCell()
        {
            Table table = new Table(this.Driver.FindElement(By.Id(FirstRowHeaderTableCellTests.TableId)));
            return table.FindCell(5, 1);
        }

        [TestMethod]
        public void FirstRowHeaderTableCellPropertiesTest()
        {
            this.TestTableCellProperties();
        }
    }
}
