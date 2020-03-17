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

        public DbSet<EventModel> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventModel>().ToTable("Event");
        }
    }
}
