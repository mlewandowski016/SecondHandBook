namespace SecondHandBook.Entities
{
    public class MyBook
    {
        public int Id { get; set; }
        public virtual Book Book { get; set; }
        public virtual User Owner { get; set; }
        public virtual DateTime AddedDate { get; set; }
    }
}
