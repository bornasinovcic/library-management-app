using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApp.Model;

public class Genre
{
	[Key]
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
}
