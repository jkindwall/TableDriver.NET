using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableDriver.RowQuery
{
    internal static class ParseHelper
    {
        public static string[] SplitByToken(string input, char token)
        {
            IEnumerable<int> tokenIndecies = ParseHelper.ScanForTokens(input, token);

            if (!tokenIndecies.Any()) { return new string[] { input }; }

            int subStringStart = 0;
            return tokenIndecies
                .Select(i =>
                {
                    string subString = input.Substring(subStringStart, i - subStringStart);
                    subStringStart = i + 1;
                    return subString;
                })
                .Concat(new string[] { input.Substring(tokenIndecies.Last() + 1) })
                .ToArray();
        }

        private static IEnumerable<int> ScanForTokens(string input, char token)
        {
            List<int> indecies = new();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == token) { indecies.Add(i); }
                if (input[i] == '\\') { i++; }
            }

            return indecies;
        }
    }
}
