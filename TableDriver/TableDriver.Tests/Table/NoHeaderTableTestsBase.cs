using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    public abstract class NoHeaderTableTestsBase : TableTestsBase
    {
        protected new void TestTableProperties(string tableId)
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            Assert.AreEqual(5, table.ColumnCount);
            Assert.AreEqual(12, table.RowCount);
            Assert.AreEqual("table", table.Element.TagName);
            Assert.AreEqual(tableId, table.Element.GetAttribute("id"));
            Assert.IsNull(table.HeaderRow);
            Assert.IsNull(table.HeaderElements);
        }

        protected new void TestFindRow()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            TableRow row = table.FindRow("0=Grape");
            Assert.IsNotNull(row);
            Assert.AreEqual("3", row.Element.GetAttribute("name"));

            row = table.FindRow("2=Large");
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
                row = table.FindRow("0=Lemon");
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
                row = table.FindRow("6=Fancy");
            }
            catch (ArgumentException ex)
            {
                exceptionThrown = true;
                Assert.IsTrue(ex.Message.StartsWith("The table does not contain a column with the header caption of '6'."));
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

        protected new void TestFindRows()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableRow> rows = table.FindRows("3=Yes");
            Assert.IsNotNull(rows);
            Assert.AreEqual(3, rows.Count);
            Assert.AreEqual("1", rows[0].Element.GetAttribute("name"));
            Assert.AreEqual("2", rows[1].Element.GetAttribute("name"));
            Assert.AreEqual("4", rows[2].Element.GetAttribute("name"));

            rows = table.FindRows(@"0=Mango|1=Red&2=Small|\4=5.0");
            Assert.IsNotNull(rows);
            Assert.AreEqual(4, rows.Count);
            Assert.AreEqual("0", rows[0].Element.GetAttribute("name"));
            Assert.AreEqual("6", rows[1].Element.GetAttribute("name"));
            Assert.AreEqual("9", rows[2].Element.GetAttribute("name"));
            Assert.AreEqual("11", rows[3].Element.GetAttribute("name"));

            rows = table.FindRows("1=Purple&2=Large");
            Assert.IsNotNull(rows);
            Assert.IsFalse(rows.Any());

            rows = table.FindRows(@"\13=Fred");
            Assert.IsNotNull(rows);
            Assert.IsFalse(rows.Any());

            bool exceptionThrown = false;
            try
            {
                rows = table.FindRows("7=Near");
            }
            catch (ArgumentException ex)
            {
                exceptionThrown = true;
                Assert.IsTrue(ex.Message.StartsWith("The table does not contain a column with the header caption of '7'."));
                Assert.AreEqual("fieldCondition", ex.ParamName);
            }
            Assert.IsTrue(exceptionThrown);

        }

        protected new void TestFindCell()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            TableCell cell = table.FindCell("0=Watermelon", "2");
            Assert.IsNotNull(cell);
            Assert.AreEqual("Large", cell.Element.Text);

            cell = table.FindCell(@"\2=Small", 0);
            Assert.IsNotNull(cell);
            Assert.AreEqual("Grape", cell.Element.Text);

            cell = table.FindCell(10, "0");
            Assert.IsNotNull(cell);
            Assert.AreEqual("Mangosteen", cell.Element.Text);

            cell = table.FindCell(8, 4);
            Assert.IsNotNull(cell);
            Assert.AreEqual("4.7", cell.Element.Text);

            bool exceptionThrown = false;
            try
            {
                cell = table.FindCell("0=Mulberry", "4");
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);

            exceptionThrown = false;
            try
            {
                cell = table.FindCell(@"\6=Zesty", "4");
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);

            exceptionThrown = false;
            try
            {
                cell = table.FindCell("9=Obscure", "4");
            }
            catch (ArgumentException ex)
            {
                exceptionThrown = true;
                Assert.IsTrue(ex.Message.StartsWith("The table does not contain a column with the header caption of '9'."));
                Assert.AreEqual("fieldCondition", ex.ParamName);
            }
            Assert.IsTrue(exceptionThrown);

            exceptionThrown = false;
            try
            {
                cell = table.FindCell("0=Apple", "8");
            }
            catch (ArgumentException ex)
            {
                exceptionThrown = true;
                Assert.IsTrue(ex.Message.StartsWith("The table does not contain a column with the header caption of '8'."));
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

        protected new void TestFindCells()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableCell> cells = table.FindCells("1=Green", "0");
            Assert.IsNotNull(cells);
            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual("Watermelon", cells[0].Element.Text);
            Assert.AreEqual("Kiwi", cells[1].Element.Text);

            cells = table.FindCells(@"0=Orange|1=Red", 4);
            Assert.IsNotNull(cells);
            Assert.AreEqual(4, cells.Count);
            Assert.AreEqual("5.0", cells[0].Element.Text);
            Assert.AreEqual("6.7", cells[1].Element.Text);
            Assert.AreEqual("3.2", cells[2].Element.Text);
            Assert.AreEqual("7.7", cells[3].Element.Text);

            cells = table.FindCells("3=Maybe", "0");
            Assert.IsNotNull(cells);
            Assert.IsFalse(cells.Any());

            cells = table.FindCells(@"\9=Fluffy", 2);
            Assert.IsNotNull(cells);
            Assert.IsFalse(cells.Any());

            cells = table.FindCells(@"2=Large", 42);
            Assert.IsNotNull(cells);
            Assert.IsFalse(cells.Any());

            bool exceptionThrown = false;
            try
            {
                cells = table.FindCells("11=50%", 1);
            }
            catch (ArgumentException ex)
            {
                exceptionThrown = true;
                Assert.IsTrue(ex.Message.StartsWith("The table does not contain a column with the header caption of '11'."));
                Assert.AreEqual("fieldCondition", ex.ParamName);
            }
            Assert.IsTrue(exceptionThrown);

            exceptionThrown = false;
            try
            {
                cells = table.FindCells(@"\2=Medium", "23");
            }
            catch (ArgumentException ex)
            {
                exceptionThrown = true;
                Assert.IsTrue(ex.Message.StartsWith("The table does not contain a column with the header caption of '23'."));
                Assert.AreEqual("columnHeaderText", ex.ParamName);
            }
            Assert.IsTrue(exceptionThrown);
        }

        protected new void TestGetHeader()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IWebElement header;

            bool exceptionThrown = false;
            try
            {
                header = table.GetHeader("0");
            }
            catch (InvalidOperationException ex)
            {
                exceptionThrown = true;
                Assert.AreEqual("This table does not have identified column headers.", ex.Message);
            }
            Assert.IsTrue(exceptionThrown);

            exceptionThrown = false;
            try
            {
                header = table.GetHeader(0);
            }
            catch (InvalidOperationException ex)
            {
                exceptionThrown = true;
                Assert.AreEqual("This table does not have identified column headers.", ex.Message);
            }
            Assert.IsTrue(exceptionThrown);
        }

        protected new void TestNotEqualInRowQuery()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableRow> rows = table.FindRows("3!=No");
            Assert.IsNotNull(rows);
            Assert.AreEqual(3, rows.Count);
            Assert.AreEqual("1", rows[0].Element.GetAttribute("name"));
            Assert.AreEqual("2", rows[1].Element.GetAttribute("name"));
            Assert.AreEqual("4", rows[2].Element.GetAttribute("name"));
        }

        protected new void TestLessThanInRowQuery()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableRow> rows = table.FindRows("4<3");
            Assert.IsNotNull(rows);
            Assert.AreEqual(2, rows.Count);
            Assert.AreEqual("2", rows[0].Element.GetAttribute("name"));
            Assert.AreEqual("7", rows[1].Element.GetAttribute("name"));

            rows = table.FindRows(@"\4<1");
            Assert.IsNotNull(rows);
            Assert.AreEqual(0, rows.Count);

            rows = table.FindRows("4<Schmoop");
            Assert.IsNotNull(rows);
            Assert.AreEqual(0, rows.Count);

            bool exceptionThrown = false;
            try
            {
                table.FindRow(@"\1<8");
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }

        protected new void TestLessThanOrEqualInRowQuery()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableCell> cells = table.FindCells(@"\4<=3.2", 0);
            Assert.IsNotNull(cells);
            Assert.AreEqual(3, cells.Count);
            Assert.AreEqual("Grapefruit", cells[0].Element.Text);
            Assert.AreEqual("Strawberry", cells[1].Element.Text);
            Assert.AreEqual("Watermelon", cells[2].Element.Text);

            bool exceptionThrown = false;
            try
            {
                table.FindCell("4<=1.1", 0);
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);

            cells = table.FindCells(@"\4<=Floofy", 0);
            Assert.IsNotNull(cells);
            Assert.AreEqual(0, cells.Count);

            cells = table.FindCells("2<=90", 0);
            Assert.IsNotNull(cells);
            Assert.AreEqual(0, cells.Count);
        }

        protected new void TestGreaterThanInRowQuery()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableRow> rows = table.FindRows("4>7");
            Assert.IsNotNull(rows);
            Assert.AreEqual(5, rows.Count);
            Assert.AreEqual("3", rows[0].Element.GetAttribute("name"));
            Assert.AreEqual("4", rows[1].Element.GetAttribute("name"));
            Assert.AreEqual("9", rows[2].Element.GetAttribute("name"));
            Assert.AreEqual("10", rows[3].Element.GetAttribute("name"));
            Assert.AreEqual("11", rows[4].Element.GetAttribute("name"));

            rows = table.FindRows(@"\4>100");
            Assert.IsNotNull(rows);
            Assert.AreEqual(0, rows.Count);

            bool exceptionThrown = false;
            try
            {
                table.FindRow("4>_____T+++");
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);

            rows = table.FindRows(@"\0>3");
            Assert.IsNotNull(rows);
            Assert.AreEqual(0, rows.Count);
        }

        protected new void TestGreaterThanOrEqualInRowQuery()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableCell> cells = table.FindCells(@"\4>=7.7", 0);
            Assert.IsNotNull(cells);
            Assert.AreEqual(4, cells.Count);
            Assert.AreEqual("Grape", cells[0].Element.Text);
            Assert.AreEqual("Pineapple", cells[1].Element.Text);
            Assert.AreEqual("Mangosteen", cells[2].Element.Text);
            Assert.AreEqual("Lychee", cells[3].Element.Text);

            cells = table.FindCells("4>=12.2", 0);
            Assert.IsNotNull(cells);
            Assert.AreEqual(0, cells.Count);

            cells = table.FindCells(@"\4>=Six", 0);
            Assert.IsNotNull(cells);
            Assert.AreEqual(0, cells.Count);

            bool exceptionThrown = false;
            try
            {
                table.FindCell("3>=4.2", 0);
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }

        protected new void TestStartsWithInRowQuery()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableRow> rows = table.FindRows("0^=Mango");
            Assert.IsNotNull(rows);
            Assert.AreEqual(2, rows.Count);
            Assert.AreEqual("9", rows[0].Element.GetAttribute("name"));
            Assert.AreEqual("10", rows[1].Element.GetAttribute("name"));

            rows = table.FindRows(@"\0^=ape");
            Assert.IsNotNull(rows);
            Assert.AreEqual(0, rows.Count);

            bool exceptionThrown = false;
            try
            {
                table.FindRow("1^=Big");
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }

        protected new void TestContainsInRowQuery()
        {
            TableSamples.GoToTestPage(this.Driver);
            Table table = this.GetTestTable();

            IList<TableCell> cells = table.FindCells(@"\0*=Grape", 4);
            Assert.IsNotNull(cells);
            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual("2.8", cells[0].Element.Text);
            Assert.AreEqual("7.8", cells[1].Element.Text);

            cells = table.FindCells("1*=l", 0);
            Assert.IsNotNull(cells);
            Assert.AreEqual(5, cells.Count);
            Assert.AreEqual("Grapefruit", cells[0].Element.Text);
            Assert.AreEqual("Grape", cells[1].Element.Text);
            Assert.AreEqual("Pineapple", cells[2].Element.Text);
            Assert.AreEqual("Blueberry", cells[3].Element.Text);
            Assert.AreEqual("Mango", cells[4].Element.Text);

            cells = table.FindCells(@"\0*=berry", 1);
            Assert.IsNotNull(cells);
            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual("Blue", cells[0].Element.Text);
            Assert.AreEqual("Red", cells[1].Element.Text);

            bool exceptionThrown = false;
            try
            {
                table.FindCell("0*=q", 0);
            }
            catch (NoSuchElementException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);

            cells = table.FindCells(@"\0*=Snapple", 3);
            Assert.IsNotNull(cells);
            Assert.AreEqual(0, cells.Count);
        }
    }
}
