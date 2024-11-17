namespace SecondHandBook.Models
{
    public class SearchQuery
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
