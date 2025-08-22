using System.Net;
using System.Text;
using System.Text.Json;

namespace Almostengr.Common.Infrastructure;

public static class HttpClientExtensions
{
    public static StringContent SerializeRequestBody<TResource>(this TResource request) where TResource : class
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        string json = JsonSerializer.Serialize(request);
        StringContent content = new(json, Encoding.UTF8, "application/json");
        return content;
    }

    public static async Task<TResource> DeserializeResponseBodyAsync<TResource>(this HttpResponseMessage response) where TResource : class
    {
        ArgumentNullException.ThrowIfNull(response, nameof(response));

        string result = await response.Content.ReadAsStringAsync();

        if (response.StatusCode >= HttpStatusCode.InternalServerError || response.StatusCode == HttpStatusCode.RequestTimeout)
        {
            throw new ServerErrorException(response.StatusCode, result);
        }

        JsonSerializerOptions serializeOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        return JsonSerializer.Deserialize<TResource>(result, serializeOptions)!;
    }

    public static async Task<bool> GetBoolAsync(this HttpClient httpClient, string route)
    {
        ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));
        ArgumentNullException.ThrowIfNull(route, nameof(route));

        var response = await httpClient.GetAsync(route);
        return response.IsSuccessStatusCode;
    }

    public static async Task<string> GetStringAsync<TResource>(this HttpClient httpClient, string route) where TResource : class
    {
        ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));
        ArgumentNullException.ThrowIfNull(route, nameof(route));

        var response = await httpClient.GetAsync(route);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<TResource> GetAsync<TResource>(this HttpClient httpClient, string route) where TResource : class
    {
        ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));
        ArgumentNullException.ThrowIfNull(route, nameof(route));

        var response = await httpClient.GetAsync(route);
        return await response.DeserializeResponseBodyAsync<TResource>();
    }

    public static async Task<XResource> PostAsync<TResource, XResource>(this HttpClient httpClient, string route, TResource request)
        where TResource : class where XResource : class
    {
        ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));
        ArgumentNullException.ThrowIfNull(route, nameof(route));
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var serializedRequest = request.SerializeRequestBody();
        var response = await httpClient.PostAsync(route, serializedRequest);
        return await response.DeserializeResponseBodyAsync<XResource>();
    }

    public static async Task<XResource> PutAsync<TResource, XResource>(this HttpClient httpClient, string route, TResource request)
        where TResource : class where XResource : class
    {
        ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));
        ArgumentNullException.ThrowIfNull(route, nameof(route));
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var serializedRequest = request.SerializeRequestBody();
        var response = await httpClient.PutAsync(route, serializedRequest);
        return await response.DeserializeResponseBodyAsync<XResource>();
    }

    public static async Task<XResource> DeleteAsync<TResource, XResource>(this HttpClient httpClient, string route)
        where TResource : class where XResource : class
    {
        ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));
        ArgumentNullException.ThrowIfNull(route, nameof(route));

        var response = await httpClient.DeleteAsync(route);
        return await response.DeserializeResponseBodyAsync<XResource>();
    }
}
