using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Message.Request.Security;

public class GetRoleConflictMatrixListRequest : IRequest
{
    [Required]
    [MinLength(1)]
    public List<Guid> RoleIdList { get; set; } = [];
}

public class GetRoleConflictMatrixListResponse : TemplateProjectResponse<List<RoleConflictMatrixDto>>
{
}