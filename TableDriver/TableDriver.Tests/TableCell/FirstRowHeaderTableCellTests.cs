using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class FirstRowHeaderTableCellTests : TableCellTestsBase
    {
        private const string TableId = "first-row-headers";

        protected override TableCell GetTestTableCell()
        {
            Table table = Table.Create(this.Driver.FindElement(By.Id(FirstRowHeaderTableCellTests.TableId)));
            return table.FindCell(5, 1);
        }

        [TestMethod]
        public void FirstRowHeaderTableCellPropertiesTest()
        {
            this.TestTableCellProperties();
        }
    }
}
