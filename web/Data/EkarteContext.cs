using web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public class EkarteContext : IdentityDbContext<ApplicationUser>
    {
        public EkarteContext(DbContextOptions<EkarteContext> options) : base(options)
        {
        }

        public DbSet<Event>? Events { get; set; }
        public DbSet<ApplicationUser>? ApplicationUsers { get; set; }
        public DbSet<Ticket>? Tickets { get; set; }
        public DbSet<Rating>? Ratings { get; set; }
        public DbSet<Purchase>? Purchases { get; set; }
        public DbSet<Performer>? Performers { get; set; }
        public DbSet<Venue>? Venues { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<Event>()
                .HasKey(e => e.EventID);

            modelBuilder.Entity<Event>()
                .Property(e => e.EventID)
                .ValueGeneratedOnAdd();            

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Performer)
                .WithMany(p => p.Events)
                .HasForeignKey(e => e.PerformerID);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Venue)
                .WithMany(v => v.Events)
                .HasForeignKey(e => e.VenueID);
                
            modelBuilder.Entity<Event>()
                .HasOne(e => e.User)
                .WithMany(v => v.EventsOrganised)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<Event>()
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");

            modelBuilder.Entity<Ticket>().ToTable("Ticket");

            modelBuilder.Entity<Ticket>()
                .HasKey(t => t.TicketID);

            modelBuilder.Entity<Ticket>()
                .Property(t => t.TicketID)
                .ValueGeneratedOnAdd();
                
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Event)
                .WithMany(e => e.Tickets)
                .HasForeignKey(t => t.EventID);
                
            modelBuilder.Entity<Rating>().ToTable("Rating");

            modelBuilder.Entity<Purchase>().ToTable("Purchase");

            /*modelBuilder.Entity<Purchase>()
                .HasKey(p => p.PurchaseID);

            modelBuilder.Entity<Purchase>()
                .Property(p => p.PurchaseID)
                .ValueGeneratedOnAdd();            

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.User)
                .WithMany(u => u.Purchases)
                .HasForeignKey(p => p.PurchaseID);

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Ticket)
                .WithMany(t => t.Purchases)
                .HasForeignKey(p => p.Ticket);*/

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Performer>().ToTable("Performer");

            modelBuilder.Entity<Venue>().ToTable("Venue");
        }
    }
}