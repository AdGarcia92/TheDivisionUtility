using System;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid;
using TheDivisionUtility.TheDivision.Gear.Contracts.ValueObjects;

namespace TheDivisionUtility.TheDivision.Gear.Module.Converters
{
    public class MouseButtonEventArgsToGridRowConverter : EventArgsConverterBase<EventArgs>
    {
        protected override object Convert(object sender, EventArgs e)
        {
            MouseButtonEventArgs args = (MouseButtonEventArgs)e;
            TableView view = LayoutHelper.FindParentObject<TableView>(args.OriginalSource as DependencyObject);
            int rowHandle = view.GetRowHandleByMouseEventArgs(args);
            return view.Grid.GetRow(rowHandle) as GearPiece;
        }
    }

}
