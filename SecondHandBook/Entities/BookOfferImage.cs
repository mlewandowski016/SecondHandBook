namespace SecondHandBook.Entities
{
    public class BookOfferImage
    {
        public int Id { get; set; }
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }
        public int BookOfferId { get; set; }
        public BookOffer BookOffer { get; set; }
    }
}
