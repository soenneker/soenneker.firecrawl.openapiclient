using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Firecrawl.OpenApiClient.Tests;

[Collection("Collection")]
public sealed class FirecrawlOpenApiClientTests : FixturedUnitTest
{
    public FirecrawlOpenApiClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public void Default()
    {

    }
}
