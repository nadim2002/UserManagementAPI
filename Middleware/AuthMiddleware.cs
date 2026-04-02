using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var path = context.Request.Path.ToString().ToLower();

        // ✅ Allow Swagger and root without token
        if (path.StartsWith("/swagger") || path == "/")
        {
            await _next(context);
            return;
        }

        var token = context.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(token) || token != "mysecrettoken")
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await _next(context);
    }
}