using Prism.Events;
using TheDivisionUtility.TheDivision.Gear.Contracts.Enums;

namespace TheDivisionUtility.TheDivision.Gear.Contracts.Events
{
    public class SelectedGearChangedEvent : PubSubEvent<GearTypes>
    {
    }
}
