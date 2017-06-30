using System.Collections.Generic;
using PostSharp.Patterns.Model;
using TheDivisionUtility.TheDivision.Gear.Contracts.Enums;
using SQLite;

namespace TheDivisionUtility.TheDivision.Gear.Contracts.ValueObjects
{   [NotifyPropertyChanged]
    public class GearPiece
    {
        public GearPiece()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public GearSetTypes GearSet { get; set; }

        public GearTypes GearType { get; set; }

        public double FirearmAttribute { get; set; }

        public double StaminaAttribute { get; set; }

        public double ElectronicAttribute { get; set; }

        public double Armor { get; set; }

        public double? CritChance { get; set; }

        public double? CritDamage { get; set; }

        public double? EnemyArmorDamage { get; set; }

        public double? SMGDamage { get; set; }

        public double? AssaultRifleDamage { get; set; }

        public double? ShotgunDamage { get; set; }

        public double? LMGDamage { get; set; }

        public double? PistolDamage { get; set; }

        public double? MarksmanDamage { get; set; }

        public double? Health { get; set; }

        public double? HealthOnKill { get; set; }

        public double? ExoticDamageResilience { get; set; }

        public double? AllResistance { get; set; }

        public double? SkillHaste { get; set; }

        public double? SkillPower { get; set; }

        public double? WeaponStability { get; set; }

        public double? ReloadSpeed { get; set; }

        public double? SignatureResourceGain { get; set; }

        public double? ProptectionVsElites { get; set; }

        public double? DamageVsElites { get; set; }

        public double? ShockResistance { get; set; }

        public double? BurnResistance { get; set; }

        public double? DisorientResistance { get; set; }

        public double? BlindResistance { get; set; }

        public double? DisruptResitance { get; set; }

        public double? BleedResistance { get; set; }

        public double? KillXP { get; set; }

        public double? AmmoCapacity { get; set; }
    }
}
