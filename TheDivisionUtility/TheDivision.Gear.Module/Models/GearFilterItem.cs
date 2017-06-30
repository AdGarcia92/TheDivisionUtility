using DevExpress.Mvvm.POCO;
////using PostSharp.Patterns.Model;
using TheDivisionUtility.TheDivision.Gear.Contracts.Enums;

namespace TheDivisionUtility.TheDivision.Gear.Module.Models
{
    public class GearFilterItem
    {
        public GearFilterItem(int count, string name, GearTypes? gearType, string path)
        {
            Name = name;
            GearType = gearType;
            Count = count;
            PathData = path;
        }

        public void Update(int entitiesCount)
        {
            this.Count = entitiesCount;
        }

        public string Name { get; set; }

        public GearTypes? GearType { get; set; }

        public int Count { get; set; }

        public string PathData { get; set; }
    }
}
