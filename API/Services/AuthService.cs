
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using API.Model.DTO;
using Infrastructure.Persistence;
using Domain.Entities.User;

namespace API.Services
{
  public class AuthService : IAuthService
  {
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    private readonly IConfiguration _configuration;

    public AuthService(ApplicationDbContext db, IMapper mapper, IConfiguration configuration)
    {
      _db = db;
      _mapper = mapper;
      _configuration = configuration;
    }
    public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
    {
      var user = await _db.users.FirstOrDefaultAsync(u => u.Email == loginRequestDTO.Email && EF.Functions.Like(u.Password, loginRequestDTO.Password));
      if (user == null)
      {
        return null;
      }

      // generate a token
      return new LoginResponseDTO
      {
        Token = GenerateToken(user), // In a real application, you would generate a JWT or similar token here
        UserDTO = _mapper.Map<UserDTO>(user)
      };
    }

    public async Task<UserDTO> Registration(RegistrationRequestDTO registrationRequestDTO)
    {
      User user = new User()
      {
        Name = registrationRequestDTO.Name,
        Email = registrationRequestDTO.Email,
        Password = registrationRequestDTO.Password,
        Role = string.IsNullOrEmpty(registrationRequestDTO.Role) ? "Customer" : registrationRequestDTO.Role,
        CreateDate = DateTime.Now,
      };

      await _db.users.AddAsync(user);
      await _db.SaveChangesAsync();

      UserDTO userDTO = _mapper.Map<UserDTO>(user);
      return userDTO;
    }

    private string GenerateToken(User user)
    {
      var key = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value);

      SecurityTokenDescriptor? tokenDescriptor = new SecurityTokenDescriptor
      {
        // 1. 算法 + 秘钥； payload； expire data
        Subject = new ClaimsIdentity(new[]
        {
          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
          new Claim(ClaimTypes.Name, user.Name),
          new Claim(ClaimTypes.Email, user.Email),
          new Claim(ClaimTypes.Role, user.Role)
        }),
        Expires = DateTime.UtcNow.AddDays(365),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      // create a token making machine
      var tokenHandler = new JwtSecurityTokenHandler();

      // give the match the information and it print a card contains a token object in c#, not a string that the front end can use.
      var token = tokenHandler.CreateToken(tokenDescriptor);

      // serialize the token object to a string that the front end can use.
      return tokenHandler.WriteToken(token);
    }
  }
}