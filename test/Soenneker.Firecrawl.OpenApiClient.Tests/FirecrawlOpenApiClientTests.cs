using Soenneker.Tests.HostedUnit;

namespace Soenneker.Firecrawl.OpenApiClient.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class FirecrawlOpenApiClientTests : HostedUnitTest
{
    public FirecrawlOpenApiClientTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {

    }
}
