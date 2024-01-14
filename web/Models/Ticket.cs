namespace web.Models
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public int EventID { get; set; }
        public Event Event { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}