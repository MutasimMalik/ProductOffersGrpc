namespace Admin.API.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public string? OfferDescription { get; set; }
    }
}
