namespace SecondHandBook.Entities
{
    public class BookOffer
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int? GiverId { get; set; }
        public int? TakerId { get; set; }
        public virtual Book Book { get; set; }
        public virtual User Giver { get; set; }
        public virtual User? Taker { get; set; }
        public DateTime DateOfOffer { get; set; }
        public bool? IsCollected { get; set; }
    }
}
