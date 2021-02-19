using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class CustomHeaderRowTableTests : TableTestsBase
    {
        private const string TableId = "custom-header-row";
        private const string HeaderCss = "#custom-header-row tr[name=headerRow]";

        protected override Table GetTestTable()
        {
            return new Table(
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
    }
}
