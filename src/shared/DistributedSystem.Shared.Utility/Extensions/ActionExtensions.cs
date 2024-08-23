using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Utility.Extensions;
public static class ActionExtensions
{
    public static string FormatActionName(this Type actionType)
    {
        var fullTypeName = actionType.FullName;
        if (fullTypeName == null)
        {
            return string.Empty;
        }
        else if (fullTypeName.EndsWith("+Command") || fullTypeName.EndsWith("+Query"))
        {
            // Typically, our Commands/Queries are nested classes with type names like Storm.DotNet.Core.ProjectAggregate.Queries.AllProjects+Query.
            // Some utilities (e.g. tag helpers, Swagger schema generators) prefer this tp be translated to e.g. AllProjectsQuery.
            var formattedTypeName = fullTypeName.ReplaceLastOccurrence("+", "");
            return formattedTypeName.Substring(formattedTypeName.LastIndexOf(".") + 1);
        }
        return actionType.Name;
    }
}
