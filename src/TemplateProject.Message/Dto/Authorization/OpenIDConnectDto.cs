namespace TemplateProject.Message.Dto.Authorization;

public class OpenIDConnectDto
{
    public string Ns { get; set; } = string.Empty;
    
    public string Mode { get; set; } = string.Empty;
    
    public string Op_endpoint { get; set; } = string.Empty;

    public string Claimed_Id { get; set; } = string.Empty;
    
    public string Identity { get; set; } = string.Empty;
    
    public string Return_to { get; set; } = string.Empty;
    
    public string Response_nonce { get; set; } = string.Empty;
    
    public string Assoc_handle { get; set; } = string.Empty;
    
    public string Signed { get; set; } = string.Empty;
    
    public string Sig { get; set; } = string.Empty;
    
    public string Error { get; set; } = string.Empty;
}