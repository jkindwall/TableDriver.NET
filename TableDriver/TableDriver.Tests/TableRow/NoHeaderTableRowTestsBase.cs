using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using TableDriver.Elements;

namespace TableDriver.Tests
{
    public abstract class NoHeaderTableRowTestsBase : TableRowTestsBase
    {
        protected new void TestFindCell()
        {
            TableSamples.GoToTestPage(this.Driver);
            TableRow row = this.GetTestTableRow();

            TableCell cell = row.FindCell("2");
            Assert.IsNotNull(cell);
            Assert.AreEqual("Medium", cell.Element.Text);

            cell = row.FindCell(1);
            Assert.IsNotNull(cell);
            Assert.AreEqual("Dark Red", cell.Element.Text);

            bool exceptionThrown = false;
            try
            {
                cell = row.FindCell("8");
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
