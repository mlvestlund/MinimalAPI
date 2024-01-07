using Microsoft.EntityFrameworkCore;
using MinimalAPI.Data;
using MinimalAPI.Models;
using System.Net;

namespace MinimalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("MinimalDbContext");
            builder.Services.AddDbContext<MinimalDbContext>(options => options.UseSqlServer(connectionString));
            var app = builder.Build();

            app.MapGet("/", () => "Have fun!");

            // GET /persons fetches all persons
            app.MapGet("/persons", (MinimalDbContext context) =>
            {
                return Results.Json(context.Persons.Select(p => new {p.PersonID, p.FirstName, p.LastName, p.PhoneNumber}).ToArray());
            });

            // GET /Interests{PersonID} fetches all interests for one person
            app.MapGet("/interests/{PersonID}", (MinimalDbContext context, int PersonID) =>
            {
                var person = context.Persons.Find(PersonID);

                if (person == null || !context.PersonInterests.Any(pi => pi.PersonID == PersonID))
                {
                    return Results.NotFound();
                }

                var interests = context.PersonInterests.Where(pi => pi.PersonID == PersonID).Select(pi => new { InterestID = pi.Interest.InterestID, InterestName = pi.Interest.InterestName, InterestDescription = pi.Interest.InterestDescription }).ToArray();
                if (interests == null)
                {
                    return Results.NotFound();
                }
                return Results.Json(interests);
            });

            //GET /Links{PersonID} fetches all links for one person

            app.MapGet("/links/{PersonID}", (MinimalDbContext context, int PersonID) =>
            {
                var links = context.Links.Where(l => l.Person.PersonID == PersonID).Select(l => new { l.LinkID, l.LinkURL, l.PersonID, l.InterestID }).ToArray();
                if(links == null)
                {
                    return Results.NotFound();
                }
                return Results.Json(links);
            });

            //POST /interest creates a new interest for a person
            app.MapPost("/interest", (MinimalDbContext context, PersonInterest PersonInterest) =>
            {
                var person = context.Persons.Find(PersonInterest.PersonID);
                if(person == null)
                {
                    return Results.NotFound();
                }

                var newInterest = new Interest
                {
                    InterestName = PersonInterest.Interest.InterestName,
                    InterestDescription = PersonInterest.Interest.InterestDescription
                };
                context.Interests.Add(newInterest);

                var newPersonInterest = new PersonInterest
                {
                    PersonID = PersonInterest.PersonID,
                    Interest = newInterest
                };
                context.PersonInterests.Add(newPersonInterest);

                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            });

            //POST /personinterest creates a new connection between an excisting person and interest
            app.MapPost("/personinterest", (MinimalDbContext context, PersonInterest personInterest) =>
            {
                var person = context.Persons.Find(personInterest.PersonID);
                var interest = context.Interests.Find(personInterest.InterestID);
                if (person == null || interest == null)
                {
                    return Results.NotFound();
                }

                var newPersonInterest = new PersonInterest
                {
                    PersonID = personInterest.PersonID,
                    InterestID = personInterest.InterestID
                };
                context.PersonInterests.Add(newPersonInterest);

                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            });


            //POST /link creates a new link with connection to person and interest
            app.MapPost("/link", (MinimalDbContext context, Link link) =>
            {
                var person = context.Persons.Find(link.PersonID);
                var interest = context.Interests.Find(link.InterestID);
                if (person == null || interest == null)
                {
                    return Results.NotFound();
                }

                var newLink = new Link
                {
                    LinkURL = link.LinkURL,
                    PersonID = link.PersonID,
                    InterestID = interest.InterestID
                };
                context.Links.Add(newLink);

                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            });

            app.Run();
        }
    }
}
