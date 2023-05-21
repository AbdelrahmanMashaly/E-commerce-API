using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Domain.IRepository;
using Talabat.Repository;

namespace Talabat.APIs.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
           services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(typeof(MappingProfile));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (action) =>
                {
                    var errors = action.ModelState.Where(p => p.Value.Errors.Count() > 0).SelectMany(p => p.Value.Errors)
                    .Select(e => e.ErrorMessage).ToArray();

                    var ValidationErrorResponse = new ApiValidationErrorResponse(errors)
                    {
                    };

                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });

            return services;
        }
    }
}
