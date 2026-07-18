using System.Net;
using System.Text;
using System.Text.Json;

namespace Almostengr.Common.Common.Infrastructure;

public static class HttpClientExtensions
{
    public static StringContent SerializeRequestBody<TResource>(this TResource request)
        where TResource : class
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        string json = JsonSerializer.Serialize(request);
        StringContent content = new(json, Encoding.UTF8, "application/json");
        return content;
    }

    public static async Task<TResource> DeserializeResponseBodyAsync<TResource>(this HttpResponseMessage response, bool throwOnBadRequests)
        where TResource : class
    {
        ArgumentNullException.ThrowIfNull(response, nameof(response));

        string result = await response.Content.ReadAsStringAsync();
        ThrowIfBadResponse(response.StatusCode, throwOnBadRequests, result);

        JsonSerializerOptions serializeOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        return JsonSerializer.Deserialize<TResource>(result, serializeOptions)!;
    }

    private static void ThrowIfBadResponse(HttpStatusCode statusCode, bool throwOnBadRequests, string result)
    {
        if (statusCode >= HttpStatusCode.InternalServerError ||
            statusCode == HttpStatusCode.RequestTimeout ||
            (throwOnBadRequests && statusCode >= HttpStatusCode.BadRequest))
        {
            throw new ServerErrorException(statusCode, result);
        }
    }

    public static async Task<bool> GetBoolAsync(this HttpClient httpClient, string route)
    {
        ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));
        ArgumentNullException.ThrowIfNull(route, nameof(route));

        HttpResponseMessage response = await httpClient.GetAsync(route);
        return response.IsSuccessStatusCode;
    }

    public static async Task<string> GetStringAsync<TResource>(this HttpClient httpClient, string route, bool throwOnBadRequests = false)
        where TResource : class
    {
        ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));
        ArgumentNullException.ThrowIfNull(route, nameof(route));

        HttpResponseMessage response = await httpClient.GetAsync(route);
        string result = await response.Content.ReadAsStringAsync();

        ThrowIfBadResponse(response.StatusCode, throwOnBadRequests, result);
        return result;
    }

    public static async Task<TResource> GetAsync<TResource>(this HttpClient httpClient, string route, bool throwOnBadRequests = false)
        where TResource : class
    {
        ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));
        ArgumentNullException.ThrowIfNull(route, nameof(route));

        HttpResponseMessage response = await httpClient.GetAsync(route);
        return await response.DeserializeResponseBodyAsync<TResource>(throwOnBadRequests);
    }

    public static async Task<XResource> PostAsync<TResource, XResource>(this HttpClient httpClient, string route, TResource request, bool throwOnBadRequests = false)
        where TResource : class where XResource : class
    {
        ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));
        ArgumentNullException.ThrowIfNull(route, nameof(route));
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        StringContent serializedRequest = request.SerializeRequestBody();
        HttpResponseMessage response = await httpClient.PostAsync(route, serializedRequest);
        return await response.DeserializeResponseBodyAsync<XResource>(throwOnBadRequests);
    }

    public static async Task<XResource> PutAsync<TResource, XResource>(this HttpClient httpClient, string route, TResource request, bool throwOnBadRequests = false)
        where TResource : class where XResource : class
    {
        ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));
        ArgumentNullException.ThrowIfNull(route, nameof(route));
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        StringContent serializedRequest = request.SerializeRequestBody();
        HttpResponseMessage response = await httpClient.PutAsync(route, serializedRequest);
        return await response.DeserializeResponseBodyAsync<XResource>(throwOnBadRequests);
    }

    public static async Task<XResource> DeleteAsync<TResource, XResource>(this HttpClient httpClient, string route, bool throwOnBadRequests = false)
        where TResource : class where XResource : class
    {
        ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));
        ArgumentNullException.ThrowIfNull(route, nameof(route));

        HttpResponseMessage response = await httpClient.DeleteAsync(route);
        return await response.DeserializeResponseBodyAsync<XResource>(throwOnBadRequests);
    }
}
