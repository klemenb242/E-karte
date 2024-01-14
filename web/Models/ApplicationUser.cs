using Microsoft.AspNetCore.Identity;

namespace web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public ICollection<Event>? EventsOrganised { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        public ICollection<Purchase>? Purchases { get; set; }

    }
}