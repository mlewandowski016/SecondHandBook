using SecondHandBook.Entities;

namespace SecondHandBook.Models
{
    public class BookOfferDto
    {
        public int Id { get; set; }
        public DateTime DateOfOffer { get; set; }
        public int BookId { get; set; }
        public string Title{ get; set; }
        public string Author { get; set; }
        public int Category { get; set; }
        public int? PagesCount { get; set; }
        public DateTime? PublishDate { get; set; }
        public string ISBN { get; set; }
        public int GiverId { get; set; }
        public string Name { get; set; }
        public string Lastname  { get; set; }
    }
}
