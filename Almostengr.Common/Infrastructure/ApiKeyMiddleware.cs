using Almostengr.Common.DomainServices.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Almostengr.Common.Infrastructure;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    public readonly IApiKeyService _apiKeyService;
    private const string INVALID_MESSAGE = "Invalid API Key";

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
            await context.Response.WriteAsync(INVALID_MESSAGE);
            return;
        }

        if (!await _apiKeyService.IsValidApiKeyAsync(apiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(INVALID_MESSAGE);
            return;
        }

        await _next(context);
    }
}
