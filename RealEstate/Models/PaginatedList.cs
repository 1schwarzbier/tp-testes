namespace RealEstate.Models
{
    public class PaginatedList<T>
    {
        public int PageIndex { get; set; }
        public int PageTotal { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public IEnumerable<T>? Data { get; set; }
    }
}

