using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Message.Request.Security;

public class GetEndpointListRequest : IRequest
{
}

public class GetEndpointListResponse : TemplateProjectResponse<List<EndpointDto>>
{
}