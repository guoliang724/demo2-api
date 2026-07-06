using Microsoft.AspNetCore.Mvc;
using API.Filters.ValidateFilters;
using API.Services;
using API.Model;
using Application.Users.DTOs;

namespace API.Controllers
{
  [Route("api/auth")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
      _authService = authService;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [TypeFilter(typeof(User_ValidateRegistrationFilterAttribute))]

    public async Task<ActionResult<ApiResponse<UserDTO>>> Registration(RegistrationRequestDTO registrationRequestDTO)
    {
      UserDTO? userDTO = await _authService.Registration(registrationRequestDTO);
      var response = ApiResponse<UserDTO>.CreateAt(userDTO, "User registered successfully");
      return CreatedAtAction(nameof(Registration), new { Id = userDTO.Id }, response);
    }


    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [TypeFilter(typeof(User_ValidateLoginFilterAttribute))]

    public async Task<ActionResult<ApiResponse<LoginResponseDTO>>> Login(LoginRequestDTO loginRequestDTO)
    {
      LoginResponseDTO? userResponse = await _authService.Login(loginRequestDTO);
      if (userResponse == null)
      {
        var response = ApiResponse<LoginResponseDTO>.BadRequest("Login failed. Invalid email or password.");
        return BadRequest(response);
      }
      else
      {
        ApiResponse<LoginResponseDTO>? response = ApiResponse<LoginResponseDTO>.CreateAt(userResponse, "User logged in successfully");
        return Ok(response);
      }
    }
  }
}