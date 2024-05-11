using BusinessLogic.Layer.Interfaces;
using BusinessLogic.Layer.Repository;
using Microsoft.AspNetCore.Mvc;
using SigmaTaskAPI.Errors;
using SigmaTaskAPI.Mapping;

namespace SigmaTaskAPI.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddMemoryCache();
            services.AddScoped(typeof(ICandidateRepository), typeof(CandidateRepository));
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count > 0)
                                 .SelectMany(P => P.Value.Errors).Select(P => P.ErrorMessage).ToArray();

                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });
            return services;
        }
    }
}
