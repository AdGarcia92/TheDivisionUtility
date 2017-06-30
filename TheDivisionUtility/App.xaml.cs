using System.Windows;
using TheDivisionUtility.TheDivision.Gear.Module.DependancyResolution;

namespace TheDivisionUtility
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
       private StructureMapBootstrapper _bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _bootstrapper = new DefaultStructureMapBootstrapper();
            _bootstrapper.Run();

            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
    }
}
