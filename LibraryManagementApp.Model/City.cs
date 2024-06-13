using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApp.Model;
public class City
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "City name is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "City name must be between 2 and 50 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Country name is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Country name must be between 2 and 50 characters.")]
    public string Country { get; set; } = string.Empty;
}
