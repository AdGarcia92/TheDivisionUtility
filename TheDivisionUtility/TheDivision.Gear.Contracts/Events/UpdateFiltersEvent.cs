using System.Collections.Generic;
using Prism.Events;
using TheDivisionUtility.TheDivision.Gear.Contracts.ValueObjects;

namespace TheDivisionUtility.TheDivision.Gear.Contracts.Events
{
    public class UpdateFiltersEvent : PubSubEvent<int>
    {
    }
}
