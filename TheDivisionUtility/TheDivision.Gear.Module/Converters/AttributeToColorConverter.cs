using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using TheDivisionUtility.TheDivision.Gear.Contracts.Enums;
using TheDivisionUtility.TheDivision.Gear.Contracts.ValueObjects;

namespace TheDivisionUtility.TheDivision.Gear.Module.Converters
{
    public class AttributeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (double)value == 205) return System.Drawing.ColorTranslator.FromHtml("#E50000");

            var percentile = ((double)value - 1114) / 158;

            if (percentile >= .9)
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#63BE7B"));
            }
            if (percentile >= .8 && percentile < .9)
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#86C97E"));
            }
            if (percentile >= .7 && percentile < .8)
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#A9D27F"));
            }
            if (percentile >= .6 && percentile < .7)
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#CCDD82"));
            }
            if (percentile >= .5 && percentile < .6)
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#EEE683"));
            }
            if (percentile >= .4 && percentile < .5)
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDC81"));
            }
            if (percentile >= .3 && percentile < .4)
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FCBF7B"));
            }
            if (percentile >= .2 && percentile < .3)
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FBA276"));
            }
            if (percentile >= .1 && percentile < .2)
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#F98570"));
            }
            if (percentile >= 0 && percentile < .1)
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#F8696B"));
            }

            return System.Drawing.ColorTranslator.FromHtml("#E50000");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
