using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TableDriver.RowQuery
{
    internal class AndGroup
    {
        public IReadOnlyCollection<FieldCondition> Conditions { get; private init; }

        private AndGroup(IList<FieldCondition> conditions)
        {
            this.Conditions = new ReadOnlyCollection<FieldCondition>(conditions);
        }

        public static AndGroup Parse(string andGroupString)
        {
            string[] conditionStrings = ParseHelper.SplitByToken(andGroupString, '&');

            IList<FieldCondition> conditions = conditionStrings
                .Select(cs => FieldCondition.Parse(cs))
                .ToList();

            return new AndGroup(conditions);
        }
    }
}
