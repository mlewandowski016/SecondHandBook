using SecondHandBook.Entities;

namespace SecondHandBook.Models
{
    public class DisplayDto
    {
        public int BookId { get; set; }
        public int GiverId { get; set; }
        public int TakerId { get; set; }
        public DateTime DisplayDate { get; set; }
        public bool? IsReserved { get; set; }
        public bool? IsTaken { get; set; }
    }
}
