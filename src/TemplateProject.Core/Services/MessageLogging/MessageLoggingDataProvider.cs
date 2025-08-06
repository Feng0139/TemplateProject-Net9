using TemplateProject.Core.Data;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Extension;

namespace TemplateProject.Core.Services.MessageLogging;

public interface IMessageLoggingDataProvider : IScope
{
    Task<PagedList<MessageLog>> GetFilterAsync(
        List<Guid>? ids = default,
        DateTimeOffset? startDate = null,
        DateTimeOffset? endDate = null,
        int pageIndex = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default);
    
    Task AddMessageLogsAsync(List<MessageLog> messageLogs, CancellationToken cancellationToken);
}

public class MessageLoggingDataProvider(IRepository<MessageLog> messageRep) : IMessageLoggingDataProvider
{
    public async Task<PagedList<MessageLog>> GetFilterAsync(
        List<Guid>? ids = default,
        DateTimeOffset? startDate = null,
        DateTimeOffset? endDate = null,
        int pageIndex = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var query = messageRep.QueryNoTracking()
            .WhereIF(!ids.IsNullOrEmpty(), x => ids != null && ids.Contains(x.Id))
            .WhereIF(startDate != null, x => x.CreatedAt >= startDate)
            .WhereIF(endDate != null, x => x.CreatedAt <= endDate);
        
        var entities = await query.OrderByDescending(x => x.CreatedAt)
            .ToPagedAsync(pageIndex, pageSize, cancellationToken).ConfigureAwait(false);

        return entities;
    }

    public async Task AddMessageLogsAsync(List<MessageLog> messageLogs, CancellationToken cancellationToken)
    {
        await messageRep.InsertManyAsync(messageLogs, cancellationToken).ConfigureAwait(false);
    }
}