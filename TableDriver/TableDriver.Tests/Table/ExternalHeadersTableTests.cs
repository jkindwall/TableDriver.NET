using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class ExternalHeadersTableTests : ExternalHeadersTableTestsBase
    {
        private const string TableId = "with-external-headers";
        private const string HeaderCss = "#table-headers > span";

        protected override Table GetTestTable()
        {
            return Table.CreateWithExternalHeaders(
                this.Driver.FindElement(By.Id(ExternalHeadersTableTests.TableId)),
                this.Driver.FindElements(By.CssSelector(ExternalHeadersTableTests.HeaderCss)),
                0);
        }

        [TestMethod]
        public void ExternalHeadersTablePropertiesTest()
        {
            this.TestTableProperties(ExternalHeadersTableTests.TableId);
        }

        [TestMethod]
        public void ExternalHeadersTableGetRowsTest()
        {
            this.TestGetRows();
        }

        [TestMethod]
        public void ExternalHeadersTableFindRowTest()
        {
            this.TestFindRow();
        }

        [TestMethod]
        public void ExternalHeadersTableFindRowsTest()
        {
            this.TestFindRows();
        }

        [TestMethod]
        public void ExternalHeadersTableFindCellTest()
        {
            this.TestFindCell();
        }

        [TestMethod]
        public void ExternalHeadersTableFindCellsTest()
        {
            this.TestFindCells();
        }

        [TestMethod]
        public void ExternalHeadersTableGetHeaderTest()
        {
            this.TestGetHeader();
        }

        [TestMethod]
        public void ExternalHeadersTableNotEqualInRowQueryTest()
        {
            this.TestNotEqualInRowQuery();
        }

        [TestMethod]
        public void ExternalHeadersTableLessThanInRowQueryTest()
        {
            this.TestLessThanInRowQuery();
        }

        [TestMethod]
        public void ExternalHeadersTableLessThanOrEqualInRowQueryTest()
        {
            this.TestLessThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void ExternalHeadersTableGreaterThanInRowQueryTest()
        {
            this.TestGreaterThanInRowQuery();
        }

        [TestMethod]
        public void ExternalHeadersTableGreaterThanOrEqualInRowQueryTest()
        {
            this.TestGreaterThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void ExternalHeadersTableStartsWithInRowQueryTest()
        {
            this.TestStartsWithInRowQuery();
        }

        [TestMethod]
        public void ExternalHeadersTableContainsInRowQueryTest()
        {
            this.TestContainsInRowQuery();
        }
    }
}
