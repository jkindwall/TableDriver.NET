using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class THEADTableTests : TableTestsBase
    {
        private const string TableId = "with-thead-tbody";

        protected override Table GetTestTable()
        {
            return new Table(this.Driver.FindElement(By.Id(THEADTableTests.TableId)));
        }

        [TestMethod]
        public void THEADTablePropertiesTest()
        {
            this.TestTableProperties(THEADTableTests.TableId);
        }

        [TestMethod]
        public void THEADTableGetRowsTest()
        {
            this.TestGetRows();
        }

        [TestMethod]
        public void THEADTableFindRowTest()
        {
            this.TestFindRow();
        }

        [TestMethod]
        public void THEADTableFindRowsTest()
        {
            this.TestFindRows();
        }

        [TestMethod]
        public void THEADTableFindCellTest()
        {
            this.TestFindCell();
        }

        [TestMethod]
        public void THEADTableFindCellsTest()
        {
            this.TestFindCells();
        }

        [TestMethod]
        public void THEADTableGetHeaderTest()
        {
            this.TestGetHeader();
        }
    }
}
