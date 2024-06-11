using LibraryManagementApp.Model;
using Microsoft.EntityFrameworkCore;
using Model;

namespace LibraryManagementApp.DAL;

public class LibraryDbContext : DbContext
{
	public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

	public DbSet<City> Cities { get; set; }
	public DbSet<Genre> Genres { get; set; }

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

	}
}