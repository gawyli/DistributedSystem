using DistributedSystem.Shared.Web.Pages;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Web;
public class PageModelInitializer
{
    private const string TargetMethodName = "Initialize";
    private const string TargetMethodNameAsyncVariant = TargetMethodName + "Async";

    private readonly IModelBinderFactory _modelBinderFactory;
    private readonly IModelMetadataProvider _modelMetadataProvider;

    public PageModelInitializer(IModelBinderFactory modelBinderFactory, IModelMetadataProvider modelMetadataProvider)
    {
        _modelBinderFactory = modelBinderFactory;
        _modelMetadataProvider = modelMetadataProvider;
    }

    public async Task InitializeAsync(PageModel pageModel)
    {
        await InitializeAsync(pageModel, _modelMetadataProvider, _modelBinderFactory);
    }

    private static async Task InitializeAsync(PageModel pageModel, IModelMetadataProvider modelMetadataProvider,
        IModelBinderFactory modelBinderFactory)
    {
        var valueProvider = await CompositeValueProvider.CreateAsync(pageModel.PageContext,
            pageModel.PageContext.ValueProviderFactories);

        // Go through the type hierarchy until we:
        // - find more than 1 things that look like initializers - throw an exception
        // - find 1 thing that looks like an initializer - call it and return
        for (var pageModelType = pageModel.GetType();
             pageModelType is not null && pageModelType != typeof(BasePageModel);
             pageModelType = pageModelType.BaseType)
        {
            var targetMethods = pageModelType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => m.Name is TargetMethodName or TargetMethodNameAsyncVariant).ToList();

            switch (targetMethods.Count)
            {
                case 0:
                    continue;
                case 1:
                    {
                        await InvokeMethod(targetMethods[0], pageModel, modelMetadataProvider, modelBinderFactory,
                            valueProvider);
                        return;
                    }
                default:
                    throw new Exception(
                        $"Conflicting initialization methods found: [{string.Join(", ", targetMethods.Select(m => m.ToString()))}]");
            }
        }
    }

    private static async Task InvokeMethod(MethodBase method, PageModel pageModel,
        IModelMetadataProvider modelMetadataProvider, IModelBinderFactory modelBinderFactory,
        IValueProvider valueProvider)
    {
        ParameterInfo[] parameterInfos = method.GetParameters();
        object[] args = await CreateMethodArgs(pageModel, valueProvider,
            modelMetadataProvider, modelBinderFactory, parameterInfos);

        object? invocationResult = method.Invoke(pageModel, args);
        if (invocationResult is Task task)
        {
            await task;
        }
    }

    private static async Task<object[]> CreateMethodArgs(PageModel pageModel,
        IValueProvider valueProvider, IModelMetadataProvider modelMetadataProvider,
        IModelBinderFactory modelBinderFactory, ParameterInfo[] parameterInfos)
    {
        object[] args = new object[parameterInfos.Length];

        for (int paramIndex = 0; paramIndex < parameterInfos.Length; paramIndex++)
        {
            var parameterInfo = parameterInfos[paramIndex];
            object arg;

            if (paramIndex == parameterInfos.Length - 1 && parameterInfo.ParameterType == typeof(CancellationToken))
            {
                arg = pageModel.HttpContext.RequestAborted;
            }
            else if (parameterInfo.ParameterType.IsPrimitive)
            {
                arg = GetPrimitive(valueProvider, parameterInfo) ??
                      // Activator.CreateInstance is guaranteed to return the default value for a primitive,
                      // e.g. false for bool, 0.0 for double. In this case it's ok to '!'.
                      Activator.CreateInstance(parameterInfo.ParameterType)!;
            }
            else if (parameterInfo.ParameterType == typeof(string))
            {
                arg = GetString(valueProvider, parameterInfo) ?? default!;
            }
            else
            {
                arg = await NewByBinding(pageModel.PageContext, valueProvider, modelMetadataProvider,
                    modelBinderFactory, parameterInfo);
            }

            args[paramIndex] = arg;
        }

        return args;
    }

    private static string? GetString(IValueProvider valueProvider, ParameterInfo parameterInfo)
    {
        if (parameterInfo.Name is null)
        {
            return null;
        }

        return valueProvider.GetValue(parameterInfo.Name).Values.FirstOrDefault(sv => sv is not null);
    }

    private static object? GetPrimitive(IValueProvider valueProvider, ParameterInfo parameterInfo)
    {
        if (parameterInfo.Name is null)
        {
            return null;
        }

        var typeConverter = TypeDescriptor.GetConverter(parameterInfo.ParameterType);

        return valueProvider.GetValue(parameterInfo.Name).Values
            .Where(sv => sv is not null).Cast<string>()
            .Select(sv =>
            {
                try
                {
                    return typeConverter.ConvertFromString(sv);
                }
                catch (NotSupportedException)
                {
                    return null;
                }
            }).FirstOrDefault(v => v is not null);
    }

    private static async Task<object> NewByBinding(ActionContext actionContext,
        IValueProvider valueProvider, IModelMetadataProvider modelMetadataProvider,
        IModelBinderFactory modelBinderFactory, ParameterInfo parameterInfo)
    {
        var modelMetadata = modelMetadataProvider.GetMetadataForType(parameterInfo.ParameterType);

        var bindingResult =
            await BindAsync(actionContext, valueProvider, modelMetadata, modelBinderFactory);

        if (!bindingResult.IsModelSet || bindingResult.Model is null)
        {
            throw new($"Could not bind to instance of {parameterInfo.ParameterType} for parameter named \"{parameterInfo.Name}\"");
        }

        return bindingResult.Model;
    }

    private static async Task<ModelBindingResult> BindAsync(ActionContext pageContext,
        IValueProvider valueProvider, ModelMetadata modelMetadata, IModelBinderFactory modelBinderFactory)
    {
        var factoryContext = NewModelBinderFactoryContext(modelMetadata);
        var binder = modelBinderFactory.CreateBinder(factoryContext);

        var bindingContext = DefaultModelBindingContext.CreateBindingContext(
            pageContext,
            valueProvider,
            modelMetadata,
            null,
            string.Empty);

        await binder.BindModelAsync(bindingContext);

        return bindingContext.Result;
    }

    private static ModelBinderFactoryContext NewModelBinderFactoryContext(ModelMetadata modelMetadata)
    {
        return new ModelBinderFactoryContext
        {
            Metadata = modelMetadata,
            BindingInfo = new BindingInfo
            {
                BinderModelName = modelMetadata.BinderModelName,
                BinderType = modelMetadata.BinderType,
                BindingSource = modelMetadata.BindingSource,
                PropertyFilterProvider = modelMetadata.PropertyFilterProvider
            },
            CacheToken = modelMetadata
        };
    }
}