# TableDriver .NET
An extension to .NET Selenium WebDriver to support operations on HTML tables.

## Why Use TableDriver?
If you've ever searched the web for something like "How to find a table cell in Selenium WebDriver", you've probably come across one of several articles that teaches you how to build an XPath expression to locate a specific cell in the table.  Ultimately, that ends up looking something like this:

``` csharp
IWebElement cell = driver.FindElement(By.XPath("//table[@id='myTable']/tbody/tr[3]/td[2]"));
string price = cell.Text
```

That example will give you the cell found in the second column of the third row of the table.  That will work okay, however anyone with substantial experience in test automation should notice a distinct "code smell" here.  The table cell you want may be in row 3, column 2 right now.  But what if the data in the table changes?  What if the sorting of the data changes.  What if columns are added, removed, or reordered?

One of the ways to ensure your automated tests require minimal maintenance in order to keep running in the face of an ever-evolving AUT (Application Under Test) is to identify UI elements by contextual information rather than raw, hard-coded indexes.  In other words, rather than looking for the third textbox on the page, its better to look for the textbox with the id of "txtLastName".

To apply this principal to an html table, instead of searching for the third row of the table and then finding the 2nd cell in that row, you would first look for the row that has a value of "ABC123" in the "Product Id" column, and then finding the cell from that row that corresponds to the "Price" column.  While it is technically possible to do that with an XPath string, it requires some advanced understanding of the XPath language to formulate such a string, and the result would be rather long, and difficult to read.

The goal of the TableDriver package is to make the process of locating specific contents of an html table by such contextual information much easier by supporting a simple query string syntax for identifying a specific row (or rows) in the table.  Using the TableDriver package the code snippet above could be rewritten as follows:

``` csharp
Table table = Table.Create(driver.FindElement(By.Id("myTable")));
TableCell cell = table.FindCell("Product Id=ABC123", "Price")
string price = cell.Element.Text;
```

# Install
Search for TableDriver.NET in the NuGet Package Manager in Visual Studio, or use you preferred command line tool to install the NuGet package.

Package Manager Console
```
Install-Package TableDriver.NET
```

.NET CLI
```
dotnet add package TableDriver.NET
```

nuget.exe
```
nuget install TableDriver.NET
```

# Usage
## Creating the Table object
While the somewhat open-ended nature of the structure of the html table tag makes it difficult to support every possible table implementation, TableDriver strives to support several of the most common table structures and offers several versions of the "Create" method allowing you to specify how to interpret the table in your AUT.

### Create(IWebElement element)
Example:
``` csharp
Table table = Table.Create(driver.FindElement(By.Id("myTable")));
```

This method will attempt to infer the structure of the table by the presence of certain tag types within the \<table\> element.  The key issue is to find the column headers of the table, so that the table's contents can be queried based on values found in each column. 

The Create(...) method will first look for a \<thead\> elment.  If found, column headers will be read from the last row under the \<thead\> element.  This will work for tables structured like this one:

``` html
    <table id="myTable">
        <thead>
            <tr class="title-row"><th colspan="5">Fruits</th></tr>
            <tr class="header-row"><th>Name</th><th>Color</th><th>Rating</th></tr>
        </thead>
        <tbody>
            <tr class="data-row"><th>Apple</th><td>Red</td><td>5.0</td></tr>
            ...
        </tbody>
    </table>
```

If no \<thead\> is found, it will look for any \<tr\> elements that only contain \<th\> elements.  If any such rows are found column headers are read from the last such row found in the table, and all such rows will be excluded from the "content" region of the table.  Here is an example of such a table:

``` html
    <table id="myTable">
        <tr class="title-row"><th colspan="5">Fruits</th></tr>
        <tr class="header-row"><th>Name</th><th>Color</th><th>Rating</th></tr>
        <tr class="data-row"><th>Apple</th><td>Red</td><td>5.0</td></tr>
        ...
    </table>
```

If no \<thead\> element is found, and there are no \<tr\> elements that only contain \<th\> elements, the basic Create(...) method will finally resort to assuming that the first \<tr\> element in the table contains the column headers.  Header values will be read from the first row, and that row will be excluded from the "content" region of the table.  This would be applicable to a table structured like this:

``` html
    <table id="myTable">
        <tbody>
            <tr class="header-row"><td>Name</td><td>Color</td><td>Rating</td></tr>
            <tr class="data-row"><td>Apple</td><td>Red</td><td>5.0</td></tr>
            ...
        </tbody>
    </table>
```

### CreateWithHeaderRow(IWebElement element, IWebElement headerRowElement, int skipRows)
Example:
``` csharp
Table table = Table.CreateWithHeaderRow(
    driver.FindElement(By.Id("myTable")),
    driver.FindElement(By.CssSelector("#myTable tr.header-row")),
    2);
```

If the simple Create(...) method is incapable of automatically identifying the header row in your table, you can use the CreateWithHeaderRow(...) to explicitly identify the header row.  You can also specify how many rows at the top of your table should not be considered part of the content region (data rows) in your table using the skipRows parameter.  The above call to CreateWithHeaderRow(...) would be appropriate for a table like this one:

``` html
    <table id="myTable">
        <tr class="title-row"><th colspan="5">Fruits</th></tr>
        <tr class="header-row"><th>Name</th><td>Color</td><td>Rating</td></tr>
        <tr class="data-row"><th>Apple</th><td>Red</td><td>5.0</td></tr>
        ...
    </table>
```

### CreateWithExternalHeaders(IWebElement element, IReadOnlyList<IWebElement> headerElements, int skipRows)
Example:
``` csharp
Table table = Table.CreateWithExternalHeaders(
    driver.FindElement(By.Id("myTable")),
    driver.FindElements(By.CssSelector("div.table-headers > span")),
    0);
```

