using Shouldly;
using IHttpClientFactory = TemplateProject.Core.Services.Http.IHttpClientFactory;

namespace TemplateProject.IntegrationTest.Services.Http;

public class HttpClientFixture(IntegrationTestBase testBase) : IntegrationFixture(testBase)
{
    [Fact]
    public async Task RequestAsync()
    {
        await Run<IHttpClientFactory>(async factory =>
        {
            var response = await factory.GetAsync<string>("http://www.baidu.com", default);

            response.Data.ShouldNotBeNull();
        });
    }
}