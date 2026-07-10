
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities.Villa
{
  [Table("villas")]
  public class Villa
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public string Details { get; set; } = String.Empty;

    public double Rate { get; set; }

    public int Sqft { get; set; }

    public int Occupancy { get; set; }

    public string ImageUrl { get; set; } = String.Empty;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
  }
}