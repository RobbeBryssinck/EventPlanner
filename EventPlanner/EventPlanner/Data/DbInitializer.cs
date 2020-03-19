using EventPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EventPlannerContext context)
        {
            context.Database.EnsureCreated();

            // Look for any events.
            if (context.Events.Any())
            {
                return;   // DB has been seeded
            }

            var events = new Event[]
            {
                new Event{EventName="ASP.NET Core cursus",Date=DateTime.Parse("2020-03-16"),VisitorLimit=100,
                    Description="Lorem ipsum",Location="Eindhoven",Email="test@test.com",ImageSrc="test.jpg"},
            };
            foreach (Event e in events)
            {
                context.Events.Add(e);
            }
            context.SaveChanges();
        }
    }
}
