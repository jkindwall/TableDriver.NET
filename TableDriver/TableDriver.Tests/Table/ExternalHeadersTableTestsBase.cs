using Microsoft.VisualStudio.TestTools.UnitTesting;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    public abstract class ExternalHeadersTableTestsBase : TableTestsBase
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
            Assert.AreEqual(5, table.HeaderElements.Count);
        }
    }
}
