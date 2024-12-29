using Microsoft.AspNetCore.Mvc;
using StudentTracker.APIs.Errors;
using StudentTracker.APIs.Helpers;
using StudentTracker.Core.Repositories.Contract;
using StudentTracker.Repository;
using StudentTracker.Service;

namespace StudentTracker.APIs.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(typeof(MappingProfiles));

            //services.AddHostedService<LectureCloningService>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                         .SelectMany(P => P.Value.Errors)
                                                         .Select(E => E.ErrorMessage)
                                                         .ToArray();
                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowNgrok",
                    builder =>
                    {//https://*.ngrok-free.app
                        builder.WithOrigins("https://*.ngrok-free.app") // Use your actual Ngrok URL
                               .SetIsOriginAllowedToAllowWildcardSubdomains()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            return services;
        }
    }
}
