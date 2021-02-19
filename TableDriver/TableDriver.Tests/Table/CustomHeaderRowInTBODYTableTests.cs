﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class CustomHeaderRowInTBODYTableTests : TableTestsBase
    {
        private const string TableId = "custom-header-row-in-tbody";
        private const string HeaderCss = "#custom-header-row-in-tbody tr[name=headerRow]";

        protected override Table GetTestTable()
        {
            return new Table(
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
    }
}
