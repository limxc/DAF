using System;
using System.Reflection;

namespace DAF.Core.Extensions
{
    public static class ViewModelLocatorExtension
    {
        /// <summary>
        ///     默认定位器
        /// </summary>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public static Type PrismVmLocator(this Type viewType)
        {
            var viewName = viewType.FullName;
            viewName = viewName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
            var viewModelName = $"{viewName}{suffix}, {viewAssemblyName}";
            return Type.GetType(viewModelName);
        }

        /// <summary>
        ///     相同Namespace下
        /// </summary>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public static Type SameNsVmLocator(this Type viewType)
        {
            var viewName = viewType.FullName;
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
            var viewModelName = $"{viewName}{suffix}, {viewAssemblyName}";
            return Type.GetType(viewModelName);
        }

        /// <summary>
        ///     外部dll(如xxx.dll对应的xxx.Core.dll)
        /// </summary>
        /// <param name="viewType"></param>
        /// <param name="asmSuffix"></param>
        /// <returns></returns>
        public static Type AsmCoreVmLocator(this Type viewType, string asmSuffix = "Core")
        {
            var viewName = viewType.FullName;
            viewName = viewName.Replace(".Views.", $".{asmSuffix}.ViewModels.");
            var assemblyName = viewType.Assembly.GetName().Name;
            var viewModelAssemblyName = viewType.GetTypeInfo().Assembly.FullName
                .Replace(assemblyName, $"{assemblyName}.{asmSuffix}");
            var suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
            var viewModelName = $"{viewName}{suffix}, {viewModelAssemblyName}";

            return Type.GetType(viewModelName);
        }
    }
}