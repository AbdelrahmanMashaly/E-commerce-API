namespace Talabat.APIs.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection SwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static WebApplication SwaggerMiddleware(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
