using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApp.Model;

public class Author
{
	[Key]
	public int Id { get; set; }
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string FullName => $"{FirstName} {LastName}";
    public char Gender { get; set; }
    public string Email { get; set; } = string.Empty;
	public DateTime DateOfBirth { get; set; }

    public int? PlaceOfBirthId { get; set; }
    public City? PlaceOfBirth { get; set; }
    public ICollection<AuthorBook>? AuthorBooks { get; set; }
}
