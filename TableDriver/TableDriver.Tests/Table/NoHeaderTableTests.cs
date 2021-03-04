using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class NoHeaderTableTests : NoHeaderTableTestsBase
    {
        private const string TableId = "no-headers";

        protected override Table GetTestTable()
        {
            return Table.CreateWithNoHeaders(this.Driver.FindElement(By.Id(NoHeaderTableTests.TableId)), 0);
        }

        [TestMethod]
        public void NoHeaderTablePropertiesTest()
        {
            this.TestTableProperties(NoHeaderTableTests.TableId);
        }

        [TestMethod]
        public void NoHeaderTableGetRowsTest()
        {
            this.TestGetRows();
        }

        [TestMethod]
        public void NoHeaderTableFindRowTest()
        {
            this.TestFindRow();
        }

        [TestMethod]
        public void NoHeaderTableFindRowsTest()
        {
            this.TestFindRows();
        }

        [TestMethod]
        public void NoHeaderTableFindCellTest()
        {
            this.TestFindCell();
        }

        [TestMethod]
        public void NoHeaderTableFindCellsTest()
        {
            this.TestFindCells();
        }

        [TestMethod]
        public void NoHeaderTableGetHeaderTest()
        {
            this.TestGetHeader();
        }

        [TestMethod]
        public void NoHeaderTableNotEqualInRowQueryTest()
        {
            this.TestNotEqualInRowQuery();
        }

        [TestMethod]
        public void NoHeaderTableLessThanInRowQueryTest()
        {
            this.TestLessThanInRowQuery();
        }

        [TestMethod]
        public void NoHeaderTableLessThanOrEqualInRowQueryTest()
        {
            this.TestLessThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void NoHeaderTableGreaterThanInRowQueryTest()
        {
            this.TestGreaterThanInRowQuery();
        }

        [TestMethod]
        public void NoHeaderTableGreaterThanOrEqualInRowQueryTest()
        {
            this.TestGreaterThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void NoHeaderTableStartsWithInRowQueryTest()
        {
            this.TestStartsWithInRowQuery();
        }

        [TestMethod]
        public void NoHeaderTableContainsInRowQueryTest()
        {
            this.TestContainsInRowQuery();
        }
    }
}
