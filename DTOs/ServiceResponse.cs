namespace StockTracking.DTOs
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = false;

        public IEnumerable<string>? Errors { get; set; }

    }
}
