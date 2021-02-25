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
        public void CustomHeaderRowTablePropertiesTest()
        {
            this.TestTableProperties(ExternalHeadersTableTests.TableId);
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
