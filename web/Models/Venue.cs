namespace web.Models
{
    public class Venue
    {
        public int VenueID { get; set; }
        public string Name { get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}