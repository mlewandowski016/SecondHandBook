namespace SecondHandBook.Entities
{
    public class Display
    {
        public Guid Id { get; set; }
        public virtual Book Book { get; set; }
        public virtual User Giver { get; set; }
        public virtual User Taker { get; set; }
        public DateTime DisplayDate { get; set; }
        public bool IsReserved { get; set; }
    }
}
