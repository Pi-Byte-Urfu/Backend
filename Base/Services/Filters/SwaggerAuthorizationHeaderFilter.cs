using Backend.Auth.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Backend.Base.Services.Filters;

public class SwaggerAuthorizationHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation == null)
            throw new Exception("Invalid operation");

        var parametersOfFunction = context.MethodInfo.GetParameters();
        var hasAuthInfoParameter = parametersOfFunction.Any(p => p.CustomAttributes.Any(attr => attr.AttributeType == typeof(FromHeaderAttribute)) && p.ParameterType == typeof(UserAuthInfo));

        if (hasAuthInfoParameter)
        {
            operation.Parameters.Remove(operation.Parameters.Where(param => param.Name == "authInfo").First());
            var openApiParameter = new OpenApiParameter
            {
                Name = "AuthInfo",
                In = ParameterLocation.Header,
                Description = "Custom authentication header",
                Schema = new OpenApiSchema { Format = "json", Example = OpenApiAnyFactory.CreateFromJson("{\"id\": 1, \"userType\": 0}") },
                Required = true
            };
            operation.Parameters.Add(openApiParameter);
        }
    }
}