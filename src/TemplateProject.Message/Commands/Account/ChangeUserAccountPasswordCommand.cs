using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Account;

public class ChangeUserAccountPasswordCommand : ICommand
{
    public Guid? UserId { get; set; }
    
    /// <summary>
    /// 旧密码
    /// </summary>
    public required string OldPassword { get; set; }
    
    /// <summary>
    /// 新密码
    /// </summary>
    public required string NewPassword { get; set; }
    
    /// <summary>
    /// 新密码确认
    /// </summary>
    public required string NewPasswordConfirm { get; set; }

}

public class ChangeUserAccountPasswordResponse : TemplateProjectResponse
{
}