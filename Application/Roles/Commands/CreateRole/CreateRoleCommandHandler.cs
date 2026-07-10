using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Roles.DTOs;
using AutoMapper;
using Domain.Entities.Roles;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Application.Roles.Commands.CreateRole
{
  public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleResponseDTO>
  {
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public CreateRoleCommandHandler(RoleManager<AppRole> roleManager, IMapper mapper)
    {
      _roleManager = roleManager;
      _mapper = mapper;
    }

    public async Task<RoleResponseDTO> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
      var isExist = await _roleManager.RoleExistsAsync(request.RoleName);

      if (isExist) throw new Exception("Exist");

      IdentityResult? result = await _roleManager.CreateAsync(new AppRole { Name = request.RoleName }) ?? throw new Exception("Create Fail");

      var role = await _roleManager.FindByNameAsync(request.RoleName);
      var responseDTO = _mapper.Map<RoleResponseDTO>(role);
      return responseDTO;

    }
  }
}