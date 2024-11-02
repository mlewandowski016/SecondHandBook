using SecondHandBook.Entities;

namespace SecondHandBook.Models
{
    public class BookOfferDto
    {
        public int BookId { get; set; }
        public int GiverId { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
        public DateTime DisplayDate { get; set; }
    }
}
