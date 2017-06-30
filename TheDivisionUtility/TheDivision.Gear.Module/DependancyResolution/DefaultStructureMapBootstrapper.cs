using System;
using System.Diagnostics;
using System.Windows;
using Prism.Mvvm;
using Prism.Regions;
using TheDivisionUtility.TheDivision.Gear.Module.Views;
using Prism.StructureMap;
using StructureMap;

namespace TheDivisionUtility.TheDivision.Gear.Module.DependancyResolution
{
    internal class DefaultStructureMapBootstrapper : StructureMapBootstrapper
    {
        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {           
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();

            return mappings;
        }

        protected override void ConfigureServiceLocator()
        {
            throw new NotImplementedException();
        }


        protected override IContainer CreateContainer()
        {
            var container = IoC.Initialize();

            return container;
        }

        protected override void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewModelFactory(
                type =>
                {
                    try
                    {
                        return this.Container.GetInstance(type);
                    }
                    catch (Exception e)
                    {
                        var traceSwitch = new TraceSwitch("MySwitch", string.Empty);
                        if (traceSwitch.TraceError)
                        {
                        }
                    }

                    return null;
                });
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (MainWindow)Shell;
            Application.Current.MainWindow.Show();
        }
    }
}