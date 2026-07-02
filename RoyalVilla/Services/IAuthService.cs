using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoyalVilla.Model.DTO;

namespace RoyalVilla.Services
{
  public interface IAuthService
  {
    public Task<UserDTO> Registration(RegistrationRequestDTO registrationRequestDTO);

    public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
  }
}