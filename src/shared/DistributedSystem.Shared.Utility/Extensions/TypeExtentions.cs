using Ardalis.GuardClauses;
using System.Reflection;

namespace DistributedSystem.Shared.Utility.Extensions;
public static class TypeExtensions
{
    #region Nullable types

    public static Type GetNullableTypeBaseType(this Type targetType)
    {
        var returnType = targetType;
        if (targetType.IsNullable())
        {
            returnType = targetType.GetGenericArguments()[0];
        }

        return (returnType);
    }

    public static bool IsNullable(this Type targetType)
    {
        return targetType.IsGenericType && (targetType.GetGenericTypeDefinition() == typeof(Nullable<>));
    }

    #endregion


    #region Constructors

    public static object NewInstance(this Type type, Type[] constructorParamTypes, object[] constructorParams)
    {
        var constructor = type.GetConstructor(constructorParamTypes);
        Guard.Against.Null(constructor);
        return constructor.Invoke(constructorParams);
    }

    public static object NewInstance(this Type type, object[] constructorParams)
    {
        return type.NewInstance(constructorParams.Select(cp => cp.GetType()).ToArray(), constructorParams);
    }

    public static object NewInstance(this Type type)
    {
        return type.NewInstance(new object[] { });
    }

    #endregion


    #region Attributes

    public static T? GetAttribute<T>(this MemberInfo memberInfo) where T : Attribute
    {
        T? attribute = null;

        var attributes = Attribute.GetCustomAttributes(memberInfo, typeof(T), true);
        if (attributes.Length > 0)
        {
            attribute = (T)attributes[0];
        }

        return attribute;
    }

    #endregion


    #region Superclasses

    public static bool InheritsFromGenericParent(this Type type, Type parentType)
    {
        if (!parentType.IsGenericType)
        {
            throw new ArgumentException($"Type {parentType.Name} is not generic", "parentType");
        }

        if ((type == null) || (type.BaseType == null))
        {
            return false;
        }
        if (type.IsGenericType && type.GetGenericTypeDefinition().IsAssignableFrom(parentType))
        {
            return true;
        }
        return type.BaseType.InheritsFromGenericParent(parentType) || type.GetInterfaces().Any(t => t.InheritsFromGenericParent(parentType));
    }

    #endregion
}