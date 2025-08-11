using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Message.Request.Security;

public class GetMenuListRequest : IRequest
{
    [Required]
    [MinLength(1)]
    public List<Guid> IdList { get; set; } = [];
}

public class GetMenuListResponse : TemplateProjectResponse<List<MenuDto>>
{
}