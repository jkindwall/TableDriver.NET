using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableDriver.Elements
{
    public class Table
    {
        private IDictionary<string, int> HeadersByText { get; init; }
        private IList<string> HeadersByIndex { get; init; }
        private string RowXPathPrefix { get; init; }

        public IWebElement Element { get; private init; }

        public TableRow? HeaderRow { get; private init; }

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
                builder.Append($"/tr[{skipRows}]/following-sibling::");
            }
            else
            {
                builder.Append("/");
            }

            return builder.ToString();
        }

        public Table(IWebElement element)
            : this(element, Table.TryFindHeaderRow(element))
        {
        }

        private Table(IWebElement element, (IWebElement HeaderRow, int SkipRows) tryFindResults)
            : this(element, tryFindResults.HeaderRow, tryFindResults.SkipRows)
        {
        }

        public Table(IWebElement element, IWebElement? headerRowElement, int skipRows)
        {
            if (element.TagName.ToUpperInvariant() != "TABLE")
            {
                throw new ArgumentException("Must specify a <table> element.", nameof(element));
            }

            this.Element = element;
            this.HeaderRow = headerRowElement == null ? null : new TableRow(headerRowElement);
            this.HeadersByText = new Dictionary<string, int>();
            this.HeadersByIndex = new List<string>();
            this.RowXPathPrefix = Table.BuildRowXPathPrefix(element, skipRows);
            this.InitHeaders();
        }

        private void InitHeaders()
        {
            if (this.HeaderRow == null)
            {
                IList<IWebElement> columns = this.Element.FindElements(By.XPath($"{this.RowXPathPrefix}tr/th | {this.RowXPathPrefix}tr/td"));
                for (int i = 0; i < columns.Count; i++)
                {
                    string indexText = i.ToString();
                    this.HeadersByText[indexText] = i;
                    this.HeadersByIndex.Add(indexText);
                }
            }
            else
            {
                IList<IWebElement> headers = this.HeaderRow.Element.FindElements(By.XPath("/td | /th"));
                foreach (IWebElement header in headers)
                {
                    this.HeadersByText[header.Text] = this.HeadersByIndex.Count;
                    this.HeadersByIndex.Add(header.Text);
                }
            }
        }
    }
}
