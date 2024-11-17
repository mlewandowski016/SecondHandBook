namespace SecondHandBook.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string? Category { get; set; }
        public int? PageCount { get; set; }
        public int? PublishDate { get; set; }
        public string ISBN { get; set; }
    }
}
