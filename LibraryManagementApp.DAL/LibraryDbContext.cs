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
            new City { Id = 1, Name = "New York" },
            new City { Id = 2, Name = "Los Angeles" },
            new City { Id = 3, Name = "Chicago" },
            new City { Id = 4, Name = "Houston" },
            new City { Id = 5, Name = "Phoenix" },
            new City { Id = 6, Name = "Philadelphia" },
            new City { Id = 7, Name = "San Antonio" },
            new City { Id = 8, Name = "San Diego" },
            new City { Id = 9, Name = "Dallas" },
            new City { Id = 10, Name = "San Jose" },
            new City { Id = 11, Name = "Austin" },
            new City { Id = 12, Name = "Jacksonville" }
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