
using Application.Users.DTOs;

namespace API.Services
{
  public interface IAuthService
  {
    public Task<UserDTO> Registration(RegistrationRequestDTO registrationRequestDTO);

    public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
  }
}