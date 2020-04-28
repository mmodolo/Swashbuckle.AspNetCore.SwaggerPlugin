using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Swashbuckle.AspNetCore.SwaggerPlugin.SampleAPI.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiAccessTokenAuthAttribute : Attribute, IAsyncActionFilter
    {
        //TODO: mover para o settings
        private const string _ApiAccessTokenHeaderName = "x-api-key";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            if (!context.HttpContext.Request.Headers.TryGetValue(_ApiAccessTokenHeaderName, out var potentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();

        }
    }
}
