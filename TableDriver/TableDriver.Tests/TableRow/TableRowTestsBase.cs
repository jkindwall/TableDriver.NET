using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    public abstract class TableRowTestsBase : WebDriverTestsBase
    {
        protected abstract TableRow GetTestTableRow();

        protected void TestTableRowProperties()
        {
            TableSamples.GoToTestPage(this.Driver);
            TableRow row = this.GetTestTableRow();

            Assert.AreEqual(10, row.RowIndex);
            Assert.AreEqual(5, row.CellCount);
            Assert.AreEqual("tr", row.Element.TagName);
            Assert.AreEqual("10", row.Element.GetAttribute("name"));
        }

        protected void TestGetCells()
        {
            TableSamples.GoToTestPage(this.Driver);
            TableRow row = this.GetTestTableRow();

            IList<TableCell> cells = row.GetCells();
            Assert.IsNotNull(cells);
            Assert.AreEqual(5, cells.Count);
            Assert.AreEqual("Mangosteen", cells[0].Element.Text);
            Assert.AreEqual("Dark Red", cells[1].Element.Text);
            Assert.AreEqual("Medium", cells[2].Element.Text);
            Assert.AreEqual("No", cells[3].Element.Text);
            Assert.AreEqual("11.0", cells[4].Element.Text);
        }

        protected void TestFindCell()
        {
            TableSamples.GoToTestPage(this.Driver);
            TableRow row = this.GetTestTableRow();

            TableCell cell = row.FindCell("Size");
            Assert.IsNotNull(cell);
            Assert.AreEqual("Medium", cell.Element.Text);

            cell = row.FindCell(1);
            Assert.IsNotNull(cell);
            Assert.AreEqual("Dark Red", cell.Element.Text);

            bool exceptionThrown = false;
            try
            {
                cell = row.FindCell("Alias");
            }
            catch (ArgumentException ex)
            {
                exceptionThrown = true;
                Assert.IsTrue(ex.Message.StartsWith("The table does not contain a column with the header caption of 'Alias'."));
                Assert.AreEqual("columnHeaderText", ex.ParamName);
            }
            Assert.IsTrue(exceptionThrown);

            exceptionThrown = false;
            try
            {
                cell = row.FindCell(5);
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }
    }
}
