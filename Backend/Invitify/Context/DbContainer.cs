using Invitify.Entities;
using Invitify.Privilage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Invitify.Context
{
    public class DbContainer : IdentityDbContext<ExtendIdentityUser, IdentityRole, string>
    {
        public DbContainer(DbContextOptions<DbContainer> ops): base(ops)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }

        public DbSet<Contact> contact { get; set; }

        public DbSet<Country> country { get; set; }

        public DbSet<State> state { get; set; }

        public DbSet<ContactType> contactType { get; set; }

        public DbSet<EventDates> eventDates { get; set; }

        public DbSet<EventGallery> eventGallery { get; set; }

        public DbSet<EventSchedule> eventSchedule { get; set; }

        public DbSet<EventSpeakers> eventSpeakers { get; set; }

        public DbSet<EventSponsors> eventSponsors { get; set; }

        public DbSet<Eventt> eventt { get; set; }

        public DbSet<Properties> properties { get; set; }

        public DbSet<image> image { get; set; }

        public DbSet<TempEventt> tempEventt { get; set; }

        public DbSet<TempEventDates> tempEventDates { get; set; }

        public DbSet<TempEventSchedule> tempEventSchedule { get; set; }

        public DbSet<TempEventSpeakers> tempEventSpeaker { get; set; }

        public DbSet<TempEventSponsors> tempEventSponsor { get; set; }

        public DbSet<TempEventGallery> tempEventGallery { get; set; }

        public DbSet<Invitees> invitees { get; set; }

        public DbSet<Logo> logo { get; set; }

        public DbSet<Registration> registration { get; set; }

        public DbSet<EventEntryOrganizer> eventEntryOrganizer { get; set; }

        public DbSet<Attendance> attendance { get; set; }
    }
}
