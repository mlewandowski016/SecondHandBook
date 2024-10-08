namespace SecondHandBook.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public BookCategory Category { get; set; }
        public int? PagesCount { get; set; }
        public DateTime? PublishDate { get; set; }
    }
}
