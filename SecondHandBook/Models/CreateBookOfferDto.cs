using SecondHandBook.Entities;

namespace SecondHandBook.Models
{
    public class CreateBookOfferDto
    {
        public int BookId { get; set; }
        public string OfferDescription { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
