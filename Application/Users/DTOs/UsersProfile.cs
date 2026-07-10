
using AutoMapper;
using Domain.Entities.User;


namespace Application.Users.DTOs
{
  public class UsersProfile : Profile
  {
    public UsersProfile()
    {
      CreateMap<LoginRequestDTO, User>().ReverseMap();
      CreateMap<User, UserDTO>().ReverseMap();
    }
  }
}