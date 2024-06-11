using System.ComponentModel.DataAnnotations;

namespace Model;

public class City
{
	[Key]
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;
}
