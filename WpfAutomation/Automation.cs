namespace WpfAutomation
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

    public static class Automation
    {
        public static void SimulateClick(this CheckBox checkBox)
        {
            checkBox.RaiseMouseButton(UIElement.PreviewMouseLeftButtonDownEvent, MouseButton.Left, 1) // value: true
                    .RaiseMouseButton(Mouse.PreviewMouseDownEvent, MouseButton.Left, 1) // value: true
                    .RaiseKeyboardFocusEvent(Keyboard.PreviewGotKeyboardFocusEvent) // old: System.Windows.Controls.Button: Clear new: System.Windows.Controls.CheckBox Content: IsChecked:True
                // .SetProp(UIElement.IsKeyboardFocusWithinProperty, True) // value: true
                // .SetProp(UIElement.IsKeyboardFocusedProperty, True) // value: true
                    .RaiseRoutedEvent(FocusManager.GotFocusEvent) // value: true
                    .RaiseKeyboardFocusEvent(Keyboard.GotKeyboardFocusEvent) // old: System.Windows.Controls.Button: Clear new: System.Windows.Controls.CheckBox Content: IsChecked:True
                // .SetProp(UIElement.IsMouseCaptureWithinProperty, True) // value: true
                // .SetProp(UIElement.IsMouseCapturedProperty, True) // value: true
                    .RaiseMouseEvent(Mouse.GotMouseCaptureEvent)
                // .SetProp(UIElement.IsMouseDirectlyOverProperty, True) // value: true
                    .RaiseMouseButton(UIElement.PreviewMouseLeftButtonUpEvent, MouseButton.Left, 1) // value: true
                    .RaiseMouseButton(Mouse.PreviewMouseUpEvent, MouseButton.Left, 1) // value: true
                // .SetProp(UIElement.IsMouseCaptureWithinProperty, False) // value: true
                // .SetProp(UIElement.IsMouseCapturedProperty, False) // value: true
                    .RaiseMouseEvent(Mouse.LostMouseCaptureEvent)
                // .SetProp(UIElement.IsMouseDirectlyOverProperty, False) // value: true
                    .Toggle() // value: false
                    .RaiseRoutedEvent(ButtonBase.ClickEvent); // value: false
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

        public static void SimulateKey(this TextBox textBox, Key key, UndoAction undoAction)
        {
            string s = textBox.Text + key;
            textBox.RaiseKeyboardEvent(Keyboard.PreviewKeyDownEvent, key) // value: 
                   .RaiseKeyboardEvent(Keyboard.KeyDownEvent, key) // value: 
                   .RaiseTextInputEvent(TextCompositionManager.PreviewTextInputEvent, s) // value:
                   .SetProp(TextBox.TextProperty, s)
                   .RaiseTextChangedEvent(TextBoxBase.TextChangedEvent, undoAction) // undoAction: Create value: a
                   .RaiseRoutedEvent(TextBoxBase.SelectionChangedEvent) // value: a
                   .RaiseKeyboardEvent(Keyboard.PreviewKeyUpEvent, key) // value: a
                   .RaiseKeyboardEvent(Keyboard.KeyUpEvent, key); // value: a
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

        public static TextBox RaiseTextInputEvent(this TextBox tb, RoutedEvent @event, string resultText)
        {
            var arg = new TextCompositionEventArgs(
                Keyboard.PrimaryDevice,
                new TextComposition(InputManager.Current, tb, resultText))
                                           {
                                               RoutedEvent = @event
                                           };
            tb.RaiseEvent(arg);
            return tb;
        }

        public static TextBox RaiseTextChangedEvent(this TextBox tb, RoutedEvent @event, UndoAction action)
        {
            var arg = new TextChangedEventArgs(@event, action);
            tb.RaiseEvent(arg);
            return tb;
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
            var arg = new KeyEventArgs(Keyboard.PrimaryDevice, new FakePresentationSource(), 0, key)
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
            var arg = new KeyboardFocusChangedEventArgs(Keyboard.PrimaryDevice, 0, null, e)
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

    public class FakePresentationSource : PresentationSource
    {
        protected override CompositionTarget GetCompositionTargetCore()
        {
            throw new System.NotImplementedException();
        }
        public override Visual RootVisual { get; set; }
        public override bool IsDisposed
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
