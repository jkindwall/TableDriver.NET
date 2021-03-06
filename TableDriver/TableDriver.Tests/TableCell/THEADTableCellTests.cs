﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    [TestClass]
    [DeploymentItem(TableSamples.TestPage)]
    [DeploymentItem(@"bin\Release\chromedriver.exe")]
    [DeploymentItem(@"bin\Release\geckodriver.exe")]
    public class THEADTableCellTests : TableCellTestsBase
    {
        private const string TableId = "with-thead-tbody";

        protected override TableCell GetTestTableCell()
        {
            Table table = Table.Create(this.Driver.FindElement(By.Id(THEADTableCellTests.TableId)));
            return table.FindCell(5, 1);
        }

        [TestMethod]
        public void THEADTableCellPropertiesTest()
        {
            this.TestTableCellProperties();
        }
    }
}
