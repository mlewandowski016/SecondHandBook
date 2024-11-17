namespace SecondHandBook.Models
{
    public class UpdateBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public int? PagesCount { get; set; }
        public int? PublishDate { get; set; }
    }
}
