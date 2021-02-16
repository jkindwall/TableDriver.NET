﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TableDriver.Extensions;
using TableDriver.RowQuery;

namespace TableDriver.Elements
{
    /// <summary>
    /// Class rerpresenting a table html element and all its contents
    /// </summary>
    public class Table
    {
        private IDictionary<string, int> Headers { get; init; }
        private int SkipRows { get; init; }
        private string RowXPathPrefix { get; init; }

        /// <summary>
        /// IWebElement representing the root "table" element
        /// </summary>
        public IWebElement Element { get; private init; }

        /// <summary>
        /// IWebElement representing the header row (if any) of the table ("tr" element that contains the column header captions) 
        /// </summary>
        public IWebElement? HeaderRow { get; private init; }

        private static (IWebElement HeaderRow, int SkipRows) TryFindHeaderRow(IWebElement element)
        {
            IList<IWebElement> theadRows = element.FindElements(By.XPath("/thead/tr"));
            if (theadRows.Any())
            {
                return (theadRows.Last(), 0);
            }

            IList<IWebElement> thRows = element.FindElements(By.XPath("/tbody/tr[th] | /tr[th]"));
            if (thRows.Any())
            {
                return (thRows.Last(), thRows.Count());
            }

            return (element.FindElement(By.XPath("/tbody/tr[1] | /tr[1]")), 1);
        }

        private static string BuildRowXPathPrefix(IWebElement element, int skipRows)
        {
            StringBuilder builder = new();
            if (element.FindElements(By.XPath("/tbody")).Any())
            {
                builder.Append("/tbody");
            }

            if (skipRows > 0)
            {
                builder.Append($"/tr[{skipRows}]/following-sibling::tr");
            }
            else
            {
                builder.Append("/tr");
            }

            return builder.ToString();
        }

        private IDictionary<string, int> LoadHeaders()
        {
            Dictionary<string, int> headers = new();
            if (this.HeaderRow == null)
            {
                IList<IWebElement> columns = this.Element.FindElements(
                    By.XPath($"{this.RowXPathPrefix}tr/th | {this.RowXPathPrefix}tr/td"));
                for (int i = 0; i < columns.Count; i++)
                {
                    string indexText = i.ToString();
                    headers[indexText] = i;
                }
            }
            else
            {
                IList<IWebElement> headerElements = this.HeaderRow.FindElements(By.XPath("/td | /th"));
                int columnIndex = 0;
                foreach (IWebElement headerElement in headerElements)
                {
                    headers[headerElement.Text] = columnIndex++;
                }
            }

            return headers;
        }

        /// <summary>
        /// Crates a new instance of the Table class based on the specified "table" element
        /// </summary>
        /// <param name="element">IWebElement representing the "table" element</param>
        /// <remarks>The Table class will attempt to infer the structure of the table based on a few standard table layouts.
        /// If this does not work, try using the more specific constructor which specifies the header row and skip rows value.</remarks>
        public Table(IWebElement element)
            : this(element, Table.TryFindHeaderRow(element))
        {
        }

        private Table(IWebElement element, (IWebElement HeaderRow, int SkipRows) tryFindResults)
            : this(element, tryFindResults.HeaderRow, tryFindResults.SkipRows)
        {
        }

        /// <summary>
        /// Crates a new instance of the Table class based on the specified "table" element
        /// </summary>
        /// <param name="element">IWebElement representing the "table" element</param>
        /// <param name="headerRowElement">IWebElement representing the header row of the table.  If the table has no column headers, pass null
        /// to this parameter.  In this case, columns will only be addressable by index.</param>
        /// <param name="skipRows">The number of rows at the top of the table body that do not represent the table content 
        /// (usually because they contain column headers)</param>
        public Table(IWebElement element, IWebElement? headerRowElement, int skipRows)
        {
            if (element.TagName.ToUpperInvariant() != "TABLE")
            {
                throw new ArgumentException("Must specify a <table> element.", nameof(element));
            }

            if (headerRowElement != null && headerRowElement.TagName.ToUpperInvariant() != "TR")
            {
                throw new ArgumentException("Must specify a <tr> element.", nameof(headerRowElement));
            }

            this.Element = element;
            this.HeaderRow = headerRowElement;
            this.SkipRows = skipRows;
            this.RowXPathPrefix = Table.BuildRowXPathPrefix(element, skipRows);
            this.Headers = new ReadOnlyDictionary<string, int>(this.LoadHeaders());
        }

