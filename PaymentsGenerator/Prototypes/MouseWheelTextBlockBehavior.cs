using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Input;

namespace PaymentsGenerator.ViewModelCommandsAndBehaviors
{
    public static class MouseWheelTextBlockBehavior
    {
        public static readonly DependencyProperty MouseWheelCommandProperty =
            DependencyProperty.RegisterAttached("MouseWheel", typeof(ICommand), typeof(MouseWheelTextBlockBehavior), new FrameworkPropertyMetadata(null,new PropertyChangedCallback( OnMouseWheelChanged)));
        public static ICommand GetMouseWheel(DependencyObject target)
        {
            return target.GetValue(MouseWheelCommandProperty) as ICommand;
        }

        public static void SetMouseWheel(DependencyObject target, ICommand value)
        {
            target.SetValue(MouseWheelCommandProperty, value);
        }
        private static void OnMouseWheelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBlock element = d as TextBlock;
            if (element != null)
            {
                if ((e.NewValue != null) && (e.OldValue == null))
                    element.MouseWheel += new MouseWheelEventHandler(element_MouseWheel);
                else if ((e.NewValue == null) && (e.OldValue != null))
                    element.MouseWheel -= element_MouseWheel;
            }
        }

        static void element_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            UIElement element = (UIElement)sender;
            ICommand command = (ICommand)element.GetValue(MouseWheelCommandProperty);
            command.Execute(e.Timestamp.ToString());
        }
    }
}
    
