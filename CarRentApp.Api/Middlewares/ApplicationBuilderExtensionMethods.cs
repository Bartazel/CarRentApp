namespace CarRentApp.Api.Middlewares
{
    public static class ApplicationBuilderExtensionMethods
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
