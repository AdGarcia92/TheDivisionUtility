using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TheDivisionUtility.TheDivision.Gear.Contracts.Enums;

namespace TheDivisionUtility.TheDivision.Gear.Module.Converters
{
    public class GearTypeToAvailableMinorAttributesConverter : IValueConverter
    {
        private static readonly List<Attributes> BodyArmorAttributes = new List<Attributes>()
        {
            Attributes.KillXP,
            Attributes.AmmoCapacity
        };

        private static readonly List<Attributes> MaskAttributes = new List<Attributes>()
        {
            Attributes.DamageVsElites,
            Attributes.BurnResistance,
            Attributes.DisorientResistance,
            Attributes.BlindResistance,
            Attributes.KillXP
        };

        private static readonly List<Attributes> KneepadsAttributes = new List<Attributes>()
        {
            Attributes.DamageVsElites,
            Attributes.ShockResistance,
            Attributes.BurnResistance,
            Attributes.DisorientResistance,
            Attributes.BlindResistance,
            Attributes.DisruptResitance,
            Attributes.BleedResistance,
            Attributes.KillXP
        };

        private static readonly List<Attributes> BackpackAttributes = new List<Attributes>()
        {
            Attributes.BurnResistance,
            Attributes.DisruptResitance,
            Attributes.BleedResistance,
            Attributes.AmmoCapacity
        };

        private static readonly List<Attributes> GlovesAttributes = new List<Attributes>()
        {
        };

        private static readonly List<Attributes> HolsterAttributes = new List<Attributes>()
        {
        };
        
        private static readonly Dictionary<GearTypes, List<Attributes>> AttributeDictionary = new Dictionary<GearTypes, List<Attributes>>()
        {
            {GearTypes.Chest, BodyArmorAttributes },
            {GearTypes.Mask, MaskAttributes },
            {GearTypes.Kneepads, KneepadsAttributes },
            {GearTypes.Backpack, BackpackAttributes },
            {GearTypes.Gloves, GlovesAttributes },
            {GearTypes.Holster, HolsterAttributes }
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<Attributes> list;
            AttributeDictionary.TryGetValue((GearTypes)value, out list);

            return list;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
