using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Account;

namespace TemplateProject.Message.Commands.Account;

public class RegisterUserAccountCommand : ICommand
{
    [Required]
    public required string Name { get; set; }
    
    [Required]
    public required string Password { get; set; }
}

public class RegisterUserAccountResponse : TemplateProjectResponse<UserAccountInfoDto>
{
}