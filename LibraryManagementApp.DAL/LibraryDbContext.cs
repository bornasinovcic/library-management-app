using LibraryManagementApp.Model;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagementApp.DAL;

public class LibraryDbContext : DbContext
{
	public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

	public DbSet<Book> Books { get; set; }
	public DbSet<City> Cities { get; set; }
	public DbSet<Genre> Genres { get; set; }
	public DbSet<Author> Authors { get; set; }

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