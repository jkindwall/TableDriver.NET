using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class ExternalHeadersInTBODYTableTests : ExternalHeadersTableTestsBase
    {
        private const string TableId = "with-tbody-and-external-headers";
        private const string HeaderCss = "#table-headers > span";

        protected override Table GetTestTable()
        {
            return Table.CreateWithExternalHeaders(
                this.Driver.FindElement(By.Id(ExternalHeadersInTBODYTableTests.TableId)),
                this.Driver.FindElements(By.CssSelector(ExternalHeadersInTBODYTableTests.HeaderCss)),
                0);
        }

        [TestMethod]
        public void ExternalHeadersRowInTBODYTablePropertiesTest()
        {
            this.TestTableProperties(ExternalHeadersInTBODYTableTests.TableId);
        }

        [TestMethod]
        public void ExternalHeadersInTBODYTableGetRowsTest()
        {
            this.TestGetRows();
        }

        [TestMethod]
        public void ExternalHeadersInTBODYTableFindRowTest()
        {
            this.TestFindRow();
        }

        [TestMethod]
        public void ExternalHeadersInTBODYTableFindRowsTest()
        {
            this.TestFindRows();
        }

        [TestMethod]
        public void ExternalHeadersInTBODYTableFindCellTest()
        {
            this.TestFindCell();
        }

        [TestMethod]
        public void ExternalHeadersInTBODYTableFindCellsTest()
        {
            this.TestFindCells();
        }

        [TestMethod]
        public void ExternalHeadersInTBODYTableGetHeaderTest()
        {
            this.TestGetHeader();
        }

        [TestMethod]
        public void ExternalHeadersInTBODYTableNotEqualInRowQueryTest()
        {
            this.TestNotEqualInRowQuery();
        }

        [TestMethod]
        public void ExternalHeadersInTBODYTableLessThanInRowQueryTest()
        {
            this.TestLessThanInRowQuery();
        }

        [TestMethod]
        public void ExternalHeadersInTBODYTableLessThanOrEqualInRowQueryTest()
        {
            this.TestLessThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void ExternalHeadersInTBODYTableGreaterThanInRowQueryTest()
        {
            this.TestGreaterThanInRowQuery();
        }

        [TestMethod]
        public void ExternalHeadersInTBODYTableGreaterThanOrEqualInRowQueryTest()
        {
            this.TestGreaterThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void ExternalHeadersInTBODYTableStartsWithInRowQueryTest()
        {
            this.TestStartsWithInRowQuery();
        }

        [TestMethod]
        public void ExternalHeadersInTBODYTableContainsInRowQueryTest()
        {
            this.TestContainsInRowQuery();
        }
    }
}
