using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ImplementingCustomMiddlewareApi.Middlewares.Swagger
{
    public class AddApiKeyParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            var keyParam = new OpenApiParameter
            {
                Name = "api_key",
                In = ParameterLocation.Query,
                Description = "API Key",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            };

            operation.Parameters.Add(keyParam);
        }
    }
}
