using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class THEADTableTests : TableTestsBase
    {
        private const string TableId = "with-thead-tbody";

        protected override Table GetTestTable()
        {
            return Table.Create(this.Driver.FindElement(By.Id(THEADTableTests.TableId)));
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

        [TestMethod]
        public void THEADTableNotEqualInRowQueryTest()
        {
            this.TestNotEqualInRowQuery();
        }

        [TestMethod]
        public void THEADTableLessThanInRowQueryTest()
        {
            this.TestLessThanInRowQuery();
        }

        [TestMethod]
        public void THEADTableLessThanOrEqualInRowQueryTest()
        {
            this.TestLessThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void THEADTableGreaterThanInRowQueryTest()
        {
            this.TestGreaterThanInRowQuery();
        }

        [TestMethod]
        public void THEADTableGreaterThanOrEqualInRowQueryTest()
        {
            this.TestGreaterThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void THEADTableStartsWithInRowQueryTest()
        {
            this.TestStartsWithInRowQuery();
        }

        [TestMethod]
        public void THEADTableContainsInRowQueryTest()
        {
            this.TestContainsInRowQuery();
        }
    }
}
