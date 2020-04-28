using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerPlugin.Enums;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swashbuckle.AspNetCore.SwaggerPlugin.Extensions
{
    public static class SwaggerGenOptionsExtension
    {
        private const string _API_KEY_ID = "Api-Key-Authentication";
        private const string _APP_ID_ID = "App-Id-Authentication";
        private const string _OAUTH_ID = "OAuth-Authentication";

        private const string _API_KEY_NAME = "x-api-key";
        private const string _APP_ID_NAME = "x-app-id";
        private const string _OAUTH_NAME = "authorization";

        private const string _API_KEY_SCHEME = "ApiKeyScheme";
        private const string _APP_ID_SCHEME = "ApiKeyScheme";
        private const string _OAUTH_SCHEME = "bearer";
        public static SwaggerGenOptions AddAuthentication(this SwaggerGenOptions options, SwaggerAuthentications authenticationType)
        {
            string id;
            string name;
            string scheme;
            string description;
            SecuritySchemeType type;

            switch (authenticationType)
            {
                case SwaggerAuthentications.ApiKey:
                    id = _API_KEY_ID;
                    name = _API_KEY_NAME;
                    scheme = _API_KEY_SCHEME;
                    type = SecuritySchemeType.ApiKey;
                    description = "ApiKey must be in the header.";
                    break;
                case SwaggerAuthentications.AppId:
                    id = _APP_ID_ID;
                    name = _APP_ID_NAME;
                    scheme = _APP_ID_SCHEME;
                    type = SecuritySchemeType.ApiKey;
                    description = "AppId must be in the header.";
                    break;
                case SwaggerAuthentications.OAuth:
                    id = _OAUTH_ID;
                    name = _OAUTH_NAME;
                    scheme = _OAUTH_SCHEME;
                    type = SecuritySchemeType.Http;
                    description = "JWT Bearer Scheme.";
                    break;
                default:
                    return options;
            }

            options.AddSecurityDefinition(id, new OpenApiSecurityScheme
            {
                Description = description,
                Type = type,
                Name = name,
                In = ParameterLocation.Header,
                Scheme = scheme
            });

            var key = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = id
                },
                In = ParameterLocation.Header
            };

            var requirement = new OpenApiSecurityRequirement
                {
                   { key, new List<string>() }
                };

            options.AddSecurityRequirement(requirement);

            return options;
        }
    }
}
