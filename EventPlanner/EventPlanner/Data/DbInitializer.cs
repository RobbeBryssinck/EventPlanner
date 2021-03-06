﻿using EventPlanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Data
{
    public static class DbInitializer
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.Users.Any())
                return;

            if (userManager.FindByEmailAsync("admin@rockstarsit.nl").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@rockstarsit.nl",
                    FirstName = "Admin",
                    LastName = "Admin",
                    DateOfBirth = DateTime.Parse("1970-01-01"),
                };

                IdentityResult result = userManager.CreateAsync(user, "rockstarsitadmin").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.FindByEmailAsync("jeroen@rockstarsit.nl").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "jeroen",
                    Email = "jeroen@rockstarsit.nl",
                    FirstName = "Jeroen",
                    LastName = "de Deken",
                    DateOfBirth = DateTime.Parse("1970-01-01"),
                };

                IdentityResult result = userManager.CreateAsync(user, "jeroen1").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Rockstar").Wait();
                }
            }

            if (userManager.FindByEmailAsync("henk3345@gmail.nl").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "henk",
                    Email = "henk3345@gmail.nl",
                    FirstName = "Henk",
                    LastName = "de Vries",
                    DateOfBirth = DateTime.Parse("1970-01-01"),
                };

                IdentityResult result = userManager.CreateAsync(user, "henk11").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }

            }
            if (userManager.FindByEmailAsync("robbe2000@gmail.nl").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "robbe",
                    Email = "robbe2000@gmail.nl",
                    FirstName = "Robbe",
                    LastName = "Bryssinck",
                    DateOfBirth = DateTime.Parse("1970-01-01"),
                };

                IdentityResult result = userManager.CreateAsync(user, "robbe11").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
            if (userManager.FindByEmailAsync("dennydevito@gmail.nl").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "denny",
                    Email = "dennydevito@gmail.nl",
                    FirstName = "Denny",
                    LastName = "Cox",
                    DateOfBirth = DateTime.Parse("1970-01-01"),
                };

                IdentityResult result = userManager.CreateAsync(user, "denny12").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
            if (userManager.FindByEmailAsync("finn568@gmail.nl").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "finn",
                    Email = "finn5680@gmail.nl",
                    FirstName = "Finn",
                    LastName = "van den Broek",
                    DateOfBirth = DateTime.Parse("1970-01-01"),
                };

                IdentityResult result = userManager.CreateAsync(user, "finn2020").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
            if (userManager.FindByEmailAsync("lars2000@gmail.nl").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "lars",
                    Email = "lars2000@gmail.nl",
                    FirstName = "Lars",
                    LastName = "van den Brandt",
                    DateOfBirth = DateTime.Parse("1970-01-01"),
                };

                IdentityResult result = userManager.CreateAsync(user, "lars156").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }

            }
            if (userManager.FindByEmailAsync("dennis808@gmail.nl").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "dennis",
                    Email = "dennis808@gmail.nl",
                    FirstName = "Dennis",
                    LastName = "Cools",
                    DateOfBirth = DateTime.Parse("1970-01-01"),
                };

                IdentityResult result = userManager.CreateAsync(user, "dennis808").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }

            }
            if (userManager.FindByEmailAsync("dylan53@gmail.nl").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "dylan",
                    Email = "dylan53@gmail.nl",
                    FirstName = "Dylan",
                    LastName = "Aarts",
                    DateOfBirth = DateTime.Parse("1970-01-01"),
                };

                IdentityResult result = userManager.CreateAsync(user, "dylan556").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }

            }
            if (userManager.FindByEmailAsync("bob85@gmail.nl").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "bob",
                    Email = "bob85@gmail.nl",
                    FirstName = "Bob",
                    LastName = "van de Ven",
                    DateOfBirth = DateTime.Parse("1970-01-01"),
                };

                IdentityResult result = userManager.CreateAsync(user, "bob5623").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }

            }
            if (userManager.FindByEmailAsync("david@rockstarsit.nl").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "david",
                    Email = "david@rockstarsit.nl",
                    FirstName = "David",
                    LastName = "Chang",
                    DateOfBirth = DateTime.Parse("1970-01-01"),
                };

                IdentityResult result = userManager.CreateAsync(user, "david1").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Rockstar").Wait();
                }
            }
            if (userManager.FindByEmailAsync("luca@rockstarsit.nl").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "luca",
                    Email = "luca@rockstarsit.nl",
                    FirstName = "Luca",
                    LastName = "Bullens",
                    DateOfBirth = DateTime.Parse("1970-01-01"),
                };

                IdentityResult result = userManager.CreateAsync(user, "luca090").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Rockstar").Wait();
                }
            }
            if (userManager.FindByEmailAsync("tijmen@rockstarsit.nl").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "tijmen",
                    Email = "tijmen@rockstarsit.nl",
                    FirstName = "Tijmen",
                    LastName = "Coenders",
                    DateOfBirth = DateTime.Parse("1970-01-01"),
                };

                IdentityResult result = userManager.CreateAsync(user, "tijmen569").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Rockstar").Wait();
                }
            }
            if (userManager.FindByEmailAsync("peter@rockstarsit.nl").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "peter",
                    Email = "peter@rockstarsit.nl",
                    FirstName = "Peter",
                    LastName = "Pannenkoek",
                    DateOfBirth = DateTime.Parse("1970-01-01"),
                };

                IdentityResult result = userManager.CreateAsync(user, "peter785").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Rockstar").Wait();
                }
            }
            if (userManager.FindByEmailAsync("john@rockstarsit.nl").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "john",
                    Email = "john@rockstarsit.nl",
                    FirstName = "John",
                    LastName = "Johnson",
                    DateOfBirth = DateTime.Parse("1970-01-01"),
                };

                IdentityResult result = userManager.CreateAsync(user, "john567").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Rockstar").Wait();
                }
            }
            if (userManager.FindByEmailAsync("gerard@rockstarsit.nl").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "gerard",
                    Email = "gerard@rockstarsit.nl",
                    FirstName = "Gerard",
                    LastName = "de Vries",
                    DateOfBirth = DateTime.Parse("1970-01-01"),
                };

                IdentityResult result = userManager.CreateAsync(user, "gerard572").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Rockstar").Wait();
                }
            }
            if (userManager.FindByEmailAsync("petra@rockstarsit.nl").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "petra",
                    Email = "petra@rockstarsit.nl",
                    FirstName = "Petra",
                    LastName = "de Vries",
                    DateOfBirth = DateTime.Parse("1970-01-01"),
                };

                IdentityResult result = userManager.CreateAsync(user, "petra56").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Rockstar").Wait();
                }
            }

        }

        public static void Initialize(EventPlannerContext context)
        {
            context.Database.EnsureCreated();

            if (context.Events.Any())
            {
                return;
            }

            var events = new Event[]
            {
                new Event{EventName="Bordspellenavond!",
                    Date=DateTime.Parse("2020-07-03 19:00"),VisitorLimit=50,TotalVisitors = 50,
                    Description="Ja hoor!! We zetten onverminderd door met de mega-succesvolle bordspellenavond… " +
                        "Neem je spellen mee, en je partner, vriend of zus. Eet je mee? Laat dit dan even weten aan onze ultieme spelleider Sven Klaassen Bos!",
                    Location="Eindhoven,Rachelsmolen1",
                    Email="shout@teamrockstars.nl",ImageSrc="Bordspel.jpg", CategoryId = 1, ForEmployees=EventGroup.RockstarsEmployees},

                new Event{EventName="Team Rockstars IT @ Future Tech",
                    Date=DateTime.Parse("2020-09-18 17:00"),VisitorLimit=50,TotalVisitors = 50,
                    Description="Ja hoor IT Rockstar Kevin klimt het podium op met zijn sessie “Van Oud naar Cloud”. In chapter Upper South wordt deze sessie gegeven, en wel op 8 oktober bij Team Rockstars IT HQ in ‘s-Hertogenbosch. " +
                    "Programma 17.00 – 18.00 Welkom & Drinks 18.00 – 18.30 Diner 18.30 – 20.15 Van Oud naar Cloud 20.15 – 21.00 Borrel The Talk Van oud naar cloud. Bedrijfskritische (maatwerk)software toekomstbestendig maken is voor veel bedrijven een grote en risicovolle stap. " +
                    "Om koploper te blijven ontkom je er niet aan om deze stap te nemen. In deze sessie neemt IT Rockstar Kevin je mee in het proces, techniek en de voordelen van het ombouwen van een 20 jaar oude stand alone-data intermediair naar een moderne architectuur en RESTful API in Azure.",
                    Location="Jaarbeursplein,Utrecht",
                    Email="shout@teamrockstars.nl",ImageSrc="Teamitup-FutureTech.jpg", CategoryId = 2, ForEmployees=EventGroup.Public},

                new Event{EventName="Team Rockstars Digital Squad | IT Rockstar Sven Haster – The basics of certificates, keys and TLS",
                    Date=DateTime.Parse("2020-08-13 19:00"),VisitorLimit=50,TotalVisitors = 50,
                    Description="“Have you always wanted to know what a certificate is?" +
                    "How it’s used?" +
                    "What certificate you should send to the other party with client certificate authentication?" +
                    "What the difference is between DER and PEM?" +
                    "How to quickly validate a certificate?" +
                    "What a CRL is?" +
                    "On August 13 – 19.00 hrs – Sven will try to answer these questions and more." +
                    "Subscribe here" +
                    "Direct link: https://zoom.us/webinar/register/WN_vZWIRRYCREqG_0z0TGoesw",
                    Location ="Team Rockstars IT Digital Squad",
                    Email="shout@teamrockstars.nl",ImageSrc="Sven.jpg", CategoryId = 2, ForEmployees=EventGroup.Public},

                new Event{EventName="Guilds & Leisures | Hardlopen @ Vestingloop!",
                    Date=DateTime.Parse("2020-07-07 11:00"),VisitorLimit=50,TotalVisitors = 50,
                    Description="Vanuit de vliegensvlugge Hardloop Guild hebben we weer een mooie run op het program staan! " +
                    "Op 7 juni wordt in ’s Hertogenbosch de vestingloop gelopen. Bij deze run zijn de afstanden 5-10-15 km beschikbaar dus minder of meer ervaren mensen kunnen meelopen. " +
                    "Inschrijven voor de Vestingloop kan op eigen gelegenheid via http://www.vestingloop.nl. Kosten? Inschrijving periode 1: t/m zaterdag 29 februari 2020 " +
                    "Inschrijving periode 2: t/m zaterdag 6 juni 2020 5 km inschrijving periode 1 € 15,45 10 km inschrijving periode 1 € 7,45 15 km inschrijving periode " +
                    "1 € 19,45 5 km inschrijving periode 2 € 17,45 10 km inschrijving periode 2 € 19,45 15 km inschrijving periode 2 € 21,45 Team Rockstars zorgt voor een – " +
                    "stevig – ontbijt en de gear! Doe je mee? Meld je dan hieronder aan zodat we samen kunnen starten!",
                    Location="Lekkerbeetjesstraat8,'s-Hertogenbosch",
                    Email="shout@teamrockstars.nl",ImageSrc="Hardlopen.jpg", CategoryId = 1, ForEmployees=EventGroup.RockstarsEmployees},

                new Event{EventName="Pizzasessie IT Rockstar Kevin Hagenaars | Van Oud naar Cloud",
                    Date=DateTime.Parse("2020-10-08 17:00"),VisitorLimit=50,TotalVisitors = 50,
                    Description="Ja hoor IT Rockstar Kevin klimt het podium op met zijn sessie “Van Oud naar Cloud”. In chapter Upper South wordt deze sessie gegeven, en wel op 8 oktober bij Team Rockstars IT HQ in ‘s-Hertogenbosch. " +
                    "Programma 17.00 – 18.00 Welkom & Drinks 18.00 – 18.30 Diner 18.30 – 20.15 Van Oud naar Cloud 20.15 – 21.00 Borrel The Talk Van oud naar cloud. Bedrijfskritische (maatwerk)software toekomstbestendig maken is voor veel bedrijven een grote en risicovolle stap. " +
                    "Om koploper te blijven ontkom je er niet aan om deze stap te nemen. In deze sessie neemt IT Rockstar Kevin je mee in het proces, techniek en de voordelen van het ombouwen van een 20 jaar oude stand alone-data intermediair naar een moderne architectuur en RESTful API in Azure.",
                    Location="Lekkerbeetjesstraat8,'s-Hertogenbosch",
                    Email="shout@teamrockstars.nl",ImageSrc="KevinHagenaars.jpg", CategoryId = 1, ForEmployees=EventGroup.Public},
                
                new Event{EventName="Pizzasessie IT Rockstar Kevin Hagenaars | Van Oud naar Clouds",
                    Date=DateTime.Parse("2021-12-08 17:00"),VisitorLimit=50,TotalVisitors = 50,
                    Description="En ook in Chapter Central gaan we weer door het digitale dak heen! Op 11 juni vanaf 17.00 staan je Chapterleads en Chapter Chief weer voor je klaar. De meeting vindt plaats via Teams. Je Chapterlead zal een afspraak voor deelname in jullie agenda’s zetten.",
                    Location="Lekkerbeetjesstraat8,'s-Hertogenbosch",
                    Email="shout@teamrockstars.nl",ImageSrc="Chapter2RockstarsIT.jpg", CategoryId = 2, ForEmployees=EventGroup.Public},
                
                 new Event{EventName="Chaptermeeting Chapter Upper South",
                    Date=DateTime.Parse("2020-12-24 17:00"),VisitorLimit=50,TotalVisitors = 50,
                    Description="Chapter Upper South laat je horennnnn! Op 11 juni vanaf 17.00 staan je Chapterleads en Chapter Chief namelijk weer voor je klaar. De meeting vindt plaats via Teams. Je Chapterlead zal een afspraak voor deelname in jullie agenda’s zetten.",
                    Location="Jaarbeursplein,Utrecht",
                    Email="shout@teamrockstars.nl",ImageSrc="Chapter2RockstarsIT.jpg", CategoryId = 1, ForEmployees=EventGroup.Public},
                    
                 new Event{EventName="Team Rockstars IT @ Future Tech",
                    Date=DateTime.Parse("2020-06-03 17:00"),VisitorLimit=50,TotalVisitors = 50,
                    Description="Ja hoor IT Rockstar Kevin klimt het podium op met zijn sessie “Van Oud naar Cloud”. In chapter Upper South wordt deze sessie gegeven, en wel op 8 oktober bij Team Rockstars IT HQ in ‘s-Hertogenbosch. " +
                    "Programma 17.00 – 18.00 Welkom & Drinks 18.00 – 18.30 Diner 18.30 – 20.15 Van Oud naar Cloud 20.15 – 21.00 Borrel The Talk Van oud naar cloud. Bedrijfskritische (maatwerk)software toekomstbestendig maken is voor veel bedrijven een grote en risicovolle stap. " +
                    "Om koploper te blijven ontkom je er niet aan om deze stap te nemen. In deze sessie neemt IT Rockstar Kevin je mee in het proces, techniek en de voordelen van het ombouwen van een 20 jaar oude stand alone-data intermediair naar een moderne architectuur en RESTful API in Azure.",
                    Location="Jaarbeursplein,Utrecht",
                    Email="shout@teamrockstars.nl",ImageSrc="Teamitup-FutureTech.jpg", CategoryId = 1, ForEmployees=EventGroup.Public},

                new Event{EventName="Pizzasessie IT Rockstar Sven Haster | Four easy steps to migrate to JPMS",
                    Date=DateTime.Parse("2020-07-15 17:00"),VisitorLimit=40,TotalVisitors = 40,
                    Description="Vanuit Chapter East introduceren wij IT Rockstar Sven Haster!! Op 15 April aanstaande zal hij zijn sessie “Four easy steps " +
                        "to migrate to JPMS” geven in Jules Verne in Apeldoorn. Hier wil je bij zijn! Java 9 introduceerde het Java module system, ook wel project " +
                        "Jigsaw genoemd. Voor veel ontwikkelaars is dit nog een abstract en ondoorzichtig geheel en dit is dan ook dé reden dat veel projecten nog op Java 8 zitten. " +
                        "In deze presentatie laat IT Rockstar Sven vier simpele stappen zien om je project JPMS compliant te maken en naar Java 9+ te migreren.",
                    Location="Apeldoorn,Laanvandemensenrechten362",
                    Email="shout@teamrockstars.nl",ImageSrc="SvenHaster.jpg", CategoryId = 1, ForEmployees=EventGroup.RockstarsEmployees},

                new Event{EventName="IT Rockstar Christiaan Nieuwlaat | Modulaire Software Ontwikkeling – Chapter Lower South",
                    Date=DateTime.Parse("2020-04-16 19:00"),VisitorLimit=50,TotalVisitors = 50,
                    Description="And he does it again!! Op 16 April zal IT Rockstar een sessie verzorgen over Modulaire Software Ontwikkeling. " +
                        "Dit doet hij in het vertrouwde thuishonk van chapter Lower South; Doloris in Tilburg. Steeds meer systemen worden op een manier opgebouwd zodat deze " +
                        "bestaan uit “herbruikbare” onderdelen. Dit gebeurt op macro niveau, het ombouwen van een monoliet naar microservices, maar ook op micro niveau, het " +
                        "gebruik maken van modules als bibliotheken, bijvoorbeeld in de vorm van java 9+ modules. We gaan het hebben over deze verschillende niveaus, waarom we " +
                        "gebruik willen maken van modulaire ontwikkeling en wat de voor- en nadelen hiervan zijn.",
                    Location="Tilburg,Spoorlaan26",
                    Email="shout@teamrockstars.nl",ImageSrc="ChristiaanNieuwlaat.jpg", CategoryId = 1, ForEmployees=EventGroup.Public},

                new Event{EventName="Pizzasessie IT Rockstar Bart Kardol | Digitalisering verandert de wereld, ook op zee",
                    Date=DateTime.Parse("2020-09-22 17:00"),VisitorLimit=50,TotalVisitors = 50,
                    Description="n chapter Upper South hebben we IT Rockstar gepland staan op 22 April voor een supervette sessie: “Digitalisering verandert de wereld, ook op zee”. " +
                    "Catchy titel, nietwaar? Inschrijven voor zijn sessie doe je hier! Het event vindt plaats op Team Rockstars IT HQ in ‘s-Hertogenbosch!",
                    Location="DenBosch,Lekkerbeetjesstraat5",
                    Email="shout@teamrockstars.nl",ImageSrc="BartKardolBreed.jpg", CategoryId = 1, ForEmployees=EventGroup.Public},

                new Event{EventName="Bordspellenavond!",
                    Date=DateTime.Parse("2020-05-03 19:00"),VisitorLimit=50,TotalVisitors = 50,
                    Description="Ja hoor!! We zetten onverminderd door met de mega-succesvolle bordspellenavond… " +
                        "Neem je spellen mee, en je partner, vriend of zus. Eet je mee? Laat dit dan even weten aan onze ultieme spelleider Sven Klaassen Bos!",
                    Location="Eindhoven,Rachelsmolen1",
                    Email="shout@teamrockstars.nl",ImageSrc="Bordspel.jpg", CategoryId = 1, ForEmployees=EventGroup.RockstarsEmployees},
            };
            foreach (Event e in events)
            {
                context.Events.Add(e);
            }
            context.SaveChanges();

            var coaches = new Coach[]
            {
                new Coach{Name="Anne", Email="anne@teamrockstars.nl",
                    Info="Als happy thinker kijkt Anne naar waar je al (bewust of onbewust) goed in bent en biedt ze optimisme en begrip. " +
                    "Luisteren & lachen doet ze graag. Als jongste thuis stond ze haar mannetje en heeft ze avontuurlijke en gastvrije ‘genen’ meegekregen. " +
                    "Communicatie studieachtergrond en 10 jaar ervaring in Recruitment en Coaching.",
                    ImageSrc="CoachingAnne.jpg"},

                new Coach{Name="Eline", Email="eline@teamrockstars.nl",
                    Info="Eline weet je te motiveren en uit te dagen zonder te duwen en doet dit door haar superpowers in te zetten; ze luistert, observeert en zegt wat ze ziet, " +
                    "hoort en voelt. Als benjamin in een gezin met vijf kinderen heeft ze deze skills sterk kunnen ontwikkelen. 10 jaar ervaring in Recruitment, HR, " +
                    "Learning & Development en Coaching.",
                    ImageSrc="CoachingEline.jpg"},
            };
            foreach (Coach coach in coaches)
            {
                context.Coaches.Add(coach);
            }
            context.SaveChanges();
            var categories = new Categorie[]
         {
                new Categorie
                {
                    CategorieName="Educatief", Info="Bij deze evenementen kan je wat meer leren over technische dingen."
                },

                new Categorie
                {
                    CategorieName="Recreatief", Info="Bij deze evenementen hebben we plezier."
                }
         };
            foreach (Categorie categorie in categories)
            {
                context.Categories.Add(categorie);
            }
            context.SaveChanges();

            var ratings = new Rating[]
            {
                new Rating
                {
                    EventId=1, RatingTitle="Is goed", StarRating=4, Comment="Het event werkt voorbeeldig"
                },

                new Rating
                {
                    EventId=1, RatingTitle="Super goed", StarRating=5, Comment="Het evenement werkt perfect, niks kan beter"
                },

                new Rating
                {
                    EventId=2, RatingTitle="Niet mooi", StarRating=1, Comment="Het werkt niet goed"
                },

                new Rating
                {
                    EventId=3, RatingTitle="Gaaf", StarRating=5, Comment="Het is zeer gaaf"
                },

                new Rating
                {
                    EventId=4, RatingTitle="slecht", StarRating=1, Comment="Werkt niet naar wens"
                },

                new Rating
                {
                    EventId=4, RatingTitle="Wel oke", StarRating=3, Comment="werkt prima hoe ik wil"
                }
            };
            foreach (Rating rating in ratings)
            {
                context.Ratings.Add(rating);
            }
            context.SaveChanges();

            var MailSubscribers = new MailSubscriber[]
            {
                new MailSubscriber
                {
                    Email="rockstars.it.project@gmail.com"
                }
            };

            foreach(MailSubscriber mailSubscriber in MailSubscribers)
            {
                context.MailSubscribers.Add(mailSubscriber);
            }
            context.SaveChanges();
        }
    }
}
