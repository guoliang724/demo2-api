using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyalVilla.Model.DTO
{
  public class LoginResponseDTO
  {
    public string? Token { get; set; }

    public UserDTO? UserDTO { get; set; }
  }
}