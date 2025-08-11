using TemplateProject.Core.Caching;
using TemplateProject.Core.Services.Jobs;
using IHttpClientFactory = TemplateProject.Core.Services.Http.IHttpClientFactory;

namespace TemplateProject.Core.Services.Sync;

public partial interface ISyncService : IScope
{
}

public partial class SyncService : ISyncService
{
    private IBackgroundJobClient _backgroundJob;
    private ICacheManager _cacheManager;
    private IHttpClientFactory _httpClientFactory;

    public SyncService(
        IBackgroundJobClient backgroundJob,
        ICacheManager cacheManager,
        IHttpClientFactory httpClientFactory)
    {
        _backgroundJob = backgroundJob;
        _cacheManager = cacheManager;
        _httpClientFactory = httpClientFactory;
    }
}