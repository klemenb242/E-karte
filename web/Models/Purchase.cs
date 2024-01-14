namespace web.Models
{
    public class Purchase
    {
        public int PurchaseID { get; set; }
        public int TicketID { get; set; }
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
        public Ticket Ticket { get; set; }
    }
}