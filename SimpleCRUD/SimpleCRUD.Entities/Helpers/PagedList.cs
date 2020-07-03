using System.Collections.Generic;

namespace SimpleCRUD.Entities.Helpers
{
    public class PagedList<T>
    {
        public int count { get; set; }
        public IEnumerable<T> results { get; set; }
    }
}
