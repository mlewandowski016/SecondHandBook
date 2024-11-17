namespace SecondHandBook.Models
{
    public class MyBookDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public int? PagesCount { get; set; }
        public DateTime? PublishDate { get; set; }
        public string ISBN { get; set; }
    }
}
