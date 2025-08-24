using Microsoft.EntityFrameworkCore;
using PagilaGraphQL.Data;

namespace PagilaGraphQL.GraphQL
{
    public class Query
    {
        // Actors
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Actor> GetActors([Service] PagilaContext context) =>
            context.Actors;

        // Films
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Film> GetFilms([Service] PagilaContext context) =>
            context.Films;

        // Customers
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Customer> GetCustomers([Service] PagilaContext context) =>
            context.Customers;

        // Rentals
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Rental> GetRentals([Service] PagilaContext context) =>
            context.Rentals;

        // Payments
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Payment> GetPayments([Service] PagilaContext context) =>
            context.Payments;

        // Categories
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Category> GetCategories([Service] PagilaContext context) =>
            context.Categories;

        // FilmActors (optional, can traverse via Film.Actors or Actor.Films)
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<FilmActor> GetFilmActors([Service] PagilaContext context) =>
            context.FilmActors;

        // FilmCategories (optional, can traverse via Film.Categories or Category.Films)
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<FilmCategory> GetFilmCategories([Service] PagilaContext context) =>
            context.FilmCategories;

        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Actor> GetActorsWithFilms([Service] PagilaContext context) =>
            context.Actors
           .Include(a => a.FilmActors)        // load the join table
               .ThenInclude(fa => fa.Film)   // load the related films
           .Where(a => a.FilmActors.Any(fa => fa.Film != null));
    }
}