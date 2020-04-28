using Swashbuckle.AspNetCore.SwaggerPlugin.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swashbuckle.AspNetCore.SwaggerPlugin.Interfaces
{
    public interface ISwaggerConfiguration
    {
        string Description { get; set; }
        string PageTitle { get; set; }
        string Title { get; set; }
        string RoutePrefix { get; set; }
        string JsonRoute { get; set; }
        string ContactName { get; set; }
        string ContactEmail { get; set; }
        string DeprecatedMessage { get; set; }
        SwaggerAuthentications Authentication { get; set; }
    }
}
