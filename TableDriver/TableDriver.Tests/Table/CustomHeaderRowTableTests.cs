using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class CustomHeaderRowTableTests : TableTestsBase
    {
        private const string TableId = "custom-header-row";
        private const string HeaderCss = "#custom-header-row tr[name=headerRow]";

        protected override Table GetTestTable()
        {
            return Table.CreateWithHeaderRow(
                this.Driver.FindElement(By.Id(CustomHeaderRowTableTests.TableId)),
                this.Driver.FindElement(By.CssSelector(CustomHeaderRowTableTests.HeaderCss)),
                2);
        }

        [TestMethod]
        public void CustomHeaderRowTablePropertiesTest()
        {
            this.TestTableProperties(CustomHeaderRowTableTests.TableId);
        }

        [TestMethod]
        public void CustomHeaderRowTableGetRowsTest()
        {
            this.TestGetRows();
        }

        [TestMethod]
        public void CustomHeaderRowTableFindRowTest()
        {
            this.TestFindRow();
        }

        [TestMethod]
        public void CustomHeaderRowTableFindRowsTest()
        {
            this.TestFindRows();
        }

        [TestMethod]
        public void CustomHeaderRowTableFindCellTest()
        {
            this.TestFindCell();
        }

        [TestMethod]
        public void CustomHeaderRowTableFindCellsTest()
        {
            this.TestFindCells();
        }

        [TestMethod]
        public void CustomHeaderRowTableGetHeaderTest()
        {
            this.TestGetHeader();
        }

        [TestMethod]
        public void CustomHeaderRowTableNotEqualInRowQueryTest()
        {
            this.TestNotEqualInRowQuery();
        }

        [TestMethod]
        public void CustomHeaderRowTableLessThanInRowQueryTest()
        {
            this.TestLessThanInRowQuery();
        }

        [TestMethod]
        public void CustomHeaderRowTableLessThanOrEqualInRowQueryTest()
        {
            this.TestLessThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void CustomHeaderRowTableGreaterThanInRowQueryTest()
        {
            this.TestGreaterThanInRowQuery();
        }

        [TestMethod]
        public void CustomHeaderRowTableGreaterThanOrEqualInRowQueryTest()
        {
            this.TestGreaterThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void CustomHeaderRowTableStartsWithInRowQueryTest()
        {
            this.TestStartsWithInRowQuery();
        }

        [TestMethod]
        public void CustomHeaderRowTableContainsInRowQueryTest()
        {
            this.TestContainsInRowQuery();
        }
    }
}
