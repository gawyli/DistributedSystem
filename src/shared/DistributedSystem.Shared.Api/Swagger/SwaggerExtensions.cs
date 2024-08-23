using DistributedSystem.Shared.Core.Abstractions;
using DistributedSystem.Shared.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Api.Swagger;
public static class SwaggerExtensions
{



    public static string? FormatSwaggerSchemaId(this Type endpointParameterType)
    {
        if (endpointParameterType.IsAssignableTo(typeof(IAction)))
        {
            // This is necessary coz several endpoints have param types which are nested classes with the same short name (Command or Query) - and the short name is the default Swagger schemaId.
            // So by default, a type like Storm.DotNet.Core.ProjectAggregate.Queries.AllProjects+Query becomes simply "Query", which causes clashes.
            // We want a type like this to use AllProjectsQuery as its Swagger SchemaId (which is what would have been the deault prior to our introduction of nested classes).
            return endpointParameterType.FormatActionName();
        }
        else
        {
            return endpointParameterType.Name;
        }
    }
}