Sometimes web UI designers like to put the headers for a table outside the main \<table\> element.  The table headers might be in some sort of structure made up of \<div\> and/or \<span\> tags, or they may be located within a totally separate \<table\> element.  In this situation you will have to supply the full collection of header elements when you create the table object (typically with a call to driver.FindElements(...)).  Use the CreateWithExternalHeaders(...) method when working with a table like this one:

``` html
    <div class="table-headers"><span>Name</span><span>Color</span><span>Rating</span></div>
    <table id="myTable">
        <tr class="data-row"><th>Apple</th><td>Red</td><td>5.0</td></tr>
        ...
    </table>
```

### CreateWithNoHeaders(IWebElement element, int skipRows)
Example:
``` csharp
Table table = Table.CreateWithNoHeaders(driver.FindElement(By.Id("myTable")), 1);
```

Occasinally you may have a table that simply does not have any headers at all.  In this case you will not have any choice but to reference columns by index.  When you use the CreateWithNoHeaders(...) method, the Table object's internal list of header strings will be automatically populated with column indexes ("0", "1", "2", ...).  Here is an example of a table with no headers:

``` html
    <table id="myTable">
        <tr class="title-row"><th colspan="5">Fruits</th></tr>
        <tr class="data-row"><th>Apple</th><td>Red</td><td>5.0</td></tr>
        ...
    </table>
```

## Row Queries
### Basic Structure
TableDriver uses a simple query syntax similar to a URL query string to locate specific rows in a table.  A row query consists of one or more field conditions separated by boolean AND (&) and OR (|) operators.

A "field condition" consists of a field name (typically the header text of the corresponding column) followed by a comparison operator (usually the '=' character) followed by a comparison value:
```
Name=Apple
```

The following operators are supported in a field condition:

|Operator|Description|Example|
|:--:|--|--|
|= |equal|```Name=Apple```|
|!=|not equal|```Color!=Orange```|
|< |less than|```Price<2.99```|
|<=|less than or equal|```Weight<=5```|
|> |greater than|```Length>16.02```|
|>=|greater than or equal|```Quantity>=12```|
|^=|starts with|```Name^=Pine```|
|*=|contains|```Name*=apefrui```|

Multiple field conditions can be combined into a single Row query using boolean operators:
```
Color=Red&Rating=5.0|Name=Orange
```

### Order of Operations
The order in which operators are evaluated in a Row Query is as follows:
1. =, !=, <, <=, >, >=, ^=, *=
2. &
3. |
 
### Field Name by Index
Occasionally you may have a need to identify a column in your row query by index instead of by header text.  You can do this by starting the field name portion of your field condition with a backslash (\\), and following it with the 0-based numeric index of the column:
```
\0=Apple
```

### Escape Sequences
Table driver supports the following escape sequences in order to support including the special characters (\\, =, &, |, etc.) in the literal field name or expected value portions of a field condition:
```
\\  \=  \!  \<  \>  \^  \*  \&  \|
```

### Supporting Methods
The following methods of the Table class support the use of row queries to identify one or more rows in the table:
``` csharp
public TableRow FindRow(string rowQuery);
public ReadOnlyCollection<TableRow> FindRows(string rowQuery);
public TableCell FindCell(string rowQuery, string columnHeaderText);
public TableCell FindCell(string rowQuery, int columnIndex);
public ReadOnlyCollection<TableCell> FindCells(string rowQuery, string columnHeaderText);
public ReadOnlyCollection<TableCell> FindCells(string rowQuery, int columnIndex);
```

### Duplicate Headers
In the case of a table that has multiple column headers with the same inner text, the columns will be differentiated anywhere column headers are referenced by appending a numeric index to each header after the first one.  For example, if a table has three column headers that all contain the text "Color", TableDriver will reference these columns as "Color", "Color-1", and "Color-2".  Example:
``` csharp
// Finds the cell in the third "Color" column from the row that has the text "Crimson" in the second "Color" column.
TableCell cell = table.FindCell("Color-1=Crimson", "Color-2");
```

## Not Supported

### colspan/rowspan > 1
If any \<th\> or \<td\> elements in the header row or the data rows specify a colspan or rowspan attribute with a value other than 1, it may cause unpredictable results when using TableDriver.  The Table object assumes that the header row and all data rows in a table have exactly the same number of cells.  Colspan/rowspan cells may throw off the 1:1 matching of column headers to row cells.  If the only such cells in the table are in rows not considered part of the content area and are not part of the header row (such as the title row found in several of the above example tables) this should not pose a problem for the TableDriver Table object.

### Footers
TableDriver currently does not support any operations specific to table footers.  If a table includes footer rows completely contained within a \<tfoot\> tag, these footer rows will simply be ignored.  If, however, a table contains footer rows within the \<tbody\> tag or as direct children of the \<table\> tag when no \<tbody\> tag is present, these rows will be considered part of the table's content region and will be considered by calls to methods and properties like RowCount and FindRow, so tests on tables with this type of footer row should take this into account.

## To Do
The following features are being considered to be supported in the future:

### Table contents verification.
I'd like to implement some sort of method for doing mass verification of part or all of the data in a table.  The tricky part is figuring out the best way to communicate the verification results.

### Support other languages
I will very likely port this package to Java at some point in the near future.  A javascript version is also a possibility.  However, my experience with other languages supported by Selenium (Python, Perl, PHP, Ruby) is limited, making those far less likely at the moment.  However, I fully support anyone interested in implementing their own ports to one of those languages.