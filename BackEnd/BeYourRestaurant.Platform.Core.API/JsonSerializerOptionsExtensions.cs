using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BeYourRestaurant.Platform.Core.API
{
    /// <summary>
    /// Extension method for <see cref="JsonSerializerOptions"/>
    /// </summary>
    public static class JsonSerializerOptionsExtensions
    {
        /// <summary>
        /// Configure the default options for <paramref name="jsonSerializerOptions"/> in API services
        /// </summary>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns>Default configurations</returns>
        public static JsonSerializerOptions AddDefaultOptions(this JsonSerializerOptions jsonSerializerOptions)
        {
            jsonSerializerOptions.IgnoreNullValues = true;
            jsonSerializerOptions.PropertyNameCaseInsensitive = true;
            jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            return jsonSerializerOptions;
        }
    }
}
