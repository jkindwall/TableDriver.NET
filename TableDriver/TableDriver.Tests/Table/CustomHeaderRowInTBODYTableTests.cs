using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class CustomHeaderRowInTBODYTableTests : TableTestsBase
    {
        private const string TableId = "custom-header-row-in-tbody";
        private const string HeaderCss = "#custom-header-row-in-tbody tr[name=headerRow]";

        protected override Table GetTestTable()
        {
            return Table.CreateWithHeaderRow(
                this.Driver.FindElement(By.Id(CustomHeaderRowInTBODYTableTests.TableId)),
                this.Driver.FindElement(By.CssSelector(CustomHeaderRowInTBODYTableTests.HeaderCss)),
                2);
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTablePropertiesTest()
        {
            this.TestTableProperties(CustomHeaderRowInTBODYTableTests.TableId);
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableGetRowsTest()
        {
            this.TestGetRows();
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableFindRowTest()
        {
            this.TestFindRow();
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableFindRowsTest()
        {
            this.TestFindRows();
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableFindCellTest()
        {
            this.TestFindCell();
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableFindCellsTest()
        {
            this.TestFindCells();
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableGetHeaderTest()
        {
            this.TestGetHeader();
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableNotEqualInRowQueryTest()
        {
            this.TestNotEqualInRowQuery();
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableLessThanInRowQueryTest()
        {
            this.TestLessThanInRowQuery();
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableLessThanOrEqualInRowQueryTest()
        {
            this.TestLessThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableGreaterThanInRowQueryTest()
        {
            this.TestGreaterThanInRowQuery();
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableGreaterThanOrEqualInRowQueryTest()
        {
            this.TestGreaterThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableStartsWithInRowQueryTest()
        {
            this.TestStartsWithInRowQuery();
        }

        [TestMethod]
        public void CustomHeaderRowInTBODYTableContainsInRowQueryTest()
        {
            this.TestContainsInRowQuery();
        }
    }
}
