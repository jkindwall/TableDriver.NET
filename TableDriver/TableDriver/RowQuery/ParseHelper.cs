using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TableDriver.Extensions;

namespace TableDriver.RowQuery
{
    internal static class ParseHelper
    {
        public static string[] SplitByTokens(string input, params string[] tokens)
        {
            IEnumerable<(int TokenIndex, int TokenLength)> tokenIndecies = ParseHelper.ScanForTokens(input, tokens);

            if (!tokenIndecies.Any()) { return new string[] { input }; }

            int subStringStart = 0;
            return tokenIndecies
                .SelectMany(i =>
                {
                    string subString = input.Substring(subStringStart, i.TokenIndex - subStringStart);
                    string tokenString = input.Substring(i.TokenIndex, i.TokenLength);
                    subStringStart = i.TokenIndex + i.TokenLength;
                    return new string[] { subString, tokenString };
                })
                .AppendItem(input.Substring(tokenIndecies.Last().TokenIndex + tokenIndecies.Last().TokenLength))
                .ToArray();
        }

        public static string[] SplitByToken(string input, char token)
        {
            return ParseHelper.SplitByTokens(input, $"{token}").Where((_, i) => i % 2 == 0).ToArray();
        }

        private static IEnumerable<(int TokenIndex, int TokenLength)> ScanForTokens(string input, params string[] tokens)
        {
            List<(int TokenIndex, int TokenLength)> indecies = new();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\\') { i++; }
                else if (tokens.Any(t => t[0] == input[i])) 
                {
                    string substring = input.Substring(i);
                    int tokenLength = tokens
                        .Where(t => substring.StartsWith(t))
                        .Select(t => t.Length)
                        .AppendItem(0)
                        .Max();

                    if (tokenLength > 0)
                    {
                        indecies.Add((i, tokenLength));
                        i += (tokenLength - 1);
                    }
                }
            }

            return indecies;
        }
    }
}
