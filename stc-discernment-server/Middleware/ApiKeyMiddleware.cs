using System;
using stc_discernment_server.Models;
namespace stc_discernment_server.Middleware {

    public class ApiKeyMiddleware {

        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "x-stc-api-key";

        public ApiKeyMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AppDbContext db) {

            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey)) {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key was invalid or not provided.");
                return;
            }

            await _next(context);
        }
    }
}

