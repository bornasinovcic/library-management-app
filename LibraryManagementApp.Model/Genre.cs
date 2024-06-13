using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApp.Model;

public class Genre
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Genre name is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Genre name must be between 2 and 50 characters.")]
    public string Name { get; set; } = string.Empty;
}
