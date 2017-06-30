using DevExpress.Xpf.Editors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TheDivisionUtility.Controls
{
    public class DivisionEditBox : TextEdit
    {
        public string IconData
        {
            get { return GetValue(IconDataProperty) as string; }
            set { SetValue(IconDataProperty, value); }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            UpdateVisualState(true);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            UpdateVisualState(true);
        }

        static DivisionEditBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DivisionEditBox),
                new FrameworkPropertyMetadata(typeof(DivisionEditBox)));
        }

        public static readonly DependencyProperty IconDataProperty =
          DependencyProperty.Register("IconData", typeof(string), typeof(DivisionEditBox));

        private void UpdateVisualState(bool updateTransitions)
        {
            if (IsFocused)
            {
                VisualStateManager.GoToState(this, "Focused", updateTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Normal", updateTransitions);
            }
        }
    }
}
