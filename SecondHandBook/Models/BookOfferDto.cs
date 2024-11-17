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
        public string Category { get; set; }
        public int? PagesCount { get; set; }
        public int? PublishDate { get; set; }
        public string ISBN { get; set; }
        public int GiverId { get; set; }
        public string Name { get; set; }
        public string Lastname  { get; set; }
        public List<ImageDataDto> Images { get; set; }
        public string BookDescription { get; set; }
        public string OfferDescription { get; set; }
        public int TakerId { get; set; }
        public bool isCollected { get; set; }
    }
}
