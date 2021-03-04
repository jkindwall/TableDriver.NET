using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class NoHeaderInTBODYTableTests : NoHeaderTableTestsBase
    {
        private const string TableId = "no-headers-in-tbody";

        protected override Table GetTestTable()
        {
            return Table.CreateWithNoHeaders(this.Driver.FindElement(By.Id(NoHeaderInTBODYTableTests.TableId)), 0);
        }

        [TestMethod]
        public void NoHeaderInTBODYTablePropertiesTest()
        {
            this.TestTableProperties(NoHeaderInTBODYTableTests.TableId);
        }

        [TestMethod]
        public void NoHeaderInTBODYTableGetRowsTest()
        {
            this.TestGetRows();
        }

        [TestMethod]
        public void NoHeaderInTBODYTableFindRowTest()
        {
            this.TestFindRow();
        }

        [TestMethod]
        public void NoHeaderInTBODYTableFindRowsTest()
        {
            this.TestFindRows();
        }

        [TestMethod]
        public void NoHeaderInTBODYTableFindCellTest()
        {
            this.TestFindCell();
        }

        [TestMethod]
        public void NoHeaderInTBODYTableFindCellsTest()
        {
            this.TestFindCells();
        }

        [TestMethod]
        public void NoHeaderInTBODYTableGetHeaderTest()
        {
            this.TestGetHeader();
        }

        [TestMethod]
        public void NoHeaderInTBODYTableNotEqualInRowQueryTest()
        {
            this.TestNotEqualInRowQuery();
        }

        [TestMethod]
        public void NoHeaderInTBODYTableLessThanInRowQueryTest()
        {
            this.TestLessThanInRowQuery();
        }

        [TestMethod]
        public void NoHeaderInTBODYTableLessThanOrEqualInRowQueryTest()
        {
            this.TestLessThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void NoHeaderInTBODYTableGreaterThanInRowQueryTest()
        {
            this.TestGreaterThanInRowQuery();
        }

        [TestMethod]
        public void NoHeaderInTBODYTableGreaterThanOrEqualInRowQueryTest()
        {
            this.TestGreaterThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void NoHeaderInTBODYTableStartsWithInRowQueryTest()
        {
            this.TestStartsWithInRowQuery();
        }

        [TestMethod]
        public void NoHeaderInTBODYTableContainsInRowQueryTest()
        {
            this.TestContainsInRowQuery();
        }
    }
}
