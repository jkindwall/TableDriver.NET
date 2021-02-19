﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class FirstRowHeaderTableRowTests : TableRowTestsBase
    {
        private const string TableId = "first-row-headers";

        protected override TableRow GetTestTableRow()
        {
            Table table = new Table(this.Driver.FindElement(By.Id(FirstRowHeaderTableRowTests.TableId)));
            return table.FindRow(10);
        }

        [TestMethod]
        public void FirstRowHeaderTableRowPropertiesTest()
        {
            this.TestTableRowProperties();
        }

        [TestMethod]
        public void FirstRowHeaderTableRowGetCellsTest()
        {
            this.TestGetCells();
        }

        [TestMethod]
        public void FirstRowHeaderTableRowFindCellTest()
        {
            this.TestFindCell();
        }
    }
}