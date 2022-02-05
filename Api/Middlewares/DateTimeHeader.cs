using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    public class DateTimeHeader
    {
        private readonly RequestDelegate _next;
        public DateTimeHeader(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext){
            httpContext.Request.Headers.Add("New-Key", DateOnly.FromDateTime(DateTime.Today).ToString());
            await Task.FromResult(_next(httpContext));
        }
    }

    public static class DateTimeHeaderExtensions{
        public static IApplicationBuilder UseDateTimeHeader(this IApplicationBuilder builder){
            return builder.UseMiddleware<DateTimeHeader>();
        }
    }
}

