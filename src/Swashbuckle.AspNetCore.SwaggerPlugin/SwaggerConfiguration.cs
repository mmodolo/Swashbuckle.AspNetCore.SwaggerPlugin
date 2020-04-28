using Swashbuckle.AspNetCore.SwaggerPlugin.Enums;
using Swashbuckle.AspNetCore.SwaggerPlugin.Interfaces;

namespace Swashbuckle.AspNetCore.SwaggerPlugin
{
    public class SwaggerConfiguration : ISwaggerConfiguration
    {
        public string Description { get; set; }
        public string PageTitle { get; set; }
        public string Title { get; set; }
        public string RoutePrefix { get; set; }
        public string JsonRoute { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string DeprecatedMessage { get; set; }
        public SwaggerAuthentications Authentication { get; set; }
    }
}
