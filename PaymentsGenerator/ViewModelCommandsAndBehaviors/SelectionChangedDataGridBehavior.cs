using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace PaymentsGenerator.ViewModelCommandsAndBehaviors
{
    public class SelectionChangedDataGridBehavior
    {
        public static DependencyProperty SelectionChangedCommandProperty = DependencyProperty.RegisterAttached("SelectionChanged",
                   typeof(ICommand),
                   typeof(SelectionChangedDataGridBehavior),
                   new FrameworkPropertyMetadata(null, new PropertyChangedCallback(SelectionChangedDataGridBehavior.SelectionChanged)));

        public static void SetSelectionChanged(DependencyObject target, ICommand value)
        {
            target.SetValue(SelectionChangedCommandProperty, value);
        }

        public static ICommand GetSelectionChanged(DependencyObject target)
        {
            return (ICommand)target.GetValue(SelectionChangedCommandProperty);
        }


        private static void SelectionChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            DataGrid element = target as DataGrid;
            if (element != null)
            {
                if ((e.NewValue != null) && (e.OldValue == null))
                    element.SelectionChanged += new SelectionChangedEventHandler(element_SelectionChanged);
                else if ((e.NewValue == null) && (e.OldValue != null))
                    element.SelectionChanged -= element_SelectionChanged;
            }
        }

        static void element_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UIElement element = (UIElement)sender;
            ICommand command = (ICommand)element.GetValue(SelectionChangedCommandProperty);
            command.Execute(((DataGrid)sender).SelectedItems);
        }
    }
}
