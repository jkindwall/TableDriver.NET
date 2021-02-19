﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class THTagsTableTests : TableTestsBase
    {
        private const string TableId = "with-th-tags";

        protected override Table GetTestTable()
        {
            return new Table(this.Driver.FindElement(By.Id(THTagsTableTests.TableId)));
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
    }
}