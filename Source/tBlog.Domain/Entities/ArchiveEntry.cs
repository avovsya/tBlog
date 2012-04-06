using System.Collections.Generic;

namespace tBlog.Domain.Entities
{
    public class YearArchiveEntry
    {
        public int Year { get; set; }
        public IEnumerable<MonthArchiveEntry> MonthArchiveEntries { get; set; }
    }

    public class MonthArchiveEntry
    {
        public int Month { get; set; }
        public IEnumerable<Entry> Entries { get; set; }
    }
}
