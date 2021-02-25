using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class FirstRowHeaderInTBODYTableRowTests : TableRowTestsBase
    {
        private const string TableId = "first-row-headers-in-tbody";

        protected override TableRow GetTestTableRow()
        {
            Table table = Table.Create(this.Driver.FindElement(By.Id(FirstRowHeaderInTBODYTableRowTests.TableId)));
            return table.FindRow(10);
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTableRowPropertiesTest()
        {
            this.TestTableRowProperties();
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTableRowGetCellsTest()
        {
            this.TestGetCells();
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTableRowFindCellTest()
        {
            this.TestFindCell();
        }
    }
}
