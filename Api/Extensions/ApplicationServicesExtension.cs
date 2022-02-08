using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Errors;
using Dal.Repositories;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Api.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IHotelsRepository, HotelsRepository>();
            services.AddScoped<IRoomsRepository, RoomsRepository>();
            services.AddScoped<IReservationsRepository, ReservationsRepository>();
            services.AddScoped<IReservationService, ReservationService>();

            services.Configure<ApiBehaviorOptions>(
                options => {
                    options.InvalidModelStateResponseFactory = actionContext => {
                        var errors = actionContext.ModelState
                            .Where(e => e.Value.Errors.Count > 0)
                            .SelectMany(x => x.Value.Errors)
                            .Select(e => e.ErrorMessage).ToArray();

                        var errorResponse = new ApiValidationErrorResponse { Errors = errors };

                        return new BadRequestObjectResult(errorResponse);
                    };
                }
            );

            return services;
        }
    }
}