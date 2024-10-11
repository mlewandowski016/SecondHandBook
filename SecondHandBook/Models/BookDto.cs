namespace SecondHandBook.Models
{
    public class BookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public int? PagesCount { get; set; }
        public DateTime? PublishDate { get; set; }
    }
}
