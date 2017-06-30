using System.Windows;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Grid;

namespace TheDivisionUtility.TheDivision.Gear.Module.Converters
{
    public class InitNewRowDataClass
    {
        public GridControl Grid { get; set; }
        public InitNewRowEventArgs Args { get; set; }
        public TableView View { get; set; }

        public InitNewRowDataClass()
        {
            Grid = null;
            Args = null;
        }
        public InitNewRowDataClass(GridControl g, InitNewRowEventArgs r, TableView v)
        {
            Grid = g;
            Args = r;
            View = v;
        }
    }

    public class InitNewRowEventArgsConverter : EventArgsConverterBase<InitNewRowEventArgs>
    {
        protected override object Convert(object sender, InitNewRowEventArgs e)
        {
            TableView view = LayoutHelper.FindParentObject<TableView>(e.OriginalSource as DependencyObject);
            GridControl grid = LayoutHelper.FindParentObject<GridControl>(e.OriginalSource as DependencyObject);
            return new InitNewRowDataClass(grid, e, view);
        }
    }
}
