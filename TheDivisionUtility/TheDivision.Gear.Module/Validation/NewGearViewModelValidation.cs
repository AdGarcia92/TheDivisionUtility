using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TheDivisionUtility.TheDivision.Gear.Contracts.Enums;
using TheDivisionUtility.TheDivision.Gear.Module.ViewModels;

namespace TheDivisionUtility.TheDivision.Gear.Module.Validation
{
    internal class NewGearViewModelValidation : ViewModelPropertyValidatorBase<NewGearViewModel>
    {
        private static readonly Dictionary<GearTypes, Tuple<int, int>> ArmorBoundaries = new Dictionary<GearTypes, Tuple<int, int>>()
        {
            {GearTypes.Chest, new Tuple<int, int>(1704, 2003) },
            {GearTypes.Mask, new Tuple<int, int>(852, 1001) },
            {GearTypes.Kneepads, new Tuple<int, int>(1419, 1668)},
            {GearTypes.Backpack, new Tuple<int, int>(1135, 1334) },
            {GearTypes.Gloves, new Tuple<int, int>(852, 1001) },
            {GearTypes.Holster, new Tuple<int, int>(852, 1001) }
        };

        protected override void ApplyRule()
        {
            SetRule(
                new Expression<Func<NewGearViewModel, object>>[]
                {
                    instance => instance.NewGear.Armor
                },
                instance =>
                {
                    Tuple<int, int> boundaries;
                    ArmorBoundaries.TryGetValue(instance.NewGear.GearType, out boundaries);

                    return (instance.NewGear.Armor > boundaries.Item2 || instance.NewGear.Armor < boundaries.Item1);
                },
                "Armor out of range");
            SetRule(
               new Expression<Func<NewGearViewModel, object>>[]
               {
                    instance => instance.NewGear
               },
               instance =>
               {
                   Tuple<int, int> boundaries;
                   ArmorBoundaries.TryGetValue(instance.NewGear.GearType, out boundaries);

                   return (instance.NewGear.Armor > boundaries.Item2 || instance.NewGear.Armor < boundaries.Item1);
               },
               "Armor out of range");
        }

        private void SetRule(
            IEnumerable<Expression<Func<NewGearViewModel, object>>> properties,
            Func<NewGearViewModel, bool> isWrong,
            string message)
        {
            foreach (var property in properties)
            {
                Rule(property, isWrong, message);
            }
        }
    }
}
