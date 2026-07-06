
using Application.Dishes;

namespace Application.DTOs
{
  public class RestaurantResponseDTO
  {

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public bool HasDelivery { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }

    public List<DishDTO> Dishes { get; set; } = new List<DishDTO>();
  }
}