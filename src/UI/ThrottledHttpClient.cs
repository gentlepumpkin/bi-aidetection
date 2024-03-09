using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;

//this class is used to throttle the number of requests to the same server so that multiple requests dont overlap and cause timeout errors
public class ThrottledHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ConcurrentDictionary<string, SemaphoreSlim> _semaphores;
    private readonly TimeSpan _semaphoreTimeout;

    public ThrottledHttpClient(TimeSpan semaphoreTimeout)
    {
        _httpClient = new HttpClient();
        _semaphores = new ConcurrentDictionary<string, SemaphoreSlim>();
        _semaphoreTimeout = semaphoreTimeout;
    }

    public TimeSpan Timeout
    {
        get => _httpClient.Timeout;
        set => _httpClient.Timeout = value;
    }

    public HttpRequestHeaders DefaultRequestHeaders => _httpClient.DefaultRequestHeaders;

    public async Task<HttpResponseMessage> GetAsync(string uri, bool WaitIfInUse = true)
    {
        if (!Uri.TryCreate(uri, UriKind.Absolute, out Uri parsedUri))
        {
            throw new ArgumentException("Invalid URI string.", nameof(uri));
        }

        return await GetAsync(parsedUri, WaitIfInUse);
    }

    public async Task<HttpResponseMessage> GetAsync(Uri uri, bool WaitIfInUse = true)
    {
        string hostKey = $"{uri.Host}:{uri.Port}";
        var semaphore = _semaphores.GetOrAdd(hostKey, new SemaphoreSlim(1, 1));

        try
        {
            if (WaitIfInUse)
            {
                await semaphore.WaitAsync(_semaphoreTimeout);
            }
            else if (!semaphore.Wait(0))
            {
                return null; // Immediate return with null if semaphore is not immediately available.
            }

            // Proceed with the request as the semaphore has been acquired
            return await _httpClient.GetAsync(uri);
        }
        catch
        {
            throw;
        }
        finally
        {
            semaphore.Release();
        }
    }
}

// Usage example:
// var throttledClient = new ThrottledHttpClient(TimeSpan.FromSeconds(10));
// throttledClient.Timeout = TimeSpan.FromSeconds(30);
// var response = await throttledClient.GetAsync(new Uri("http://example.com"));

