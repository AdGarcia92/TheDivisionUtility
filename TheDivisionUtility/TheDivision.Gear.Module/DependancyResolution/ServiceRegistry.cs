using Prism.Events;
using StructureMap;

namespace TheDivisionUtility.TheDivision.Gear.Module.DependancyResolution
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            For<IEventAggregator>().Singleton().Use<EventAggregator>();
        }
    }
}
