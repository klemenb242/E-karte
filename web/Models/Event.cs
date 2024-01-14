using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace web.Models
{
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EventID { get; set; }
        public string? Title { get; set; }
        public DateTime? Date { get; set; }
        public string? UserID { get; set; }
        public int? VenueID { get; set; }
        public int? PerformerID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
        public ApplicationUser? User { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        public Venue? Venue { get; set; }
        public Performer? Performer { get; set; }
        public ICollection<Ticket>? Tickets  { get; set; }
    }
}