using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
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
        public void CustomHeaderRowInTBODYTablePropertiesTest()
        {
            this.TestTableProperties(ExternalHeadersInTBODYTableTests.TableId);
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
    }
}