        /// <summary>
        /// Gets all the content rows of the table
        /// </summary>
        /// <returns>Collection of TableRow objects representing all rows in the table</returns>
        public ReadOnlyCollection<TableRow> GetRows()
        {
            IList<TableRow> tableRows = this.Element
                .FindElements(By.XPath(this.RowXPathPrefix))
                .Select(e => new TableRow(e, this.Headers, this.SkipRows))
                .ToList();
            return new ReadOnlyCollection<TableRow>(tableRows);
        }

        /// <summary>
        /// Gets the first row in the table that matches the specified rowQuery string
        /// </summary>
        /// <param name="rowQuery">A RowQuery string identifying one or more rows by their contents</param>
        /// <returns>TableRow representing the first row that matches the specified rowQuery string</returns>
        public TableRow FindRow(string rowQuery)
        {
            string rowXPath = this.BuildXPath(rowQuery);
            IWebElement rowElement = this.Element.FindElement(By.XPath(rowXPath));
            return new TableRow(rowElement, this.Headers, this.SkipRows);
        }

        /// <summary>
        /// Gets all rows in the table that matches the specified rowQuery string
        /// </summary>
        /// <param name="rowQuery">A RowQuery string identifying one or more rows by their contents</param>
        /// <returns>Collection of TableRows representing all rows that match the specified rowQuery string</returns>
        public ReadOnlyCollection<TableRow> FindRows(string rowQuery)
        {
            string rowXPath = this.BuildXPath(rowQuery);
            IList<TableRow> tableRows = this.Element
                .FindElements(By.XPath(rowXPath))
                .Select(e => new TableRow(e, this.Headers, this.SkipRows))
                .ToList();
            return new ReadOnlyCollection<TableRow>(tableRows);
        }

        /// <summary>
        /// Gets the cell in the specified column from the first row that matches the specified rowQuery string
        /// </summary>
        /// <param name="rowQuery">A RowQuery string identifying one or more rows by their contents</param>
        /// <param name="columnHeaderText">Identifies the column under which to find cells by the column's header text</param>
        /// <returns>TableCell representing the cell under the specified column from the specified row</returns>
        public TableCell FindCell(string rowQuery, string columnHeaderText)
        {
            if (!this.Headers.ContainsKey(columnHeaderText))
            {
                throw new ArgumentException(
                    $"The table does not contain a column with the header caption of '{columnHeaderText}'.",
                    nameof(columnHeaderText));
            }

            return this.FindCell(rowQuery, this.Headers[columnHeaderText]);
        }

        /// <summary>
        /// Gets the cell in the specified column from the first row that matches the specified rowQuery string
        /// </summary>
        /// <param name="rowQuery">A RowQuery string identifying one or more rows by their contents</param>
        /// <param name="columnIndex">Identifies the column under which to find cells by the column's index</param>
        /// <returns>TableCell representing the cell under the specified column from the specified row</returns>
        public TableCell FindCell(string rowQuery, int columnIndex)
        {
            string rowXPath = this.BuildXPath(rowQuery);
            int xpathCellIndex = columnIndex + 1;
            string cellXPath = $"{rowXPath}/td[{xpathCellIndex}]";
            IWebElement cellElement = this.Element.FindElement(By.XPath(cellXPath));
            return new TableCell(cellElement, columnIndex, this.SkipRows);
        }

        /// <summary>
        /// Gets the cells in the specified column from the all rows that match the specified rowQuery string
        /// </summary>
        /// <param name="rowQuery">A RowQuery string identifying one or more rows by their contents</param>
        /// <param name="columnHeaderText">Identifies the column under which to find cells by the column's header text</param>
        /// <returns>Collection of TableCells representing the cells under the specified column from the specified rows</returns>
        public ReadOnlyCollection<TableCell> FindCells(string rowQuery, string columnHeaderText)
        {
            if (!this.Headers.ContainsKey(columnHeaderText))
            {
                throw new ArgumentException(
                    $"The table does not contain a column with the header caption of '{columnHeaderText}'.",
                    nameof(columnHeaderText));
            }

            return this.FindCells(rowQuery, this.Headers[columnHeaderText]);
        }

