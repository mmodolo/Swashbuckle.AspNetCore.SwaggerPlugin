using System;
using System.Collections.Generic;
using System.Text;

namespace Swashbuckle.AspNetCore.SwaggerPlugin.Enums
{
    public enum SwaggerAuthentications
    {
        /// <summary>
        /// Do not use Authentication
        /// </summary>
        None = 0,

        /// <summary>
        /// Use API key
        /// </summary>
        ApiKey = 1,


        /// <summary>
        /// Use AppId
        /// </summary>
        AppId = 2,

        /// <summary>
        /// Use AppId and API key
        /// </summary>
        AppIdAndApiKey = 3,

        /// <summary>
        /// Use basic or bearer token authorization header.
        /// </summary>
        OAuth = 4
    }
}
