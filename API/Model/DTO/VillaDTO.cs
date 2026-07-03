
namespace API.Model.DTO
{
  public class VillaDTO
  {
    public int Id { get; set; }
    public required string Name { get; set; }

    public string Details { get; set; } = String.Empty;

    public double Rate { get; set; }

    public int Sqft { get; set; }

    public int Occupancy { get; set; }

    public string ImageUrl { get; set; } = String.Empty;

  }
}