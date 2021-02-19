﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    public class NoHeaderInTBODYTableCellTests : TableCellTestsBase
    {
        private const string TableId = "no-headers-in-tbody";

        protected override TableCell GetTestTableCell()
        {
            Table table = new Table(
                this.Driver.FindElement(By.Id(NoHeaderInTBODYTableCellTests.TableId)),
                null,
                0);
            return table.FindCell(5, 1);
        }

        [TestMethod]
        public void NoHeaderInTBODYTableCellPropertiesTest()
        {
            this.TestTableCellProperties();
        }
    }
}
