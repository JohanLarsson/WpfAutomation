namespace WpfAutomation
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public static class Automation
    {
        public static void SimulateClick(this CheckBox checkBox)
        {
            checkBox.RaiseMouseButton(UIElement.PreviewMouseLeftButtonDownEvent, MouseButton.Left, 1)
                    .RaiseMouseButton(Mouse.PreviewMouseDownEvent, MouseButton.Left, 1)
                    .RaiseKeyboardFocusEvent(Keyboard.PreviewGotKeyboardFocusEvent)// old: System.Windows.Controls.TextBox new: System.Windows.Controls.CheckBox Content: IsChecked:False
                // .SetProp(UIElement.IsKeyboardFocusWithinProperty, True)
                // .SetProp(UIElement.IsKeyboardFocusedProperty, True)
                    .RaiseRoutedEvent(FocusManager.GotFocusEvent)
                    .RaiseKeyboardFocusEvent(Keyboard.GotKeyboardFocusEvent) // old: System.Windows.Controls.TextBox new: System.Windows.Controls.CheckBox Content: IsChecked:False
                // .SetProp(UIElement.IsMouseCaptureWithinProperty, True)
                // .SetProp(UIElement.IsMouseCapturedProperty, True)
                    .RaiseMouseEvent(Mouse.GotMouseCaptureEvent)
                // .SetProp(UIElement.IsMouseDirectlyOverProperty, True)
                    .RaiseMouseButton(UIElement.PreviewMouseLeftButtonUpEvent, MouseButton.Left, 1)
                    .RaiseMouseButton(Mouse.PreviewMouseUpEvent, MouseButton.Left, 1)
                // .SetProp(UIElement.IsMouseCaptureWithinProperty, False)
                // .SetProp(UIElement.IsMouseCapturedProperty, False)
                    .RaiseMouseEvent(Mouse.LostMouseCaptureEvent)
                // .SetProp(UIElement.IsMouseDirectlyOverProperty, False)
                    .Toggle()
                    .RaiseRoutedEvent(ButtonBase.ClickEvent);
        }

        public static void SimulateKey(this CheckBox checkBox, Key key)
        {
            checkBox.RaiseKeyboardEvent(Keyboard.PreviewKeyDownEvent, Key.Space)
                // .SetProp(UIElement.IsMouseCaptureWithinProperty, True)
                // .SetProp(UIElement.IsMouseCapturedProperty, True)
                    .RaiseMouseEvent(Mouse.GotMouseCaptureEvent)
                // .SetProp(UIElement.IsMouseDirectlyOverProperty, True)
                    .RaiseKeyboardEvent(Keyboard.PreviewKeyUpEvent, Key.Space)
                // .SetProp(UIElement.IsMouseCaptureWithinProperty, False)
                // .SetProp(UIElement.IsMouseCapturedProperty, False)
                    .RaiseMouseEvent(Mouse.LostMouseCaptureEvent)
                // .SetProp(UIElement.IsMouseDirectlyOverProperty, False)
                    .Toggle()
                    .RaiseRoutedEvent(ButtonBase.ClickEvent);
        }

        public static void SimulateKey(this TextBoxBase textBox, Key key)
        {
            textBox.RaiseKeyboardEvent(UIElement.PreviewKeyDownEvent, key)
                   .RaiseKeyboardEvent(UIElement.KeyDownEvent, key)
                   .RaiseKeyboardEvent(UIElement.KeyDownEvent, key)
                   .SetKeyboardFocus()
                   .RaiseKeyboardEvent(UIElement.PreviewKeyUpEvent, key)
                   .RaiseKeyboardEvent(UIElement.KeyUpEvent, key);
            textBox.AppendText(key.ToString());
        }

        public static ToggleButton Toggle(this ToggleButton toggleButton)
        {
            if (toggleButton.IsChecked == null)
            {
                toggleButton.SetProp(ToggleButton.IsCheckedProperty, false);
            }
            else
            {
                toggleButton.SetProp(ToggleButton.IsCheckedProperty, !toggleButton.IsChecked);
            }
            if (toggleButton.IsChecked == true)
            {
                toggleButton.RaiseEvent(new RoutedEventArgs(ToggleButton.CheckedEvent));
            }
            else
            {
                toggleButton.RaiseEvent(new RoutedEventArgs(ToggleButton.UncheckedEvent));
            }
            return toggleButton;
        }

        public static T RaiseMouseButton<T>(this T e, RoutedEvent @event, MouseButton button, int clickCount)
            where T : UIElement
        {
            var arg = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, button)
            {
                RoutedEvent = @event
            };

            e.RaiseEvent(arg);
            return e;
        }

        public static T RaiseKeyboardEvent<T>(this T e, RoutedEvent @event, Key key) where T : UIElement
        {
            var arg = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key)
            {
                RoutedEvent = @event
            };
            e.RaiseEvent(arg);
            return e;
        }

        public static T RaiseKeyboardFocusEvent<T>(this T e, RoutedEvent @event) where T : UIElement
        {
            var arg = new KeyboardFocusChangedEventArgs(Keyboard.PrimaryDevice, 0, null, e)
            {
                RoutedEvent = @event
            };
            e.RaiseEvent(arg);
            return e;
        }

        public static T RaiseRoutedEvent<T>(this T e, RoutedEvent @event) where T : UIElement
        {
            var arg = new RoutedEventArgs
            {
                RoutedEvent = @event
            };
            e.RaiseEvent(arg);
            return e;
        }
       
        public static T RaiseMouseEvent<T>(this T e, RoutedEvent @event) where T : UIElement
        {
            var arg = new MouseEventArgs(Mouse.PrimaryDevice, 0)
            {
                RoutedEvent = @event
            };
            e.RaiseEvent(arg);
            return e;
        }
        
        public static T SetKeyboardFocus<T>(this T e) where T : UIElement
        {
            if (e.IsKeyboardFocused)
            {
                return e;
            }
            FocusManager.SetFocusedElement(e, Keyboard.Focus(e));
            var arg = new RoutedEventArgs
            {
                RoutedEvent = UIElement.GotKeyboardFocusEvent
            };
            e.RaiseEvent(arg);
            return e;
        }

        public static T SetProp<T>(this T e, DependencyProperty prop, object value) where T : UIElement
        {
            e.SetValue(prop, value);
            return e;
        }
    }
}
