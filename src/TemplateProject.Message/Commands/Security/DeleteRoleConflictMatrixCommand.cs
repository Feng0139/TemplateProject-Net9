using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Security;

public class DeleteRoleConflictMatrixCommand : ICommand
{
    public Guid Id { get; set; }
}

public class DeleteRoleConflictMatrixResponse : TemplateProjectResponse
{
}