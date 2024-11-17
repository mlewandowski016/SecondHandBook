using SecondHandBook.Entities;

namespace SecondHandBook.Models
{
    public class UpdateBookOfferDto
    {
        public string OfferDescription { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
