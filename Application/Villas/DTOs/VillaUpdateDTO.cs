
using System.ComponentModel.DataAnnotations;


namespace Application.Villas.DTOs
{
  public class VillaUpdateDTO
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    public string? Description { get; set; }

    [Required]
    public int VillaId { get; set; }
  }
}