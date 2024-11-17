namespace SecondHandBook.Models
{
    public class SearchListResult<T>
    {
        public List<T> Items { get; set; }
        public SearchListResult(List<T> items)
        {
            Items = items;
        }
    }
}
