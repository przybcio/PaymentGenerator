using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace PaymentsGenerator.ViewModelCommandsAndBehaviors
{
    public static class SelectionChangedComboBoxBehavior
    {
        public static DependencyProperty SelectionChangedCommandProperty = DependencyProperty.RegisterAttached("SelectionChanged",
                    typeof(ICommand),
                    typeof(SelectionChangedComboBoxBehavior),
                    new FrameworkPropertyMetadata(null, new PropertyChangedCallback(SelectionChangedComboBoxBehavior.SelectionChanged)));

        public static void SetSelectionChanged(DependencyObject target, ICommand value)
        {
            target.SetValue(SelectionChangedComboBoxBehavior.SelectionChangedCommandProperty, value);
        }

        public static ICommand GetSelectionChanged(DependencyObject target)
        {
            return (ICommand)target.GetValue(SelectionChangedCommandProperty);
        }


        private static void SelectionChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ComboBox element = target as ComboBox;
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
            ICommand command = (ICommand)element.GetValue(SelectionChangedComboBoxBehavior.SelectionChangedCommandProperty);
            command.Execute(e.AddedItems[0].ToString());
        }
    }
}
