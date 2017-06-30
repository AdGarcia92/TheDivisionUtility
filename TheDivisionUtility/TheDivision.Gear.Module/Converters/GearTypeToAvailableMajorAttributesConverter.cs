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
    public class GearTypeToAvailableMajorAttributesConverter : IValueConverter
    {
        private static readonly List<Attributes> BodyArmorAttributes = new List<Attributes>()
        {
            Attributes.EnemyArmorDamage,
            Attributes.Health,
            Attributes.HealthOnKill,
            Attributes.ExoticDamageResilience,
            Attributes.AllResistance,
            Attributes.SkillHaste,
            Attributes.ProptectionVsElites,
            Attributes.KillXP,
            Attributes.AmmoCapacity
        };

        private static readonly List<Attributes> MaskAttributes = new List<Attributes>()
        {
            Attributes.CritChance,
            Attributes.EnemyArmorDamage,
            Attributes.Health,
            Attributes.HealthOnKill,
            Attributes.ExoticDamageResilience,
            Attributes.AllResistance,
            Attributes.SkillPower,
            Attributes.ProptectionVsElites,
            Attributes.DamageVsElites,
            Attributes.BurnResistance,
            Attributes.DisorientResistance,
            Attributes.BlindResistance,
            Attributes.KillXP
        };

        private static readonly List<Attributes> KneepadsAttributes = new List<Attributes>()
        {
            Attributes.CritDamage,
            Attributes.EnemyArmorDamage,
            Attributes.Health,
            Attributes.ExoticDamageResilience,
            Attributes.AllResistance,
            Attributes.SkillPower,
            Attributes.ProptectionVsElites,
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
            Attributes.CritDamage,
            Attributes.Health,
            Attributes.SkillPower,
            Attributes.WeaponStability,
            Attributes.SignatureResourceGain,
            Attributes.BurnResistance,
            Attributes.DisruptResitance,
            Attributes.BleedResistance,
            Attributes.AmmoCapacity
        };

        private static readonly List<Attributes> GlovesAttributes = new List<Attributes>()
        {
            Attributes.CritChance,
            Attributes.CritDamage,
            Attributes.EnemyArmorDamage,
            Attributes.SMGDamage,
            Attributes.AssaultRifleDamage,
            Attributes.ShotgunDamage,
            Attributes.LMGDamage,
            Attributes.PistolDamage,
            Attributes.MarksmanDamage,
            Attributes.HealthOnKill,
            Attributes.SkillHaste
        };

        private static readonly List<Attributes> HolsterAttributes = new List<Attributes>()
        {
            Attributes.CritChance,
            Attributes.Health,
            Attributes.SkillHaste,
            Attributes.ReloadSpeed,
            Attributes.ProptectionVsElites
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
