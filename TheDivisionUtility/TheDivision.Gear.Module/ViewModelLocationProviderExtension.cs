using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;

namespace TheDivisionUtility.TheDivision.Gear.Module
{
    internal static class ViewModelLocationProviderExtension
        {
            [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1311:StaticReadonlyFieldsMustBeginWithUpperCaseLetter",
                Justification = "Reviewed. Suppression is OK here.")]
            private static readonly string _defaultViewModelNameSpaceConvention = "ViewModels";

            [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1311:StaticReadonlyFieldsMustBeginWithUpperCaseLetter",
                Justification = "Reviewed. Suppression is OK here.")]
            private static readonly string _defaultViewModelTypeConventionName = "ViewModel";

            [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1311:StaticReadonlyFieldsMustBeginWithUpperCaseLetter",
                Justification = "Reviewed. Suppression is OK here.")]

            // ReSharper disable once InconsistentNaming
            private static readonly Func<Type, Type> _defaultViewModelTypeToViewTypeResolverProvider;

            [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1311:StaticReadonlyFieldsMustBeginWithUpperCaseLetter",
                Justification = "Reviewed. Suppression is OK here.")]

            // ReSharper disable once InconsistentNaming
            private static readonly Dictionary<string, Func<object>> _factories;

            static ViewModelLocationProviderExtension()
            {
                _factories = new Dictionary<string, Func<object>>();
                _defaultViewModelTypeToViewTypeResolverProvider = viewModelType =>
                {
                    var viewName =
                        viewModelType.FullName.Replace(
                            string.Format(".{0}.", _defaultViewModelNameSpaceConvention),
                            ".Views.");
                    var viewWithView = viewName.Replace(_defaultViewModelTypeConventionName, string.Empty);
                    var viewWithOutView = viewName.Replace("Model", string.Empty);
                    return Type.GetType(viewWithView)
                           ?? Type.GetType(viewWithOutView)
                           ?? viewModelType.Assembly.GetType(viewWithView)
                           ?? viewModelType.Assembly.GetType(viewWithOutView);
                };
            }

            public static Type GetViewModelTypeToViewType(Type viewModel)
            {
                var view = GetView(viewModel);
                if (view == null)
                {
                    var viewType = _defaultViewModelTypeToViewTypeResolverProvider(viewModel);
                    if (viewType == null)
                    {
                        throw new ModuleTypeLoaderNotFoundException(viewModel.FullName);
                    }

                    return viewType;
                }

                return view.GetType();
            }

            public static bool MatchesViewModelNamingConvention(Type viewModel)
            {
                return viewModel.FullName.Contains(_defaultViewModelNameSpaceConvention)
                       && viewModel.FullName.EndsWith(_defaultViewModelTypeConventionName);
            }

            /// <summary>
            /// Mapping of view models base on view type (or instance) goes here
            /// </summary>
            /// <param name="viewModel"></param>
            /// <returns></returns>
            private static object GetView(object viewModel)
            {
                var viewModelKey = viewModel.GetType().ToString();
                return _factories.ContainsKey(viewModelKey) ? _factories[viewModelKey]() : null;
            }
        } 
}
