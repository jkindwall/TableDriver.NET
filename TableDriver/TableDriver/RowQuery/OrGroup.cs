using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TableDriver.RowQuery
{
    internal class OrGroup
    {
        public IReadOnlyCollection<AndGroup> AndGroups { get; private init; }

        private OrGroup(IList<AndGroup> andGroups)
        {
            this.AndGroups = new ReadOnlyCollection<AndGroup>(andGroups);
        }

        public static OrGroup Parse(string orGroupString)
        {
            string[] andGroupStrings = ParseHelper.SplitByToken(orGroupString, '|');

            IList<AndGroup> groups = andGroupStrings
                .Select(ags => AndGroup.Parse(ags))
                .ToList();

            return new OrGroup(groups);
        }
    }
}
