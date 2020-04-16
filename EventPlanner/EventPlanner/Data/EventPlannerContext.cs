using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Models;

namespace EventPlanner.Data
{
    public class EventPlannerContext : DbContext
    {
        public EventPlannerContext(DbContextOptions<EventPlannerContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Categorie> Categorie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Coach>().ToTable("Coach");
            modelBuilder.Entity<Rating>().ToTable("Rating");
            modelBuilder.Entity<Registration>().ToTable("Registration");
            modelBuilder.Entity<Categorie>().ToTable("Categorie");
        }
    }
}
