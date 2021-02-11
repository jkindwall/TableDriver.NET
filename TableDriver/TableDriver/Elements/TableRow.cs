using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TableDriver.Elements
{
    public class TableRow
    {
        public IWebElement Element { get; private init; }

        public TableRow(IWebElement element)
        {
            if (element.TagName.ToUpperInvariant() != "TR")
            {
                throw new ArgumentException("Must specify a <tr> element.", nameof(element));
            }

            this.Element = element;
        }
    }
}
