using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using TheDivisionUtility.TheDivision.Gear.Contracts.Enums;
using TheDivisionUtility.TheDivision.Gear.Contracts.ValueObjects;

namespace TheDivisionUtility.TheDivision.Gear.Module.Converters
{
    public class ArmorToPercentileConverter : IMultiValueConverter
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

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || (GearTypes)values[1] == GearTypes.None) return string.Empty;

            Tuple<int, int> boundaryValues;
            ArmorBoundaries.TryGetValue((GearTypes)values[1], out boundaryValues);

            var divisior = boundaryValues.Item2 - boundaryValues.Item1;

            ////Adjuster = "851" Divisor = "149"

            if (divisior != 299)
            {
            }

            var percentile = ((double)values[0] - boundaryValues.Item1) / divisior;
            var gearType = (GearTypes)values[1];
            var rounded = Math.Round(percentile, 2);
            return $" {rounded:0%}.";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
