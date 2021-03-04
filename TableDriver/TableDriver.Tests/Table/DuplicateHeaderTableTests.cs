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
    [TestClass]
    public class DuplicateHeaderTableTests : WebDriverTestsBase
    {
        private const string TableId = "with-duplicate-headers";

        protected Table GetTestTable()
        {
            return Table.Create(this.Driver.FindElement(By.Id(DuplicateHeaderTableTests.TableId)));
        }

        [TestMethod]
        public void DuplicateCellTests()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            Assert.AreEqual("Red", table.FindCell("Color-1=Crimson", "Color").Element.Text);
            Assert.AreEqual("or maybe", table.FindCell("Color=Red", "").Element.Text);
            Assert.AreEqual("Crimson", table.FindCell("=or maybe", "Color-1").Element.Text);
            Assert.AreEqual("or possibly", table.FindCell("Color-2=Vermillion", "-1").Element.Text);
            Assert.AreEqual("Vermillion", table.FindCell("-1=or possibly", "Color-2").Element.Text);

            Assert.AreEqual("Blue", table.FindCell("Color-1=Azure", "Color").Element.Text);
            Assert.AreEqual("or maybe", table.FindCell("Color=Blue", "").Element.Text);
            Assert.AreEqual("Azure", table.FindCell(1, "Color-1").Element.Text);
            Assert.AreEqual("or rather", table.FindCell("Color-2=Indigo", "-1").Element.Text);
            Assert.AreEqual("Indigo", table.FindCell("-1=or rather", "Color-2").Element.Text);

            Assert.AreEqual("Purple", table.FindCell("Color-1=Violet", "Color").Element.Text);
            Assert.AreEqual("no wait", table.FindCell("Color=Purple", "").Element.Text);
            Assert.AreEqual("Violet", table.FindCell("=no wait", "Color-1").Element.Text);
            Assert.AreEqual("or how about", table.FindCell("Color-2=Lilac", "-1").Element.Text);
            Assert.AreEqual("Lilac", table.FindCell("-1=or how about", "Color-2").Element.Text);

            IList<string> thirdColors = table.FindCells("=or maybe", "Color-2").Select(c => c.Element.Text).ToList();
            Assert.AreEqual("Vermillion", thirdColors[0]);
            Assert.AreEqual("Indigo", thirdColors[1]);
        }

        protected void TestGetRows()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableRow> rows = table.GetRows();
            Assert.IsNotNull(rows);
            Assert.AreEqual(12, rows.Count);
            for (int i = 0; i < 12; i++)
            {
                Assert.AreEqual(i.ToString(), rows[i].Element.GetAttribute("name"));
            }
        }

        protected void TestFindRow()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            TableRow row = table.FindRow("Name=Grape");
            Assert.IsNotNull(row);
            Assert.AreEqual("3", row.Element.GetAttribute("name"));

            row = table.FindRow("Size=Large");
            Assert.IsNotNull(row);
            Assert.AreEqual("4", row.Element.GetAttribute("name"));

            row = table.FindRow(@"\4=6.3");
            Assert.IsNotNull(row);
            Assert.AreEqual("5", row.Element.GetAttribute("name"));

            row = table.FindRow(8);
            Assert.IsNotNull(row);
            Assert.AreEqual("8", row.Element.GetAttribute("name"));

            bool exceptionThrown = false;
            try
            {
                row = table.FindRow("Name=Lemon");
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);

            exceptionThrown = false;
            try
            {
                row = table.FindRow(12);
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);

            exceptionThrown = false;
            try
            {
                row = table.FindRow("Style=Fancy");
            }
            catch (ArgumentException ex)
            {
                exceptionThrown = true;
                Assert.IsTrue(ex.Message.StartsWith("The table does not contain a column with the header caption of 'Style'."));
                Assert.AreEqual("fieldCondition", ex.ParamName);
            }
            Assert.IsTrue(exceptionThrown);

            exceptionThrown = false;
            try
            {
                row = table.FindRow(@"\8=32");
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }

        protected void TestFindRows()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableRow> rows = table.FindRows("Citrus?=Yes");
            Assert.IsNotNull(rows);
            Assert.AreEqual(3, rows.Count);
            Assert.AreEqual("1", rows[0].Element.GetAttribute("name"));
            Assert.AreEqual("2", rows[1].Element.GetAttribute("name"));
            Assert.AreEqual("4", rows[2].Element.GetAttribute("name"));

            rows = table.FindRows(@"Name=Mango|Color=Red&Size=Small|\4=5.0");
            Assert.IsNotNull(rows);
            Assert.AreEqual(4, rows.Count);
            Assert.AreEqual("0", rows[0].Element.GetAttribute("name"));
            Assert.AreEqual("6", rows[1].Element.GetAttribute("name"));
            Assert.AreEqual("9", rows[2].Element.GetAttribute("name"));
            Assert.AreEqual("11", rows[3].Element.GetAttribute("name"));

            rows = table.FindRows("Color=Purple&Size=Large");
            Assert.IsNotNull(rows);
            Assert.IsFalse(rows.Any());

            rows = table.FindRows(@"\13=Fred");
            Assert.IsNotNull(rows);
            Assert.IsFalse(rows.Any());

            bool exceptionThrown = false;
            try
            {
                rows = table.FindRows("Proximity=Near");
            }
            catch (ArgumentException ex)
            {
                exceptionThrown = true;
                Assert.IsTrue(ex.Message.StartsWith("The table does not contain a column with the header caption of 'Proximity'."));
                Assert.AreEqual("fieldCondition", ex.ParamName);
            }
            Assert.IsTrue(exceptionThrown);

        }

        protected void TestFindCell()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            TableCell cell = table.FindCell("Name=Watermelon", "Size");
            Assert.IsNotNull(cell);
            Assert.AreEqual("Large", cell.Element.Text);

            cell = table.FindCell(@"\2=Small", 0);
            Assert.IsNotNull(cell);
            Assert.AreEqual("Grape", cell.Element.Text);

            cell = table.FindCell(10, "Name");
            Assert.IsNotNull(cell);
            Assert.AreEqual("Mangosteen", cell.Element.Text);

            cell = table.FindCell(8, 4);
            Assert.IsNotNull(cell);
            Assert.AreEqual("4.7", cell.Element.Text);

            bool exceptionThrown = false;
            try
            {
                cell = table.FindCell("Name=Mulberry", "Rating");
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);

            exceptionThrown = false;
            try
            {
                cell = table.FindCell(@"\6=Zesty", "Rating");
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);

            exceptionThrown = false;
            try
            {
                cell = table.FindCell("Fame=Obscure", "Rating");
            }
            catch (ArgumentException ex)
            {
                exceptionThrown = true;
                Assert.IsTrue(ex.Message.StartsWith("The table does not contain a column with the header caption of 'Fame'."));
                Assert.AreEqual("fieldCondition", ex.ParamName);
            }
            Assert.IsTrue(exceptionThrown);

            exceptionThrown = false;
            try
            {
                cell = table.FindCell("Name=Apple", "Weight");
            }
            catch (ArgumentException ex)
            {
                exceptionThrown = true;
                Assert.IsTrue(ex.Message.StartsWith("The table does not contain a column with the header caption of 'Weight'."));
                Assert.AreEqual("columnHeaderText", ex.ParamName);
            }
            Assert.IsTrue(exceptionThrown);

            exceptionThrown = false;
            try
            {
                cell = table.FindCell(7, 9);
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }

        protected void TestFindCells()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableCell> cells = table.FindCells("Color=Green", "Name");
            Assert.IsNotNull(cells);
            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual("Watermelon", cells[0].Element.Text);
            Assert.AreEqual("Kiwi", cells[1].Element.Text);

            cells = table.FindCells(@"Name=Orange|Color=Red", 4);
            Assert.IsNotNull(cells);
            Assert.AreEqual(4, cells.Count);
            Assert.AreEqual("5.0", cells[0].Element.Text);
            Assert.AreEqual("6.7", cells[1].Element.Text);
            Assert.AreEqual("3.2", cells[2].Element.Text);
            Assert.AreEqual("7.7", cells[3].Element.Text);

            cells = table.FindCells("Citrus?=Maybe", "Name");
            Assert.IsNotNull(cells);
            Assert.IsFalse(cells.Any());

            cells = table.FindCells(@"\9=Fluffy", 2);
            Assert.IsNotNull(cells);
            Assert.IsFalse(cells.Any());

            cells = table.FindCells(@"Size=Large", 42);
            Assert.IsNotNull(cells);
            Assert.IsFalse(cells.Any());

            bool exceptionThrown = false;
            try
            {
                cells = table.FindCells("Skin Transparency=50%", 1);
            }
            catch (ArgumentException ex)
            {
                exceptionThrown = true;
                Assert.IsTrue(ex.Message.StartsWith("The table does not contain a column with the header caption of 'Skin Transparency'."));
                Assert.AreEqual("fieldCondition", ex.ParamName);
            }
            Assert.IsTrue(exceptionThrown);

            exceptionThrown = false;
            try
            {
                cells = table.FindCells(@"\2=Medium", "Sweetness");
            }
            catch (ArgumentException ex)
            {
                exceptionThrown = true;
                Assert.IsTrue(ex.Message.StartsWith("The table does not contain a column with the header caption of 'Sweetness'."));
                Assert.AreEqual("columnHeaderText", ex.ParamName);
            }
            Assert.IsTrue(exceptionThrown);
        }

        protected void TestGetHeader()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IWebElement header = table.GetHeader("Size");
            Assert.IsNotNull(header);
            Assert.AreEqual("Size", header.Text);

            header = table.GetHeader(0);
            Assert.IsNotNull(header);
            Assert.AreEqual("Name", header.Text);

            bool exceptionThrown = false;
            try
            {
                header = table.GetHeader("Quantity");
            }
            catch (ArgumentException ex)
            {
                exceptionThrown = true;
                Assert.IsTrue(ex.Message.StartsWith("The table does not contain a column with the header caption of 'Quantity'."));
                Assert.AreEqual("headerText", ex.ParamName);
            }
            Assert.IsTrue(exceptionThrown);

            exceptionThrown = false;
            try
            {
                header = table.GetHeader(8);
            }
            catch (NoSuchElementException)
            {
                if (table.HeaderRow == null) { throw; }
                exceptionThrown = true;
            }
            catch (ArgumentOutOfRangeException)
            {
                if (table.HeaderElements == null) { throw; }
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }
    }
}