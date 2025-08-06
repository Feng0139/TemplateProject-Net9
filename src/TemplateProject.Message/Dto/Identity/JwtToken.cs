namespace TemplateProject.Message.Dto.Identity;

public class JwtToken
{
    public string AccessToken { get; set; } = string.Empty;
    
    public string RefreshToken { get; set; } = string.Empty;
}