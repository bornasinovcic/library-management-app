using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApp.Model;

public class Book
{
	[Key]
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;

	public DateTime PublishDate { get; set; }
	public Genre? genre { get; set; }
	public ICollection<AuthorBook>? AuthorBooks { get; set; }
}