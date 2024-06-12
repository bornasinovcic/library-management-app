using LibraryManagementApp.Model;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagementApp.DAL;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<AuthorBook> AuthorBooks { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Romance novel" },
            new Genre { Id = 2, Name = "Science fiction" },
            new Genre { Id = 3, Name = "Mystery" },
            new Genre { Id = 4, Name = "Horror" },
            new Genre { Id = 5, Name = "Fantasy" },
            new Genre { Id = 6, Name = "Young adult" },
            new Genre { Id = 7, Name = "Adventure fiction" },
            new Genre { Id = 8, Name = "Biography" },
            new Genre { Id = 9, Name = "Dystopian" },
            new Genre { Id = 10, Name = "Historical Fiction" },
            new Genre { Id = 11, Name = "History" },
            new Genre { Id = 12, Name = "Crime and Thriller" }
        );

        modelBuilder.Entity<City>().HasData(
            new City { Id = 1, Name = "New York", Country = "United States" },
            new City { Id = 2, Name = "Toronto", Country = "Canada" },
            new City { Id = 3, Name = "London", Country = "United Kingdom" },
            new City { Id = 4, Name = "Sydney", Country = "Australia" },
            new City { Id = 5, Name = "Berlin", Country = "Germany" },
            new City { Id = 6, Name = "Paris", Country = "France" },
            new City { Id = 7, Name = "Rome", Country = "Italy" },
            new City { Id = 8, Name = "Madrid", Country = "Spain" },
            new City { Id = 9, Name = "Rio de Janeiro", Country = "Brazil" },
            new City { Id = 10, Name = "Mumbai", Country = "India" },
            new City { Id = 11, Name = "Tokyo", Country = "Japan" },
            new City { Id = 12, Name = "Beijing", Country = "China" }
        );

        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, FirstName = "Jane", LastName = "Austen", Email = "jane@example.com", DateOfBirth = new DateTime(1775, 12, 16), PlaceOfBirthId = 1 },
            new Author { Id = 2, FirstName = "George", LastName = "Orwell", Email = "george@example.com", DateOfBirth = new DateTime(1903, 6, 25), PlaceOfBirthId = 2 },
            new Author { Id = 3, FirstName = "Agatha", LastName = "Christie", Email = "agatha@example.com", DateOfBirth = new DateTime(1890, 9, 15), PlaceOfBirthId = 3 },
            new Author { Id = 4, FirstName = "Stephen", LastName = "King", Email = "stephen@example.com", DateOfBirth = new DateTime(1947, 9, 21), PlaceOfBirthId = 4 },
            new Author { Id = 5, FirstName = "J.K.", LastName = "Rowling", Email = "jk@example.com", DateOfBirth = new DateTime(1965, 7, 31), PlaceOfBirthId = 5 }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Pride and Prejudice", Description = "A romantic novel of manners.", PublishDate = new DateTime(1813, 1, 28), GenreId = 1 },
            new Book { Id = 2, Title = "1984", Description = "A dystopian social science fiction novel.", PublishDate = new DateTime(1949, 6, 8), GenreId = 9 },
            new Book { Id = 3, Title = "Murder on the Orient Express", Description = "A detective novel.", PublishDate = new DateTime(1934, 1, 1), GenreId = 3 },
            new Book { Id = 4, Title = "The Shining", Description = "A horror novel.", PublishDate = new DateTime(1977, 1, 28), GenreId = 4 },
            new Book { Id = 5, Title = "Harry Potter and the Philosopher's Stone", Description = "A fantasy novel.", PublishDate = new DateTime(1997, 6, 26), GenreId = 5 }
        );

        modelBuilder.Entity<AuthorBook>().HasData(
            new AuthorBook { AuthorId = 1, BookId = 1 },
            new AuthorBook { AuthorId = 1, BookId = 2 },
            new AuthorBook { AuthorId = 1, BookId = 3 },
            new AuthorBook { AuthorId = 1, BookId = 4 },
            new AuthorBook { AuthorId = 1, BookId = 5 },
            new AuthorBook { AuthorId = 2, BookId = 1 },
            new AuthorBook { AuthorId = 2, BookId = 2 },
            new AuthorBook { AuthorId = 2, BookId = 3 },
            new AuthorBook { AuthorId = 2, BookId = 4 },
            new AuthorBook { AuthorId = 3, BookId = 1 },
            new AuthorBook { AuthorId = 3, BookId = 2 },
            new AuthorBook { AuthorId = 3, BookId = 3 },
            new AuthorBook { AuthorId = 4, BookId = 1 },
            new AuthorBook { AuthorId = 4, BookId = 2 },
            new AuthorBook { AuthorId = 5, BookId = 1 }
        );

        // Configure composite key for AuthorBook
        modelBuilder.Entity<AuthorBook>()
            .HasKey(ab => new { ab.AuthorId, ab.BookId });

        // Configure many-to-many relationship
        modelBuilder.Entity<AuthorBook>()
            .HasOne(ab => ab.Author)
            .WithMany(a => a.AuthorBooks)
            .HasForeignKey(ab => ab.AuthorId);

        modelBuilder.Entity<AuthorBook>()
            .HasOne(ab => ab.Book)
            .WithMany(b => b.AuthorBooks)
            .HasForeignKey(ab => ab.BookId);

    }
}