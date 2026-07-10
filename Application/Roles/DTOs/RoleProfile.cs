using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.Roles;

namespace Application.Roles.DTOs
{
  public class RoleProfile : Profile
  {
    public RoleProfile()
    {
      CreateMap<AppRole, RoleResponseDTO>()
      .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name));
    }
  }
}