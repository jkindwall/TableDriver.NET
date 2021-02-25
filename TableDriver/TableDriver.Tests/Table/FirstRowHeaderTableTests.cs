using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class FirstRowHeaderTableTests : TableTestsBase
    {
        private const string TableId = "first-row-headers";

        protected override Table GetTestTable()
        {
            return Table.Create(this.Driver.FindElement(By.Id(FirstRowHeaderTableTests.TableId)));
        }

        [TestMethod]
        public void FirstRowHeaderTablePropertiesTest()
        {
            this.TestTableProperties(FirstRowHeaderTableTests.TableId);
        }

        [TestMethod]
        public void FirstRowHeaderTableGetRowsTest()
        {
            this.TestGetRows();
        }

        [TestMethod]
        public void FirstRowHeaderTableFindRowTest()
        {
            this.TestFindRow();
        }

        [TestMethod]
        public void FirstRowHeaderTableFindRowsTest()
        {
            this.TestFindRows();
        }

        [TestMethod]
        public void FirstRowHeaderTableFindCellTest()
        {
            this.TestFindCell();
        }

        [TestMethod]
        public void FirstRowHeaderTableFindCellsTest()
        {
            this.TestFindCells();
        }

        [TestMethod]
        public void FirstRowHeaderTableGetHeaderTest()
        {
            this.TestGetHeader();
        }
    }
}
