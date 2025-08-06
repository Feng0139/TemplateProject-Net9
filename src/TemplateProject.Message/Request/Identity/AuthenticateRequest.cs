using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Identity;

namespace TemplateProject.Message.Request.Identity;

public class AuthenticateRequest : IRequest
{
    [Required]
    [MinLength(1)]
    public required string Name { get; set; }
    
    [Required]
    [MinLength(4)]
    [MaxLength(4)]
    public string Suffix { get; set; } = "0000";
    
    [Required]
    [MinLength(6)]
    public required string Password { get; set; }
}

public class AuthenticateResponse : TemplateProjectResponse<JwtToken>
{
}