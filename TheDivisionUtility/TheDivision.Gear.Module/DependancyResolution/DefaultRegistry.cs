using System;
using StructureMap;
using TheDivisionUtility.TheDivision.Gear.Module.Validation;
using TheDivisionUtility.TheDivision.Gear.Module.ViewModels;
using TheDivisionUtility.TheDivision.Gear.Module.Views;

namespace TheDivisionUtility.TheDivision.Gear.Module.DependancyResolution
{
    internal class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            this.Scan(
                x =>
                {     
                    x.WithDefaultConventions();
                    x.TheCallingAssembly();
                    x.AssembliesFromApplicationBaseDirectory(assembly => assembly.FullName.Contains("TheDivision"));
                    x.LookForRegistries();
                });

            For<MainWindow>().Use<MainWindow>();
            For<IContainer>().Use<Container>();

            For<IViewModelPropertyValidator<NewGearViewModel>>().Use<NewGearViewModelValidation>();

            For<IPropertyValidator<NewGearViewModel>>()
                .Use<PropertyValidator<NewGearViewModel>>();

            For<MainWindowViewModel>()
                .Use<MainWindowViewModel>()
                .Ctor<Lazy<NewGearViewModel>>()
                .Is(x => new Lazy<NewGearViewModel>(() => new NewGearViewModel(x.GetInstance<IPropertyValidator<NewGearViewModel>>())));

            For<NewGearViewModel>()
                .Use<NewGearViewModel>()
                .Ctor<IPropertyValidator<NewGearViewModel>>()
                .Is<PropertyValidator<NewGearViewModel>>();
        }
    }
}
