

namespace Application.Users.DTOs
{
  public class LoginResponseDTO
  {
    public string? Token { get; set; }

    public UserDTO? UserDTO { get; set; }
  }
}