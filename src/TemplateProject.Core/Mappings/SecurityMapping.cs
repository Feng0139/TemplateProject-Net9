using AutoMapper;
using TemplateProject.Core.Domain.Security;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Core.Mappings;

public class SecurityMapping : Profile
{
    public SecurityMapping()
    {
        CreateMap<UserRole, UserRoleDto>().ReverseMap();
        
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<RoleConflictMatrix, RoleConflictMatrixDto>().ReverseMap();
        CreateMap<RolePermission, RolePermissionDto>().ReverseMap();
        
        CreateMap<Permission, PermissionDto>().ReverseMap();
        CreateMap<PermissionMenu, PermissionMenuDto>().ReverseMap();
        CreateMap<PermissionEndpoint, PermissionEndpointDto>().ReverseMap();
        
        CreateMap<Menu, MenuDto>().ReverseMap();
    }
}