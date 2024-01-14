namespace web.Models
{
    public class Performer
    {
        public int PerformerID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}