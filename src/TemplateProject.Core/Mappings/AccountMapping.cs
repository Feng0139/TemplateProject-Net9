using AutoMapper;
using TemplateProject.Core.Domain.Account;
using TemplateProject.Message.Dto.Account;

namespace TemplateProject.Core.Mappings;

public class AccountMapping : Profile
{
    public AccountMapping()
    {
        CreateMap<UserAccount, UserAccountDto>().ReverseMap();
        CreateMap<UserAccount, UserAccountInfoDto>().ReverseMap();
        CreateMap<UserAccountDto, UserAccountInfoDto>().ReverseMap();
        
        CreateMap<UserThirdParty, UserThirdPartyDto>().ReverseMap();
        CreateMap<UserThirdParty, UserThirdPartyInfoDto>().ReverseMap();
        CreateMap<UserThirdPartyDto, UserThirdPartyInfoDto>().ReverseMap();
    }
}