using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
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
    }
}
