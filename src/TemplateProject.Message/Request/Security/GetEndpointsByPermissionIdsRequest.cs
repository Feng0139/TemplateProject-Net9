using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Message.Request.Security;

public class GetEndpointsByPermissionIdsRequest : IRequest
{
    public List<Guid> PermissionIds { get; set; } = [];
}

public class GetEndpointsByPermissionIdsResponse : TemplateProjectResponse<List<EndpointDto>>
{
}