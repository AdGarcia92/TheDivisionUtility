using System;
using System.Diagnostics;
using System.Windows;
using DevExpress.Xpf.Bars.Native;
using Microsoft.Practices.ServiceLocation;
using Prism;
using Prism.Events;
using Prism.Logging;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using StructureMap;
using TheDivisionUtility.Properties;
using TheDivisionUtility.TheDivision.Gear.Module.Views;

namespace TheDivisionUtility.TheDivision.Gear.Module.DependancyResolution
{
    public abstract class StructureMapBootstrapper : Bootstrapper
    {
        private bool _useDefaultConfiguration = true;

        /// <summary>
        /// Gets the default StructureMap <see cref="IContainer"/> for the application.
        /// </summary>
        /// <value>The default <see cref="IContainer"/> instance.</value>
        public IContainer Container { get; protected set; }

        /// <summary>
        /// Run the bootstrapper process.
        /// </summary>
        /// <param name="runWithDefaultConfiguration">If <see langword="true"/>, registers default Prism Library services in the container. This is the default behavior.</param>
        public override void Run(bool runWithDefaultConfiguration)
        {            
            _useDefaultConfiguration = runWithDefaultConfiguration;

            Container = CreateContainer();

            ConfigureContainer();

            ConfigureViewModelLocator();

            Shell = CreateShell();
            if (Shell != null)
            {
                InitializeShell();
            }
        }

        /// <summary>
        /// Configures the <see cref="ViewModelLocator"/> used by Prism.
        /// </summary>
        protected override void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewModelFactory((type) => Container.GetInstance(type));
        }

        /// <summary>
        /// Registers in the StructureMap <see cref="IContainer"/> the <see cref="Type"/> of the Exceptions
        /// that are not considered root exceptions by the <see cref="ExceptionExtensions"/>.
        /// </summary>
        protected override void RegisterFrameworkExceptionTypes()
        {
            base.RegisterFrameworkExceptionTypes();

            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(StructureMapBuildPlanException));
            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(StructureMapConfigurationException));
            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(StructureMapException));
        }

        /// <summary>
        /// Creates the <see cref="Container"/> that will be used to create the default container.
        /// </summary>
        /// <returns>A new instance of <see cref="Container"/>.</returns>
        protected virtual IContainer CreateContainer()
        {
            return new Container();
        }

        /// <summary>
        /// Configures the <see cref="IContainer"/>. 
        /// May be overwritten in a derived class to add specific type mappings required by the application.
        /// </summary>
        protected virtual void ConfigureContainer()
        {
            if (_useDefaultConfiguration)
            {
                Container.RegisterTypeIfMissing<IModuleInitializer, ModuleInitializer>(true);
                Container.RegisterTypeIfMissing<IModuleManager, ModuleManager>(true);
                Container.RegisterTypeIfMissing<RegionAdapterMappings, RegionAdapterMappings>(true);
                Container.RegisterTypeIfMissing<IRegionManager, RegionManager>(true);
                Container.RegisterTypeIfMissing<IEventAggregator, EventAggregator>(true);
                Container.RegisterTypeIfMissing<IRegionViewRegistry, RegionViewRegistry>(true);
                Container.RegisterTypeIfMissing<IRegionBehaviorFactory, RegionBehaviorFactory>(true);
                Container.RegisterTypeIfMissing<IRegionNavigationJournalEntry, RegionNavigationJournalEntry>(false);
                Container.RegisterTypeIfMissing<IRegionNavigationJournal, RegionNavigationJournal>(false);
                Container.RegisterTypeIfMissing<IRegionNavigationService, RegionNavigationService>(false);
                Container.RegisterTypeIfMissing<IRegionNavigationContentLoader, RegionNavigationContentLoader>(true);
            }
        }

        protected override DependencyObject CreateShell()
        {
            try
            {
                var shell = this.Container.GetInstance<MainWindow>();
                return shell;
            }
            catch (Exception e)
            {
                var traceSwitch = new TraceSwitch("MySwitch", string.Empty);
                if (traceSwitch.TraceError)
                {
                    Trace.TraceError(e.ToString());
                }
            }

            return null;
        }

        /// <summary>
        /// Initializes the modules. May be overwritten in a derived class to use a custom Modules Catalog
        /// </summary>
        protected override void InitializeModules()
        {
            IModuleManager manager;

            try
            {
                manager = Container.GetInstance<IModuleManager>();
            }
            catch (StructureMapException ex)
            {
                if (ex.Message.Contains("IModuleCatalog"))
                {
                    throw new InvalidOperationException();
                }

                throw;
            }

            manager.Run();
        }
    }
}