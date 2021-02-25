using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class NoHeaderInTBODYTableCellTests : TableCellTestsBase
    {
        private const string TableId = "no-headers-in-tbody";

        protected override TableCell GetTestTableCell()
        {
            Table table = Table.CreateWithNoHeaders(this.Driver.FindElement(By.Id(NoHeaderInTBODYTableCellTests.TableId)), 0);
            return table.FindCell(5, 1);
        }

        [TestMethod]
        public void NoHeaderInTBODYTableCellPropertiesTest()
        {
            this.TestTableCellProperties();
        }
    }
}