        /// <summary>
        /// Gets the cells in the specified column from the all rows that match the specified rowQuery string
        /// </summary>
        /// <param name="rowQuery">A RowQuery string identifying one or more rows by their contents</param>
        /// <param name="columnIndex">Identifies the column under which to find cells by the column's index</param>
        /// <returns>Collection of TableCells representing the cells under the specified column from the specified rows</returns>
        public ReadOnlyCollection<TableCell> FindCells(string rowQuery, int columnIndex)
        {
            string rowXPath = this.BuildXPath(rowQuery);
            int xpathCellIndex = columnIndex + 1;
            string cellXPath = $"{rowXPath}/td[{xpathCellIndex}]";
            IList<TableCell> tableCells = this.Element
                .FindElements(By.XPath(cellXPath))
                .Select((e, i) => new TableCell(e, i, this.SkipRows))
                .ToList();
            return new ReadOnlyCollection<TableCell>(tableCells);
        }

        private string BuildXPath(string rowQuery)
        {
            return this.BuildXPath(OrGroup.Parse(rowQuery));
        }

        private string BuildXPath(OrGroup orGroup)
        {
            return String.Join(" | ", orGroup.AndGroups.Select(a => this.buildXPath(a)));
        }

        private string buildXPath(AndGroup andGroup)
        {
            IEnumerable<string> predicates = andGroup.Conditions.Select(c => this.buildXPathPredicate(c));
            string[] parts = this.RowXPathPrefix.AppendEnumerable(predicates).ToArray();
            return String.Join("", parts);
        }

        private string buildXPathPredicate(FieldCondition fieldCondition)
        {
            int xpathHeaderIndex = 0;
            if (fieldCondition.IsFieldByIndex)
            {
                xpathHeaderIndex = fieldCondition.FieldIndex!.Value + 1;
            }
            else if (this.Headers.ContainsKey(fieldCondition.Field))
            {
                xpathHeaderIndex = this.Headers[fieldCondition.Field] + 1;
            }
            else
            {
                throw new ArgumentException(
                    $"The table does not contain a column with the header caption of '{fieldCondition.Field}'.", 
                    nameof(fieldCondition));
            }

            return $"[td[{xpathHeaderIndex}]=\"{fieldCondition.Value}\"]";
        }

        /// <summary>
        /// Gets the number of columns in the table
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return this.Headers.Count;
            }
        }

        /// <summary>
        /// Gets the number of content rows in the table
        /// </summary>
        public int RowCount
        {
            get
            {
                return this.Element.FindElements(By.XPath(this.RowXPathPrefix)).Count;
            }
        }

        /// <summary>
        /// Gets the specified column header element
        /// </summary>
        /// <param name="headerText">Identifies the header to get by the header's text</param>
        /// <returns>IWebElement representing the specified column header</returns>
        public IWebElement GetHeader(string headerText)
        {
            if (this.HeaderRow == null)
            {
                throw new InvalidOperationException("This table does not have identified column headers.");
            }

            if (!this.Headers.ContainsKey(headerText))
            {
                throw new ArgumentException(
                    $"The table does not contain a column with the header caption of '{headerText}'.",
                    nameof(headerText));
            }

            return this.GetHeader(this.Headers[headerText]);
        }

        /// <summary>
        /// Get sthe specified column header element
        /// </summary>
        /// <param name="headerIndex">Identifies the header to get by the header's index</param>
        /// <returns>IWebElement representing the specified column header</returns>
        public IWebElement GetHeader(int headerIndex)
        {
            if (this.HeaderRow == null)
            {
                throw new InvalidOperationException("This table does not have identified column headers.");
            }

            int xpathIndex = headerIndex + 1;
            return this.HeaderRow.FindElement(By.XPath($"(/td | /th)[{xpathIndex}]"));
        }
    }
}
