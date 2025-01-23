namespace Agile.Middlewares
{
    public class NgrokSkipBrowserWarningMiddleware
    {
        private readonly RequestDelegate _next;

        public NgrokSkipBrowserWarningMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("ngrok-skip-browser-warning", "true");
            await _next(context);
        }
    }
}
