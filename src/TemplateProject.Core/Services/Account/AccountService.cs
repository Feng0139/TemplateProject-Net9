using AutoMapper;
using TemplateProject.Core.Extension;
using TemplateProject.Message.Commands.Account;
using TemplateProject.Message.Dto.Account;
using TemplateProject.Message.Enum;
using TemplateProject.Message.Request.Account;

namespace TemplateProject.Core.Services.Account;

public interface IAccountService : IScope
{
    Task<GetUserAccountResponse> GetUserAccountAsync(
        GetUserAccountRequest request, CancellationToken cancellationToken);

    Task<GetUserAccountByThirdPartyResponse> GetUserAccountByThirdPartyAsync(
        GetUserAccountByThirdPartyRequest request, CancellationToken cancellationToken);

    Task<GetThirdPartyByThirdPartyResponse> GetThirdPartyByThirdPartyAsync(
        GetThirdPartyByThirdPartyRequest request, CancellationToken cancellationToken);

    Task<RegisterUserAccountResponse> RegisterUserAccountAsync(
        RegisterUserAccountCommand command, CancellationToken cancellationToken);

    Task<LinkUserAccountFromThirdPartyResponse> LinkAccountFromThirdPartyAsync(
        LinkUserAccountFromThirdPartyCommand command, CancellationToken cancellationToken);

    Task<ChangeUserAccountPasswordResponse> ChangeUserAccountPasswordAsync(
        ChangeUserAccountPasswordCommand command, CancellationToken cancellationToken);
    
