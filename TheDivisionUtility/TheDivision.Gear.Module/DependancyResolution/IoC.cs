using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using StructureMap;

namespace TheDivisionUtility.TheDivision.Gear.Module.DependancyResolution
{
    internal static class IoC
    {
        public static IContainer Initialize()
        {
            IContainer container = new Container(
                c =>
                {
                    c.For<IEventAggregator>().Singleton().Use<EventAggregator>();
                    c.AddRegistry<DefaultRegistry>();
                });
           

            return container;
        }
    }
}