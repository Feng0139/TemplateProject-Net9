using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Security;

public class AddRoleConflictMatrixCommand : ICommand
{
    public Guid RoleAId { get; set; }
    
    public Guid RoleBId { get; set; }
}

public class AddRoleConflictMatrixResponse : TemplateProjectResponse
{
}