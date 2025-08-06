namespace TemplateProject.Message.Dto.Security;

public class EndpointDto
{
    public string Controller { get; set; } = string.Empty;

    public string Endpoint { get; set; } = string.Empty;

    public string Method { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
}