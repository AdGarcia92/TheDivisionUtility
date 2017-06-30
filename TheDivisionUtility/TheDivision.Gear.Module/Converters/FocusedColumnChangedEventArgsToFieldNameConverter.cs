using System;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Grid;

namespace TheDivisionUtility.TheDivision.Gear.Module.Converters
{
    public class FocusedColumnChangedEventArgsToFieldNameConverter : EventArgsConverterBase<EventArgs>
    {
        protected override object Convert(object sender, EventArgs e)
        {
            CurrentColumnChangedEventArgs args = (CurrentColumnChangedEventArgs)e;
            if (args == null || args.NewColumn == null)
            {
                return String.Empty;
            }
            return args.NewColumn.FieldName;
        }
    }
}

