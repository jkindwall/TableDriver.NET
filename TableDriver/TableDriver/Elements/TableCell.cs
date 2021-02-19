using OpenQA.Selenium;
using System;

namespace TableDriver.Elements
{
    /// <summary>
    /// Class rerpresenting a table cell html element
    /// </summary>
    public class TableCell
    {
        /// <summary>
        /// Gets the index of the column under which this cell is found
        /// </summary>
        public int ColumnIndex { get; private init; }

        /// <summary>
        /// IWebElement representing the "td" element
        /// </summary>
        public IWebElement Element { get; private init; }

        private int SkipRows { get; init; }

        internal TableCell(IWebElement element, int columnIndex, int skipRows)
        {
            if (element.TagName.ToUpperInvariant() != "TD")
            {
                throw new ArgumentException("Must specify a <td> element.", nameof(element));
            }

            this.Element = element;
            this.ColumnIndex = columnIndex;
            this.SkipRows = skipRows;
        }

        /// <summary>
        /// Gets the index of this cell's row in the content region of the table.
        /// </summary>
        public int RowIndex
        {
            get
            {
                int precedingRowCount = this.Element.FindElements(By.XPath("../preceding-sibling::tr")).Count;
                return precedingRowCount - this.SkipRows;
            }
        }
    }
}
