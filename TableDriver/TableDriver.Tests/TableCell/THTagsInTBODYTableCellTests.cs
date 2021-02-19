﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class THTagsInTBODYTableCellTests : TableCellTestsBase
    {
        private const string TableId = "with-th-tags-in-tbody";

        protected override TableCell GetTestTableCell()
        {
            Table table = new Table(this.Driver.FindElement(By.Id(THTagsInTBODYTableCellTests.TableId)));
            return table.FindCell(5, 1);
        }

        [TestMethod]
        public void THTagsInTBODYTableCellPropertiesTest()
        {
            this.TestTableCellProperties();
        }
    }
}