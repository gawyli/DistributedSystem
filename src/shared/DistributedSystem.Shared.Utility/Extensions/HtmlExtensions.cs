namespace DistributedSystem.Shared.Utility.Extensions;
public static class HtmlExtensions
{
    public static string PageAnchorId(this Type pageType)
    {
        return $"{pageType.Name}Link";
    }

    public static string PageAnchorId(this Type pageType, object uniqueIdentifier)
    {
        return $"{pageType.Name}Link_{uniqueIdentifier}";
    }

    public static string ModelValueId(this string modelPropertyName)
    {
        return modelPropertyName.Replace(".", "_");
    }

    public static string RadioButtonId<T>(this string commandPropertyName, T enumValue)
        where T : Enum
    {
        return commandPropertyName.RadioButtonId(enumValue.ToString());
    }

    public static string RadioButtonId(this string commandPropertyName, string enumValue)
    {
        return $"{commandPropertyName.Replace(".", "_")}_{enumValue}";
    }

    public static string CheckBoxId<T>(this string commandPropertyName, T enumValue)
        where T : Enum
    {
        return commandPropertyName.CheckBoxId(enumValue.ToString());
    }

    public static string CheckBoxId(this string commandPropertyName, string enumValue)
    {
        return $"{commandPropertyName.Replace(".", "_")}_{enumValue}";
    }

    public static string SubmitButtonId(this Type actionType)
    {
        return $"{actionType.FormatActionName()}Submit";
    }

    public static string TableId(this Type entityType)
    {
        return $"{entityType.Name}Table";
    }

    public static string ListId(this Type entityType)
    {
        return $"{entityType.Name}List";
    }
}