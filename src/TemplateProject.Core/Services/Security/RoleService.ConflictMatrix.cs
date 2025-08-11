using System.Net;
using TemplateProject.Message.Commands.Security;
using TemplateProject.Message.Dto.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Services.Security;

public partial interface IRoleService : IScope
{
    public Task<GetRoleConflictMatrixListResponse> GetRoleConflictMatrixListAsync(
        GetRoleConflictMatrixListRequest request, CancellationToken cancellationToken);
 
    public Task<AddRoleConflictMatrixResponse> AddRoleConflictMatrixAsync(
        AddRoleConflictMatrixCommand command, CancellationToken cancellationToken);
    
    public Task<DeleteRoleConflictMatrixResponse> DeleteRoleConflictMatrixAsync(
        DeleteRoleConflictMatrixCommand command, CancellationToken cancellationToken);
    
    public Task<CheckRoleConflictExistsResponse> CheckRoleConflictExistsAsync(
        CheckRoleConflictExistsRequest request, CancellationToken cancellationToken);
}

public partial class RoleService : IRoleService
{
    public async Task<GetRoleConflictMatrixListResponse> GetRoleConflictMatrixListAsync(
        GetRoleConflictMatrixListRequest request, CancellationToken cancellationToken)
    {
        var roleIdList = request.RoleIdList.Distinct().ToList();
        
        var list = await _roleDataProvider.GetRoleConflictMatrixListAsync(roleIdList, cancellationToken).ConfigureAwait(false);

        return new GetRoleConflictMatrixListResponse
        {
            Data = _mapper.Map<List<RoleConflictMatrixDto>>(list)
        };
    }

    public async Task<AddRoleConflictMatrixResponse> AddRoleConflictMatrixAsync(
        AddRoleConflictMatrixCommand command, CancellationToken cancellationToken)
    {
        var response = new AddRoleConflictMatrixResponse();

        var checkResponse = await CheckRoleConflictExistsAsync(
            new CheckRoleConflictExistsRequest
            {
                RoleAId = command.RoleAId,
                RoleBId = command.RoleBId
            }, cancellationToken).ConfigureAwait(false);
        
        if (checkResponse.Data)
        {
            response.Code = HttpStatusCode.BadRequest;
            response.Message = "Conflict matrix already exists";
            return response;
        }
        
        await _roleDataProvider.AddRoleConflictMatrixAsync(
            command.RoleAId, command.RoleBId, cancellationToken).ConfigureAwait(false);

        return response;
    }

    public async Task<DeleteRoleConflictMatrixResponse> DeleteRoleConflictMatrixAsync(
        DeleteRoleConflictMatrixCommand command, CancellationToken cancellationToken)
    {
        await _roleDataProvider.DeleteRoleConflictMatrixAsync([command.Id], cancellationToken).ConfigureAwait(false);
        
        return new DeleteRoleConflictMatrixResponse();
    }

    public async Task<CheckRoleConflictExistsResponse> CheckRoleConflictExistsAsync(
        CheckRoleConflictExistsRequest request, CancellationToken cancellationToken)
    {
        var roleConflictMatrixList = await _roleDataProvider
            .GetRoleConflictMatrixListAsync([request.RoleAId], cancellationToken: cancellationToken).ConfigureAwait(false);
    
        var hasConflict = roleConflictMatrixList.Any(x => 
            (x.RoleAId == request.RoleAId && x.RoleBId == request.RoleBId) || 
            (x.RoleAId == request.RoleBId && x.RoleBId == request.RoleAId));
    
        return new CheckRoleConflictExistsResponse
        {
            Data = hasConflict
        };
    }
}