[![](https://img.shields.io/nuget/v/soenneker.firecrawl.openapiclient.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.firecrawl.openapiclient/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.firecrawl.openapiclient/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.firecrawl.openapiclient/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.firecrawl.openapiclient.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.firecrawl.openapiclient/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.firecrawl.openapiclient/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.firecrawl.openapiclient/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Firecrawl.OpenApiClient

A Kiota-generated .NET client for Firecrawl's v2 API.

## Installation

```bash
dotnet add package Soenneker.Firecrawl.OpenApiClient
```

## Recommended setup

For managed API-key authentication and cached client reuse, install the companion package:

```bash
dotnet add package Soenneker.Firecrawl.OpenApiClientUtil
```

```csharp
using Soenneker.Firecrawl.OpenApiClientUtil.Registrars;

services.AddFirecrawlOpenApiClientUtilAsScoped();
```

Configure `Firecrawl:ApiKey`, inject `IFirecrawlOpenApiClientUtil`, and call `Get()` inside the scope. The generated-client utility is scoped while its authenticated HTTP transport remains singleton.

## Direct construction

```csharp
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Firecrawl.OpenApiClient;
using Soenneker.Firecrawl.OpenApiClient.Models;
using Soenneker.Firecrawl.OpenApiClient.Scrape;

var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://api.firecrawl.dev/v2")
};
httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

var adapter = new HttpClientRequestAdapter(
    new AnonymousAuthenticationProvider(),
    httpClient: httpClient);
var firecrawl = new FirecrawlOpenApiClient(adapter);

ScrapeResponse? response = await firecrawl.Scrape.PostAsync(
    new ScrapePostRequestBody { Url = "https://example.com" },
    cancellationToken: cancellationToken);
```

The anonymous Kiota provider is appropriate because this dedicated `HttpClient` already carries the bearer header. Never attach the Firecrawl key to a shared client that can send default headers to unrelated hosts.

## Navigating the client

Top-level request builders correspond to Firecrawl resources such as `Scrape`, `Crawl`, `Batch`, `Map`, `Search`, `Extract`, `Agent`, `Monitor`, and `Team`. Item builders use indexers for path parameters. Endpoint methods accept a request body when required, an optional request-configuration callback, and a cancellation token.

Responses may be nullable when the schema permits an empty response. Kiota maps documented service errors to generated error models; authentication, transport, and serialization failures surface as HTTP or Kiota exceptions.

## Safety and cost boundaries

Firecrawl can make outbound requests and consume paid credits. Do not pass arbitrary user-supplied URLs or request headers without an application policy. Restrict allowed schemes and destinations, reject internal/private network targets where appropriate, and never forward cookies or credentials for unrelated systems. Cancellation does not guarantee that Firecrawl has not already started or billed remote work.

## Generated-code boundaries

Public names and model shapes follow Firecrawl's OpenAPI description and can change when the client is regenerated. Files under `src/Soenneker.Firecrawl.OpenApiClient` are generated; keep application policy and convenience methods in a separate project or the companion utility.
