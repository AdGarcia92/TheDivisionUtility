using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interactivity;
using DevExpress.Xpf.Editors;
using DevExpress.XtraEditors.DXErrorProvider;
using TheDivisionUtility.Controls;
using TheDivisionUtility.TheDivision.Gear.Contracts.Enums;
using TheDivisionUtility.TheDivision.Gear.Contracts.ValueObjects;
using TheDivisionUtility.TheDivision.Gear.Module.ViewModels;

namespace TheDivisionUtility.TheDivision.Gear.Module.Behaviors
{
    class GearArmorValidationBehavior : Behavior<TextEdit>
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

        protected override void OnAttached()
        {
            AssociatedObject.Validate += OnValidate;
        }

        private void OnValidate(object sender, ValidationEventArgs e)
        {
            var textEdit = sender as TextEdit;
            var vm = textEdit.DataContext as NewGearViewModel;

            if (vm?.NewGear == null)
            {
                return;
            }

            Tuple<int, int> boundaries;
            ArmorBoundaries.TryGetValue(vm.NewGear.GearType, out boundaries);

            if (boundaries == null)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace((string)e.Value) || e.Value != null)
            {
                var armor = int.Parse((string)e.Value);

                if (armor < boundaries.Item1 || armor > boundaries.Item2)
                {
                    e.IsValid = false;
                    e.ErrorType = ErrorType.Critical;
                }
            }
        }
    }
}
