using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Swashbuckle.AspNetCore.SwaggerPlugin.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Swashbuckle.AspNetCore.SwaggerPlugin.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services, IConfiguration configuration, string xmlFileName = null)
        {
            services.AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;
                }
            );
            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                }
            );

            var swaggerConfig = new SwaggerConfiguration();
            configuration.GetSection(nameof(SwaggerConfiguration)).Bind(swaggerConfig);
            services.AddSingleton<ISwaggerConfiguration>(swaggerConfig);

            services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    // integrate xml comments
                    if (!string.IsNullOrEmpty(xmlFileName))
                    {                        
                        options.IncludeXmlComments(XmlCommentsFilePath(xmlFileName));
                    }
                }
            );

            return services;
        }

        public static IApplicationBuilder AddUseSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider, ISwaggerConfiguration swaggerConfig)
        {
            app.UseSwagger(options =>
            {
                options.RouteTemplate = swaggerConfig.JsonRoute;
            });

            app.UseSwaggerUI(options =>
            {
                // build a swagger endpoint for each discovered API version
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.DocumentTitle = swaggerConfig.PageTitle;
                    options.RoutePrefix = swaggerConfig.RoutePrefix;
                    options.SwaggerEndpoint($"/{swaggerConfig.RoutePrefix}/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            return app;
        }

        private static string XmlCommentsFilePath(string fileName)
        {
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            return Path.Combine(basePath, fileName);
        }
    }
}
