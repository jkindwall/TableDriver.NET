using System;
using System.Text.RegularExpressions;

namespace TableDriver.RowQuery
{
    internal class FieldCondition
    {
        public string Field { get; private init; }

        public string Value { get; private init; }

        public int? FieldIndex { get; private init; }

        public bool IsFieldByIndex
        {
            get
            {
                return this.FieldIndex.HasValue;
            }
        }

        private FieldCondition(string field, string value, int? fieldIndex)
        {
            this.Field = field;
            this.Value = value;
            this.FieldIndex = fieldIndex;
        }

        public static FieldCondition Parse(string conditionString)
        {
            string[] parts = ParseHelper.SplitByToken(conditionString, '=');

            if (parts.Length != 2)
            {
                throw new FormatException("Each condition must be in the format <field name>=<value> with the following characters escaped: \\ = & |");
            }

            Regex regex = new Regex(@"\\(.)");
            string value = regex.Replace(parts[1], "$1");

            Match fieldByIndexMatch = Regex.Match(parts[0], @"^\\(\d+)$");
            if (fieldByIndexMatch.Success)
            {
                string field = fieldByIndexMatch.Groups[1].Value;
                int fieldIndex = Int32.Parse(field);
                return new FieldCondition(field, value, fieldIndex);
            }
            else
            {
                string field = regex.Replace(parts[0], "$1");
                return new FieldCondition(field, value, null);
            }
        }
    }
}
