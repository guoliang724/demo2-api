
using System.ComponentModel.DataAnnotations;


namespace Application.Villas.DTOs
{
  public class VillaCreateDTO
  {
    [MaxLength(50)]
    // [Required]
    public string Name { get; set; } = string.Empty;
    // public required string Name { get; set; }

    public string Details { get; set; } = String.Empty;

    public double Rate { get; set; }

    // [MinimumSqftValidator(1000, ErrorMessage = "This is a test message {0}")]
    public int Sqft { get; set; }

    public int Occupancy { get; set; }

    public string ImageUrl { get; set; } = String.Empty;
  }
}