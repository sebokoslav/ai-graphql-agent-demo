using Microsoft.EntityFrameworkCore;

namespace PagilaGraphQL.Data
{
    public class PagilaContext : DbContext
    {
        public PagilaContext(DbContextOptions<PagilaContext> options) : base(options) { }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FilmCategory> FilmCategories { get; set; }
        public DbSet<FilmActor> FilmActors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Table mappings
            modelBuilder.Entity<Actor>().ToTable("actor");
            modelBuilder.Entity<Film>().ToTable("film");
            modelBuilder.Entity<Customer>().ToTable("customer");
            modelBuilder.Entity<Rental>().ToTable("rental");
            modelBuilder.Entity<Payment>().ToTable("payment");
            modelBuilder.Entity<Category>().ToTable("category");
            modelBuilder.Entity<FilmCategory>().ToTable("film_category");
            modelBuilder.Entity<FilmActor>().ToTable("film_actor");

            // Composite keys for join tables
            modelBuilder.Entity<FilmActor>().HasKey(fa => new { fa.FilmId, fa.ActorId });
            modelBuilder.Entity<FilmCategory>().HasKey(fc => new { fc.FilmId, fc.CategoryId });

            // Relationships
            modelBuilder.Entity<FilmActor>()
                .HasOne(fa => fa.Film)
                .WithMany(f => f.FilmActors)
                .HasForeignKey(fa => fa.FilmId);

            modelBuilder.Entity<FilmActor>()
                .HasOne(fa => fa.Actor)
                .WithMany(a => a.FilmActors)
                .HasForeignKey(fa => fa.ActorId);

            modelBuilder.Entity<FilmCategory>()
                .HasOne(fc => fc.Film)
                .WithMany(f => f.FilmCategories)
                .HasForeignKey(fc => fc.FilmId);

            modelBuilder.Entity<FilmCategory>()
                .HasOne(fc => fc.Category)
                .WithMany(c => c.FilmCategories)
                .HasForeignKey(fc => fc.CategoryId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Film)
                .WithMany(f => f.Rentals)
                .HasForeignKey(r => r.FilmId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.CustomerId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Rental)
                .WithMany(r => r.Payments)
                .HasForeignKey(p => p.RentalId);
        }
    }

    public class Actor
    {
        public int ActorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<FilmActor> FilmActors { get; set; }
    }

    public class Film
    {
        public int FilmId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<FilmActor> FilmActors { get; set; }
        public ICollection<FilmCategory> FilmCategories { get; set; }
        public ICollection<Rental> Rentals { get; set; }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Rental> Rentals { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }

    public class Rental
    {
        public int RentalId { get; set; }
        public int FilmId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentalDate { get; set; }
        public Film Film { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }

    public class Payment
    {
        public int PaymentId { get; set; }
        public int RentalId { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public Rental Rental { get; set; }
        public Customer Customer { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<FilmCategory> FilmCategories { get; set; }
    }

    public class FilmCategory
    {
        public int FilmId { get; set; }
        public int CategoryId { get; set; }
        public Film Film { get; set; }
        public Category Category { get; set; }
    }

    public class FilmActor
    {
        public int FilmId { get; set; }
        public int ActorId { get; set; }
        public Film Film { get; set; }
        public Actor Actor { get; set; }
    }
}
