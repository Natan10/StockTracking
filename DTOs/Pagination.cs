namespace StockTracking.DTOs
{
    public class Pagination<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public List<T> Items { get; set; }
    }
}
