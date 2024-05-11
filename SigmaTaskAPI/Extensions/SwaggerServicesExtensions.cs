namespace SigmaTaskAPI.Extensions
{
    public  static class SwaggerServicesExtensions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //Swagger is a tool that helps you document and visualize your APIs.
            //1.The API Explorer is responsible for providing access to information about the application's endpoints
            //such as their routes, HTTP methods, and parameters
            services.AddEndpointsApiExplorer();
            //2.The SwaggerGen package generates Swagger documentation for your API based on the application's endpoints
            //and the metadata provided by the API Explorer
            services.AddSwaggerGen();
            //Once Swagger is configured, you can access the generated Swagger UI,
            //which provides an interactive interface to explore and test your API endpoints.
            //It allows you to view and test API requests and responses,
            //experiment with different input values, and understand how your API works without the need for external tools.
            return services;
        }

        public static WebApplication UseSwaggerMiddleWares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
