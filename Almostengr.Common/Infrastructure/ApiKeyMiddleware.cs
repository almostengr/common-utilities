using Almostengr.Common.DomainServices.Interfaces;
using Almostengr.Common.Shared;
using Microsoft.AspNetCore.Http;

namespace Almostengr.Common.Infrastructure;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    public readonly IApiKeyService _apiKeyService;

    public ApiKeyMiddleware(
        RequestDelegate next,
        IApiKeyService apiKeyService
    )
    {
        _next = next;
        _apiKeyService = apiKeyService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("X-Api-Key", out var apiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(LibConstants.InvalidApiKey);
            return;
        }

        if (!await _apiKeyService.IsValidApiKeyAsync(apiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(LibConstants.InvalidApiKey);
            return;
        }

        await _next(context);
    }
}
