using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class FirstRowHeaderInTBODYTableTests : TableTestsBase
    {
        private const string TableId = "first-row-headers-in-tbody";

        protected override Table GetTestTable()
        {
            return Table.Create(this.Driver.FindElement(By.Id(FirstRowHeaderInTBODYTableTests.TableId)));
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTablePropertiesTest()
        {
            this.TestTableProperties(FirstRowHeaderInTBODYTableTests.TableId);
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTableGetRowsTest()
        {
            this.TestGetRows();
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTableFindRowTest()
        {
            this.TestFindRow();
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTableFindRowsTest()
        {
            this.TestFindRows();
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTableFindCellTest()
        {
            this.TestFindCell();
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTableFindCellsTest()
        {
            this.TestFindCells();
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTableGetHeaderTest()
        {
            this.TestGetHeader();
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTableNotEqualInRowQueryTest()
        {
            this.TestNotEqualInRowQuery();
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTableLessThanInRowQueryTest()
        {
            this.TestLessThanInRowQuery();
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTableLessThanOrEqualInRowQueryTest()
        {
            this.TestLessThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTableGreaterThanInRowQueryTest()
        {
            this.TestGreaterThanInRowQuery();
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTableGreaterThanOrEqualInRowQueryTest()
        {
            this.TestGreaterThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTableStartsWithInRowQueryTest()
        {
            this.TestStartsWithInRowQuery();
        }

        [TestMethod]
        public void FirstRowHeaderInTBODYTableContainsInRowQueryTest()
        {
            this.TestContainsInRowQuery();
        }
    }
}
