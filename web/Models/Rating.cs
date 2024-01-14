namespace web.Models
{
    public class Rating
    {
        public int RatingID { get; set; }
        public double Value { get; set; }
        public string UserID { get; set; }
        public int EventID { get; set; }
        public ApplicationUser User { get; set; }
        public Event Event { get; set; }
    }
}