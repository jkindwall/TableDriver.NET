namespace TableDriver.RowQuery
{
    internal static class FieldOperation
    {
        public static string[] AllOperations = new string[]
        {
            FieldOperation.Equal,
            FieldOperation.NotEqual,
            FieldOperation.LessThan,
            FieldOperation.LessThanOrEqual,
            FieldOperation.GreaterThan,
            FieldOperation.GreaterThanOrEqual,
            FieldOperation.StartsWith,
            FieldOperation.Contains
        };

        public const string Equal = "=";
        public const string NotEqual = "!=";
        public const string LessThan = "<";
        public const string LessThanOrEqual = "<=";
        public const string GreaterThan = ">";
        public const string GreaterThanOrEqual = ">=";
        public const string StartsWith = "^=";
        public const string Contains = "*=";
    }
}
