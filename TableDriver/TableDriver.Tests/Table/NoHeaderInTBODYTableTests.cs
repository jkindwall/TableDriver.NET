using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class NoHeaderInTBODYTableTests : NoHeaderTableTestsBase
    {
        private const string TableId = "no-headers-in-tbody";

        protected override Table GetTestTable()
        {
            return Table.CreateWithNoHeaders(this.Driver.FindElement(By.Id(NoHeaderInTBODYTableTests.TableId)), 0);
        }

        [TestMethod]
        public void NoHeaderTablePropertiesTest()
        {
            this.TestTableProperties(NoHeaderInTBODYTableTests.TableId);
        }

        [TestMethod]
        public void NoHeaderTableGetRowsTest()
        {
            this.TestGetRows();
        }

        [TestMethod]
        public void NoHeaderTableFindRowTest()
        {
            this.TestFindRow();
        }

        [TestMethod]
        public void NoHeaderTableFindRowsTest()
        {
            this.TestFindRows();
        }

        [TestMethod]
        public void NoHeaderTableFindCellTest()
        {
            this.TestFindCell();
        }

        [TestMethod]
        public void NoHeaderTableFindCellsTest()
        {
            this.TestFindCells();
        }

        [TestMethod]
        public void NoHeaderTableGetHeaderTest()
        {
            this.TestGetHeader();
        }
    }
}
