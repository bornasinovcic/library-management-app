using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApp.Model;
public class Book
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 100 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Publish date is required.")]
    [DataType(DataType.Date)]
    public DateTime PublishDate { get; set; }

    public int? GenreId { get; set; }
    public Genre? BookGenre { get; set; }

    public ICollection<AuthorBook>? AuthorBooks { get; set; }
}