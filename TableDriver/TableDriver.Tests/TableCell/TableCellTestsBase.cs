﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    public abstract class TableCellTestsBase : WebDriverTestsBase
    {
        protected abstract TableCell GetTestTableCell();

        protected void TestTableCellProperties()
        {
            TableSamples.GoToTestPage(this.Driver);
            TableCell cell = this.GetTestTableCell();

            Assert.AreEqual(5, cell.RowIndex);
            Assert.AreEqual(1, cell.ColumnIndex);
            Assert.AreEqual("td", cell.Element.TagName);
            Assert.AreEqual("Blue", cell.Element.Text);
        }
    }
}
