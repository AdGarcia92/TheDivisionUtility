using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using TheDivisionUtility.TheDivision.Gear.Contracts.Enums;
using TheDivisionUtility.TheDivision.Gear.Contracts.ValueObjects;

namespace TheDivisionUtility.TheDivision.Gear.Module.Converters
{
    class AttributeToPercentileConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || (double) values[0] == 205)
            {
                return string.Empty;
            }

            var percentile = ((double)values[0] - 1114) / 158;
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
