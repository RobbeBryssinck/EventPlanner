using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EventPlanner.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace EventPlanner.Data
{
    public class EventPlannerContext : IdentityDbContext<ApplicationUser>
    {
        public EventPlannerContext(DbContextOptions<EventPlannerContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Categorie> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<Coach>().ToTable("Coach");
            modelBuilder.Entity<Rating>().ToTable("Rating");
            modelBuilder.Entity<Registration>().ToTable("Registration");
            modelBuilder.Entity<Categorie>().ToTable("Categorie");
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Rockstar", NormalizedName = "Rockstar".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "User".ToUpper() });
        }
    }
}
