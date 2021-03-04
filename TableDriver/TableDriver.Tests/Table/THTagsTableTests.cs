using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class THTagsTableTests : TableTestsBase
    {
        private const string TableId = "with-th-tags";

        protected override Table GetTestTable()
        {
            return Table.Create(this.Driver.FindElement(By.Id(THTagsTableTests.TableId)));
        }

        [TestMethod]
        public void THTagsTablePropertiesTest()
        {
            this.TestTableProperties(THTagsTableTests.TableId);
        }

        [TestMethod]
        public void THTagsTableGetRowsTest()
        {
            this.TestGetRows();
        }

        [TestMethod]
        public void THTagsTableFindRowTest()
        {
            this.TestFindRow();
        }

        [TestMethod]
        public void THTagsTableFindRowsTest()
        {
            this.TestFindRows();
        }

        [TestMethod]
        public void THTagsTableFindCellTest()
        {
            this.TestFindCell();
        }

        [TestMethod]
        public void THTagsTableFindCellsTest()
        {
            this.TestFindCells();
        }

        [TestMethod]
        public void THTagsTableGetHeaderTest()
        {
            this.TestGetHeader();
        }

        [TestMethod]
        public void THTagsTableNotEqualInRowQueryTest()
        {
            this.TestNotEqualInRowQuery();
        }

        [TestMethod]
        public void THTagsTableLessThanInRowQueryTest()
        {
            this.TestLessThanInRowQuery();
        }

        [TestMethod]
        public void THTagsTableLessThanOrEqualInRowQueryTest()
        {
            this.TestLessThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void THTagsTableGreaterThanInRowQueryTest()
        {
            this.TestGreaterThanInRowQuery();
        }

        [TestMethod]
        public void THTagsTableGreaterThanOrEqualInRowQueryTest()
        {
            this.TestGreaterThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void THTagsTableStartsWithInRowQueryTest()
        {
            this.TestStartsWithInRowQuery();
        }

        [TestMethod]
        public void THTagsTableContainsInRowQueryTest()
        {
            this.TestContainsInRowQuery();
        }
    }
}
