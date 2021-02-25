using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class FirstRowHeaderInTBODYTableCellTests : TableCellTestsBase
    {
        private const string TableId = "first-row-headers-in-tbody";

        protected override TableCell GetTestTableCell()
        {
            Table table = Table.Create(this.Driver.FindElement(By.Id(FirstRowHeaderInTBODYTableCellTests.TableId)));
            return table.FindCell(5, 1);
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTableCellPropertiesTest()
        {
            this.TestTableCellProperties();
        }
    }
}
