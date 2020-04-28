using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerPlugin.Enums;
using Swashbuckle.AspNetCore.SwaggerPlugin.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerPlugin.Extensions;

namespace Swashbuckle.AspNetCore.SwaggerPlugin
{
    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly ISwaggerConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        /// <param name="config">Swagger Configuration from the Settings</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, ISwaggerConfiguration config)
        {
            _provider = provider;
            _config = config;
        }
        

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }


            switch (_config.Authentication)
            {
                case SwaggerAuthentications.ApiKey:
                case SwaggerAuthentications.AppId:
                case SwaggerAuthentications.OAuth:
                    options.AddAuthentication(_config.Authentication);
                    break;
                case SwaggerAuthentications.AppIdAndApiKey:                    
                    options.AddAuthentication(SwaggerAuthentications.AppId);
                    options.AddAuthentication(SwaggerAuthentications.ApiKey);
                    break;
                default:
                    break;
            }
        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = _config.Title,
                Version = description.GroupName,
                Description = _config.Description,
                Contact = new OpenApiContact() { Name = _config.ContactName, Email = _config.ContactEmail },
                //License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            };

            if (description.IsDeprecated)
            {
                info.Description += $" - {_config.DeprecatedMessage}";
            }

            return info;
        }
    }
}
