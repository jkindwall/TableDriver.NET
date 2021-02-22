using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TableDriver.Elements
{
    /// <summary>
    /// Class rerpresenting a table row html element and all its contents
    /// </summary>
    public class TableRow
    {
        private IDictionary<string, int> Headers { get; init; }
        private int SkipRows { get; init; }

        /// <summary>
        /// IWebElement representing the "tr" element
        /// </summary>
        public IWebElement Element { get; private init; }

        internal TableRow(IWebElement element, IDictionary<string, int> headers, int skipRows)
        {
            if (element.TagName.ToUpperInvariant() != "TR")
            {
                throw new ArgumentException("Must specify a <tr> element.", nameof(element));
            }

            this.Element = element;
            this.Headers = headers;
            this.SkipRows = skipRows;
        }

        /// <summary>
        /// Gets the number of cells in the row
        /// </summary>
        public int CellCount
        {
            get
            {
                return this.Element.FindElements(By.XPath("th | td")).Count;
            }
        }

        /// <summary>
        /// Gets the index of the row in the content region of the table.
        /// </summary>
        public int RowIndex
        {
            get
            {
                int precedingRowCount = this.Element.FindElements(By.XPath("preceding-sibling::tr")).Count;
                return precedingRowCount - this.SkipRows;
            }
        }

        /// <summary>
        /// Gets all TableCells in the row
        /// </summary>
        /// <returns>Collection of TableCells representing all cells in the row</returns>
        public ReadOnlyCollection<TableCell> GetCells()
        {
            IList<TableCell> tableCells = this.Element
                .FindElements(By.XPath("th | td"))
                .Select((e, i) => new TableCell(e, i, this.SkipRows))
                .ToList();
            return new ReadOnlyCollection<TableCell>(tableCells);
        }

        /// <summary>
        /// Gets the cell under the specified column from this row
        /// </summary>
        /// <param name="columnHeaderText">Identifies the column under which to find cells by the column's header text</param>
        /// <returns>TableCell representing the cell under the specified column from this row</returns>
        public TableCell FindCell(string columnHeaderText)
        {
            if (!this.Headers.ContainsKey(columnHeaderText))
            {
                throw new ArgumentException(
                    $"The table does not contain a column with the header caption of '{columnHeaderText}'.",
                    nameof(columnHeaderText));
            }

            return this.FindCell(this.Headers[columnHeaderText]);
        }

        /// <summary>
        /// Gets the cell under the specified column from this row
        /// </summary>
        /// <param name="columnIndex">Identifies the column under which to find cells by the column's index</param>
        /// <returns>TableCell representing the cell under the specified column from this row</returns>
        public TableCell FindCell(int columnIndex)
        {
            int xpathCellIndex = columnIndex + 1;
            return new TableCell(
                this.Element.FindElement(By.XPath($"*[self::th or self::td][{xpathCellIndex}]")), 
                columnIndex, 
                this.SkipRows);
        }
    }
}
