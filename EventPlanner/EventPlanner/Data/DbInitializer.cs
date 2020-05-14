﻿using EventPlanner.Models;
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

            if (context.Events.Any())
            {
                return;
            }

            var events = new Event[]
            {
                new Event{EventName="Guilds & Leisures | Hardlopen @ Vestingloop!",
                    Date=DateTime.Parse("2020-06-07"),VisitorLimit=50,
                    Description="Vanuit de vliegensvlugge Hardloop Guild hebben we weer een mooie run op het program staan! " +
                    "Op 7 juni wordt in ’s Hertogenbosch de vestingloop gelopen. Bij deze run zijn de afstanden 5-10-15 km beschikbaar dus minder of meer ervaren mensen kunnen meelopen. " +
                    "Inschrijven voor de Vestingloop kan op eigen gelegenheid via http://www.vestingloop.nl. Kosten? Inschrijving periode 1: t/m zaterdag 29 februari 2020 " +
                    "Inschrijving periode 2: t/m zaterdag 6 juni 2020 5 km inschrijving periode 1 € 15,45 10 km inschrijving periode 1 € 7,45 15 km inschrijving periode " +
                    "1 € 19,45 5 km inschrijving periode 2 € 17,45 10 km inschrijving periode 2 € 19,45 15 km inschrijving periode 2 € 21,45 Team Rockstars zorgt voor een – " +
                    "stevig – ontbijt en de gear! Doe je mee? Meld je dan hieronder aan zodat we samen kunnen starten!",
                    Location="Lekkerbeetjesstraat8,'s-Hertogenbosch",
                    Email="shout@teamrockstars.nl",ImageSrc="Hardlopen.jpg", CategoryId = 1, ForEmployees=EventGroup.Public},

                new Event{EventName="Pizzasessie IT Rockstar Kevin Hagenaars | Van Oud naar Cloud",
                    Date=DateTime.Parse("2020-10-08"),VisitorLimit=50,
                    Description="Ja hoor IT Rockstar Kevin klimt het podium op met zijn sessie “Van Oud naar Cloud”. In chapter Upper South wordt deze sessie gegeven, en wel op 8 oktober bij Team Rockstars IT HQ in ‘s-Hertogenbosch. " +
                    "Programma 17.00 – 18.00 Welkom & Drinks 18.00 – 18.30 Diner 18.30 – 20.15 Van Oud naar Cloud 20.15 – 21.00 Borrel The Talk Van oud naar cloud. Bedrijfskritische (maatwerk)software toekomstbestendig maken is voor veel bedrijven een grote en risicovolle stap. " +
                    "Om koploper te blijven ontkom je er niet aan om deze stap te nemen. In deze sessie neemt IT Rockstar Kevin je mee in het proces, techniek en de voordelen van het ombouwen van een 20 jaar oude stand alone-data intermediair naar een moderne architectuur en RESTful API in Azure.",
                    Location="Lekkerbeetjesstraat8,'s-Hertogenbosch",
                    Email="shout@teamrockstars.nl",ImageSrc="KevinHagenaars.jpg", CategoryId = 1, ForEmployees=EventGroup.Public},

                 new Event{EventName="Team Rockstars IT @ Future Tech",
                    Date=DateTime.Parse("2020-06-24"),VisitorLimit=50,
                    Description="Ja hoor IT Rockstar Kevin klimt het podium op met zijn sessie “Van Oud naar Cloud”. In chapter Upper South wordt deze sessie gegeven, en wel op 8 oktober bij Team Rockstars IT HQ in ‘s-Hertogenbosch. " +
                    "Programma 17.00 – 18.00 Welkom & Drinks 18.00 – 18.30 Diner 18.30 – 20.15 Van Oud naar Cloud 20.15 – 21.00 Borrel The Talk Van oud naar cloud. Bedrijfskritische (maatwerk)software toekomstbestendig maken is voor veel bedrijven een grote en risicovolle stap. " +
                    "Om koploper te blijven ontkom je er niet aan om deze stap te nemen. In deze sessie neemt IT Rockstar Kevin je mee in het proces, techniek en de voordelen van het ombouwen van een 20 jaar oude stand alone-data intermediair naar een moderne architectuur en RESTful API in Azure.",
                    Location="Jaarbeursplein,Utrecht",
                    Email="shout@teamrockstars.nl",ImageSrc="Teamitup-FutureTech.jpg", CategoryId = 1, ForEmployees=EventGroup.Public},

                new Event{EventName="Pizzasessie IT Rockstar Sven Haster | Four easy steps to migrate to JPMS",
                    Date=DateTime.Parse("2020-04-15"),VisitorLimit=40,
                    Description="Vanuit Chapter East introduceren wij IT Rockstar Sven Haster!! Op 15 April aanstaande zal hij zijn sessie “Four easy steps " +
                        "to migrate to JPMS” geven in Jules Verne in Apeldoorn. Hier wil je bij zijn! Java 9 introduceerde het Java module system, ook wel project " +
                        "Jigsaw genoemd. Voor veel ontwikkelaars is dit nog een abstract en ondoorzichtig geheel en dit is dan ook dé reden dat veel projecten nog op Java 8 zitten. " +
                        "In deze presentatie laat IT Rockstar Sven vier simpele stappen zien om je project JPMS compliant te maken en naar Java 9+ te migreren.",
                    Location="Apeldoorn,Laanvandemensenrechten362",
                    Email="shout@teamrockstars.nl",ImageSrc="SvenHaster.jpg", CategoryId = 1, ForEmployees=EventGroup.Public},

                new Event{EventName="IT Rockstar Christiaan Nieuwlaat | Modulaire Software Ontwikkeling – Chapter Lower South",
                    Date=DateTime.Parse("2020-04-16"),VisitorLimit=50,
                    Description="And he does it again!! Op 16 April zal IT Rockstar een sessie verzorgen over Modulaire Software Ontwikkeling. " +
                        "Dit doet hij in het vertrouwde thuishonk van chapter Lower South; Doloris in Tilburg. Steeds meer systemen worden op een manier opgebouwd zodat deze " +
                        "bestaan uit “herbruikbare” onderdelen. Dit gebeurt op macro niveau, het ombouwen van een monoliet naar microservices, maar ook op micro niveau, het " +
                        "gebruik maken van modules als bibliotheken, bijvoorbeeld in de vorm van java 9+ modules. We gaan het hebben over deze verschillende niveaus, waarom we " +
                        "gebruik willen maken van modulaire ontwikkeling en wat de voor- en nadelen hiervan zijn.",
                    Location="Tilburg,Spoorlaan26",
                    Email="shout@teamrockstars.nl",ImageSrc="ChristiaanNieuwlaat.jpg", CategoryId = 1, ForEmployees=EventGroup.Public},

                new Event{EventName="Pizzasessie IT Rockstar Bart Kardol | Digitalisering verandert de wereld, ook op zee",
                    Date=DateTime.Parse("2020-04-22"),VisitorLimit=50,
                    Description="n chapter Upper South hebben we IT Rockstar gepland staan op 22 April voor een supervette sessie: “Digitalisering verandert de wereld, ook op zee”. " +
                    "Catchy titel, nietwaar? Inschrijven voor zijn sessie doe je hier! Het event vindt plaats op Team Rockstars IT HQ in ‘s-Hertogenbosch!",
                    Location="DenBosch,Lekkerbeetjesstraat5",
                    Email="shout@teamrockstars.nl",ImageSrc="BartKardolBreed.jpg", CategoryId = 1, ForEmployees=EventGroup.Public},

                new Event{EventName="Bordspellenavond!",
                    Date=DateTime.Parse("2020-05-03"),VisitorLimit=50,
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

            var accounts = new Account[]
            {
                new Account{UserName="admin", Role="Administrator",
                    Password="admin", FirstName="John", LastName="Doe", ZipCode="1234AB", HouseNumber=12, Email="john.doe@gmail.com", DateOfBirth=DateTime.Parse("1990-01-01")},
                new Account{UserName="Henk123", Role="User",
                    Password="123Henk", FirstName="Henk", LastName="de Blok", ZipCode="4326KJ", HouseNumber=33, Email="henkdeblok@gmail.com", DateOfBirth=DateTime.Parse("1994-11-15")},
                new Account{UserName="Nancy", Role="User",
                    Password="Blokkenbouwer5", FirstName="Nancy", LastName="de Wit", ZipCode="6897LM", HouseNumber=5, Email="nancywit@hotmail.com", DateOfBirth=DateTime.Parse("2000-09-02")},
                new Account{UserName="RoyMoerbeek", Role="User",
                    Password="StrandGanger", FirstName="Roy", LastName="Moerbeek", ZipCode="7892AF", HouseNumber=89, Email="RoyMoerbeek@gmail.com", DateOfBirth=DateTime.Parse("2001-08-29")},
                new Account{UserName="Lieke", Role="User",
                    Password="Wachtwoord", FirstName="Lieke", LastName="Buurman", ZipCode="6845KL", HouseNumber=4, Email="liekebuurman@gmail.com", DateOfBirth=DateTime.Parse("1980-02-15")},
            };
            foreach (Account account in accounts)
            {
                context.Accounts.Add(account);
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

            var registrations = new Registration[]
            {
                new Registration{AccountId=1, EventId=2},
                new Registration{AccountId=5, EventId=3},
                new Registration{AccountId=2, EventId=1},
                new Registration{AccountId=3, EventId=4},
                new Registration{AccountId=3, EventId=1},
                new Registration{AccountId=3, EventId=2},
                new Registration{AccountId=6, EventId=4},
                new Registration{AccountId=5, EventId=1},
                new Registration{AccountId=1, EventId=4},
                new Registration{AccountId=4, EventId=2}
            };
            foreach (Registration registration in registrations)
            {
                context.Registrations.Add(registration);
            }
            context.SaveChanges();

            var categories = new Categorie[]
           {
                new Categorie
                {
                    CategorieName="Educational", Info="Bij deze evenementen kan je wat meer leren over technische dingen."
                },

                new Categorie
                {
                    CategorieName="Recreation", Info="Bij deze evenementen hebben we plezier."
                }
           };
            foreach (Categorie categorie in categories)
            {
                context.Categories.Add(categorie);
            }
            context.SaveChanges();
        }
    }
}
