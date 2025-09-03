using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;

namespace Micro.Web.Refit;

public class AccessTokenHandler(IHttpContextAccessor httpContextAccessor)
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationtoken)
    {
        var context = httpContextAccessor.HttpContext;
    
        if (context == null) return await base.SendAsync(request, cancellationtoken);
        
        var token = await context.GetTokenAsync("access_token");
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationtoken);
    }
}