    Task<bool> VerifyPassword(Guid userId, string password, CancellationToken cancellationToken);
}

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly IAccountDataProvider _dataProvider;

    public AccountService(IMapper mapper, IAccountDataProvider dataProvider)
    {
        _mapper = mapper;
        _dataProvider = dataProvider;
    }

    public async Task<GetUserAccountResponse> GetUserAccountAsync(
        GetUserAccountRequest request, CancellationToken cancellationToken)
    {
        var user = await _dataProvider.GetUserAccountAsync(
            request.UserId, request.DisplayName, cancellationToken).ConfigureAwait(false);

        return new GetUserAccountResponse
        {
            Data = _mapper.Map<UserAccountInfoDto>(user)
        };
    }

    public async Task<GetUserAccountByThirdPartyResponse> GetUserAccountByThirdPartyAsync(
        GetUserAccountByThirdPartyRequest request, CancellationToken cancellationToken)
    {
        var user = await _dataProvider.GetUserAccountByThirdPartyAsync(
            request.Platform.ToString(), request.ThirdPartyUserId, cancellationToken).ConfigureAwait(false);

        return new GetUserAccountByThirdPartyResponse
        {
            Data = _mapper.Map<UserAccountInfoDto>(user)
        };
    }

    public async Task<GetThirdPartyByThirdPartyResponse> GetThirdPartyByThirdPartyAsync(
        GetThirdPartyByThirdPartyRequest request, CancellationToken cancellationToken)
    {
        var thirdParty = await _dataProvider.GetThirdPartyByThirdPartyAsync(
            request.CurrentPlatform.ToString(), request.ThirdPartyUserId,
            request.TargetPlatform.ToString(), cancellationToken).ConfigureAwait(false);

        return new GetThirdPartyByThirdPartyResponse
        {
            Data = thirdParty
        };
    }

    public async Task<RegisterUserAccountResponse> RegisterUserAccountAsync(
        RegisterUserAccountCommand command, CancellationToken cancellationToken)
    {
        var user = await _dataProvider.RegisterUserAccountAsync(
            command.Name, command.Password.SHAEncrypt(), cancellationToken).ConfigureAwait(false);

        return new RegisterUserAccountResponse
        {
            Data = _mapper.Map<UserAccountInfoDto>(user)
        };
    }

    public async Task<LinkUserAccountFromThirdPartyResponse> LinkAccountFromThirdPartyAsync(
        LinkUserAccountFromThirdPartyCommand command, CancellationToken cancellationToken)
    {
        var getUserByThirdPartyResponse = await GetUserAccountByThirdPartyAsync(
            new GetUserAccountByThirdPartyRequest
            {
                Platform = command.Authorization.Platform,
                ThirdPartyUserId = command.Authorization.AccountId
            }, cancellationToken).ConfigureAwait(false);

        if (getUserByThirdPartyResponse.Data == null)
        {
            UserAccountInfoDto userInfo;

            if (string.IsNullOrEmpty(command.MasterDisplayName))
            {
                var registerResponse = await RegisterUserAccountAsync(new RegisterUserAccountCommand
                {
                    Name = command.Authorization.AccountName,
                    Password = "".GenerateRandomString()
                }, cancellationToken).ConfigureAwait(false);

                userInfo = registerResponse.Data ?? throw new Exception($"创建{AppPlatformEnum.Self}账号失败.");
            }
            else
            {
                var getUserAccountResponse = await GetUserAccountAsync(
                    new GetUserAccountRequest
                    {
                        DisplayName = command.MasterDisplayName
                    }, cancellationToken).ConfigureAwait(false);

                userInfo = getUserAccountResponse.Data ?? throw new Exception($"未找到对应{AppPlatformEnum.Self}账号.");
            }

            var thirdParty = new UserThirdPartyDto
            {
                Id = Guid.NewGuid(),
                UserId = userInfo.Id,
                Provider = command.Authorization.Platform.ToString(),
                ThirdPartyUserId = command.Authorization.AccountId,
                AccessToken = command.AccessToken,
                AccessTokenExpiresAt = command.AccessTokenExpiresAt,
                RefreshToken = command.RefreshToken,
                RefreshTokenExpiresAt = command.RefreshTokenExpiresAt,
                ExtraData = command.ExtraData
            };

            await _dataProvider.LinkThirdPartyAsync(thirdParty, cancellationToken).ConfigureAwait(false);
        }
        else
        {
            var thirdParty = await _dataProvider.GetThirdPartyByUserIdAsync(
                getUserByThirdPartyResponse.Data.Id,
                command.Authorization.Platform.ToString(), cancellationToken).ConfigureAwait(false);

            if (thirdParty != null)
            {
                thirdParty.AccessToken = command.AccessToken;
                thirdParty.AccessTokenExpiresAt = command.AccessTokenExpiresAt;
                thirdParty.RefreshToken = command.RefreshToken;
                thirdParty.RefreshTokenExpiresAt = command.RefreshTokenExpiresAt;
                thirdParty.ExtraData = command.ExtraData;

                await _dataProvider.UpdateThirdPartyAsync(thirdParty, cancellationToken).ConfigureAwait(false);
            }
        }

        return new LinkUserAccountFromThirdPartyResponse();
    }

    public async Task<ChangeUserAccountPasswordResponse> ChangeUserAccountPasswordAsync(
        ChangeUserAccountPasswordCommand command, CancellationToken cancellationToken)
    {
        //先获取账户 需要ID
        var user = await _dataProvider.GetUserAccountAsync(
        command.UserId, string.Empty, cancellationToken).ConfigureAwait(false);

        //先判断用户是否为空
        if (user == null)
        {
            return new ChangeUserAccountPasswordResponse();
        }

        //然后判断输入的密码是否正确,再验证新密码与确认密码是否一致
        if (!command.OldPassword.SHAEncrypt().Equals(user.Password) || !command.NewPassword.Equals(command.NewPasswordConfirm))
        {
            return new ChangeUserAccountPasswordResponse();
        }

        //最后保存数据库
        user.Password = command.NewPassword.SHAEncrypt();

        await _dataProvider.UpdateUserAccountAsync(user, cancellationToken).ConfigureAwait(false);

        return new ChangeUserAccountPasswordResponse();
    }

    public async Task<bool> VerifyPassword(Guid userId, string password, CancellationToken cancellationToken)
    {
        var user = await _dataProvider.GetUserAccountAsync(userId, string.Empty, cancellationToken).ConfigureAwait(false);

        return user != null && user.Password == password.SHAEncrypt();
    }
}