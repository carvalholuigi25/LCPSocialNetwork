using LCPSNWebApi.Classes;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace LCPSNWebApi.Filters
{
    public class ParameterFilter<T> : IParameterFilter where T : class
    {
        public ParameterFilter()
        {
        }

        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            var strcomp = StringComparison.InvariantCultureIgnoreCase;

            if (typeof(T).Name == "Post" && parameter.Name.Equals("SortBy", strcomp))
            {
                parameter.Schema.Enum = typeof(Post).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(x => new OpenApiString(x.Name)).ToList<IOpenApiAny>();     
            }
            
            if(typeof(T).Name == "Reaction" && parameter.Name.Equals("SortBy", strcomp)) {
                parameter.Schema.Enum = typeof(Reaction).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(x => new OpenApiString(x.Name)).ToList<IOpenApiAny>();
            }
        }
    }
}