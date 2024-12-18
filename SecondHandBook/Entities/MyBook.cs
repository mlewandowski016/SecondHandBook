namespace SecondHandBook.Entities
{
    public class MyBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int? OwnerId { get; set; }
        public virtual Book Book { get; set; }
        public virtual User Owner { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
