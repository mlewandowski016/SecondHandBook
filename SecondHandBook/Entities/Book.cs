namespace SecondHandBook.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string? Category { get; set; }
        public int? PageCount { get; set; }
        public int? PublishDate { get; set; }
        public string ISBN { get; set; }
        public string? Description { get; set; }
        public string? Cover { get; set; }
    }
}
