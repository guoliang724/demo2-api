
using System.ComponentModel.DataAnnotations;


namespace Application.Users.DTOs
{
  public class RegistrationRequestDTO
  {
    [Required]
    [EmailAddress]
    public required string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; } = string.Empty;

    [Required]
    public required string Password { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Role { get; set; } = "Customer";



  }
}