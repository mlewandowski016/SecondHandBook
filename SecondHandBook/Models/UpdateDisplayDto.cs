using SecondHandBook.Entities;

namespace SecondHandBook.Models
{
    public class UpdateDisplayDto
    {
        public virtual Book Book { get; set; }
        public virtual User? Taker { get; set; }
        public bool? IsReserved { get; set; }
        public bool? IsTaken { get; set; }
    }
}
