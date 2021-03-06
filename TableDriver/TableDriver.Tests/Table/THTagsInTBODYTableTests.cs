﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class THTagsInTBODYTableTests : TableTestsBase
    {
        private const string TableId = "with-th-tags-in-tbody";

        protected override Table GetTestTable()
        {
            return Table.Create(this.Driver.FindElement(By.Id(THTagsInTBODYTableTests.TableId)));
        }

        [TestMethod]
        public void THTagsInTBODYTablePropertiesTest()
        {
            this.TestTableProperties(THTagsInTBODYTableTests.TableId);
        }

        [TestMethod]
        public void THTagsInTBODYTableGetRowsTest()
        {
            this.TestGetRows();
        }

        [TestMethod]
        public void THTagsInTBODYTableFindRowTest()
        {
            this.TestFindRow();
        }

        [TestMethod]
        public void THTagsInTBODYTableFindRowsTest()
        {
            this.TestFindRows();
        }

        [TestMethod]
        public void THTagsInTBODYTableFindCellTest()
        {
            this.TestFindCell();
        }

        [TestMethod]
        public void THTagsInTBODYTableFindCellsTest()
        {
            this.TestFindCells();
        }

        [TestMethod]
        public void THTagsInTBODYTableGetHeaderTest()
        {
            this.TestGetHeader();
        }

        [TestMethod]
        public void THTagsInTBODYTableNotEqualInRowQueryTest()
        {
            this.TestNotEqualInRowQuery();
        }

        [TestMethod]
        public void THTagsInTBODYTableLessThanInRowQueryTest()
        {
            this.TestLessThanInRowQuery();
        }

        [TestMethod]
        public void THTagsInTBODYTableLessThanOrEqualInRowQueryTest()
        {
            this.TestLessThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void THTagsInTBODYTableGreaterThanInRowQueryTest()
        {
            this.TestGreaterThanInRowQuery();
        }

        [TestMethod]
        public void THTagsInTBODYTableGreaterThanOrEqualInRowQueryTest()
        {
            this.TestGreaterThanOrEqualInRowQuery();
        }

        [TestMethod]
        public void THTagsInTBODYTableStartsWithInRowQueryTest()
        {
            this.TestStartsWithInRowQuery();
        }

        [TestMethod]
        public void THTagsInTBODYTableContainsInRowQueryTest()
        {
            this.TestContainsInRowQuery();
        }
    }
}
