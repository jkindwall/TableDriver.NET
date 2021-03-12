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
    public abstract class TableTestsBase : WebDriverTestsBase
    {
        protected abstract Table GetTestTable();

        protected void TestTableProperties(string tableId)
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            Assert.AreEqual(5, table.ColumnCount);
            Assert.AreEqual(12, table.RowCount);
            Assert.AreEqual("table", table.Element.TagName);
            Assert.AreEqual(tableId, table.Element.GetAttribute("id"));
            Assert.AreEqual("tr", table.HeaderRow.TagName);
            Assert.AreEqual("headerRow", table.HeaderRow.GetAttribute("name"));
            Assert.IsNull(table.HeaderElements);
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

        protected void TestNotEqualInRowQuery()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableRow> rows = table.FindRows("Citrus?!=No");
            Assert.IsNotNull(rows);
            Assert.AreEqual(3, rows.Count);
            Assert.AreEqual("1", rows[0].Element.GetAttribute("name"));
            Assert.AreEqual("2", rows[1].Element.GetAttribute("name"));
            Assert.AreEqual("4", rows[2].Element.GetAttribute("name"));
        }

        protected void TestLessThanInRowQuery()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableRow> rows = table.FindRows("Rating<3");
            Assert.IsNotNull(rows);
            Assert.AreEqual(2, rows.Count);
            Assert.AreEqual("2", rows[0].Element.GetAttribute("name"));
            Assert.AreEqual("7", rows[1].Element.GetAttribute("name"));

            rows = table.FindRows("Rating<1");
            Assert.IsNotNull(rows);
            Assert.AreEqual(0, rows.Count);

            rows = table.FindRows("Rating<Schmoop");
            Assert.IsNotNull(rows);
            Assert.AreEqual(0, rows.Count);

            bool exceptionThrown = false;
            try
            {
                table.FindRow("Color<8");
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }

        protected void TestLessThanOrEqualInRowQuery()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableCell> cells = table.FindCells("Rating<=3.2", "Name");
            Assert.IsNotNull(cells);
            Assert.AreEqual(3, cells.Count);
            Assert.AreEqual("Grapefruit", cells[0].Element.Text);
            Assert.AreEqual("Strawberry", cells[1].Element.Text);
            Assert.AreEqual("Watermelon", cells[2].Element.Text);

            bool exceptionThrown = false;
            try
            {
                table.FindCell("Rating<=1.1", "Name");
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);

            cells = table.FindCells("Rating<=Floofy", "Name");
            Assert.IsNotNull(cells);
            Assert.AreEqual(0, cells.Count);

            cells = table.FindCells("Size<=90", "Name");
            Assert.IsNotNull(cells);
            Assert.AreEqual(0, cells.Count);
        }

        protected void TestGreaterThanInRowQuery()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableRow> rows = table.FindRows("Rating>7");
            Assert.IsNotNull(rows);
            Assert.AreEqual(5, rows.Count);
            Assert.AreEqual("3", rows[0].Element.GetAttribute("name"));
            Assert.AreEqual("4", rows[1].Element.GetAttribute("name"));
            Assert.AreEqual("9", rows[2].Element.GetAttribute("name"));
            Assert.AreEqual("10", rows[3].Element.GetAttribute("name"));
            Assert.AreEqual("11", rows[4].Element.GetAttribute("name"));

            rows = table.FindRows("Rating>100");
            Assert.IsNotNull(rows);
            Assert.AreEqual(0, rows.Count);

            bool exceptionThrown = false;
            try
            {
                table.FindRow("Rating>_____T+++");
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);

            rows = table.FindRows("Name>3");
            Assert.IsNotNull(rows);
            Assert.AreEqual(0, rows.Count);
        }

        protected void TestGreaterThanOrEqualInRowQuery()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableCell> cells = table.FindCells("Rating>=7.7", "Name");
            Assert.IsNotNull(cells);
            Assert.AreEqual(4, cells.Count);
            Assert.AreEqual("Grape", cells[0].Element.Text);
            Assert.AreEqual("Pineapple", cells[1].Element.Text);
            Assert.AreEqual("Mangosteen", cells[2].Element.Text);
            Assert.AreEqual("Lychee", cells[3].Element.Text);

            cells = table.FindCells("Rating>=12.2", "Name");
            Assert.IsNotNull(cells);
            Assert.AreEqual(0, cells.Count);

            cells = table.FindCells("Rating>=Six", "Name");
            Assert.IsNotNull(cells);
            Assert.AreEqual(0, cells.Count);

            bool exceptionThrown = false;
            try
            {
                table.FindCell("Citrus?>=4.2", "Name");
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }

        protected void TestStartsWithInRowQuery()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableRow> rows = table.FindRows("Name^=Mango");
            Assert.IsNotNull(rows);
            Assert.AreEqual(2, rows.Count);
            Assert.AreEqual("9", rows[0].Element.GetAttribute("name"));
            Assert.AreEqual("10", rows[1].Element.GetAttribute("name"));

            rows = table.FindRows("Name^=ape");
            Assert.IsNotNull(rows);
            Assert.AreEqual(0, rows.Count);

            bool exceptionThrown = false;
            try
            {
                table.FindRow("Color^=Big");
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }

        protected void TestContainsInRowQuery()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableCell> cells = table.FindCells("Name*=Grape", "Rating");
            Assert.IsNotNull(cells);
            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual("2.8", cells[0].Element.Text);
            Assert.AreEqual("7.8", cells[1].Element.Text);

            cells = table.FindCells("Color*=l", "Name");
            Assert.IsNotNull(cells);
            Assert.AreEqual(5, cells.Count);
            Assert.AreEqual("Grapefruit", cells[0].Element.Text);
            Assert.AreEqual("Grape", cells[1].Element.Text);
            Assert.AreEqual("Pineapple", cells[2].Element.Text);
            Assert.AreEqual("Blueberry", cells[3].Element.Text);
            Assert.AreEqual("Mango", cells[4].Element.Text);

            cells = table.FindCells("Name*=berry", "Color");
            Assert.IsNotNull(cells);
            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual("Blue", cells[0].Element.Text);
            Assert.AreEqual("Red", cells[1].Element.Text);

            bool exceptionThrown = false;
            try
            {
                table.FindCell("Name*=q", "Name");
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);

            cells = table.FindCells("Name*=Snapple", "Citrus?");
            Assert.IsNotNull(cells);
            Assert.AreEqual(0, cells.Count);
        }
    }
}
