namespace WpfAutomation
{
    using System.CodeDom.Compiler;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for EventsSampler.xaml
    /// </summary>
    public partial class EventsSampler : UserControl
    {
        private StringWriter _sw = new StringWriter();
        private IndentedTextWriter _itw;
        public EventsSampler()
        {
            InitializeComponent();
            _itw = new IndentedTextWriter(_sw);
            SubscribeUiElement(EventTextBox, EventCheckBox);
            SubscribeControl(EventTextBox, EventCheckBox);
            SubscribeTextBox(EventTextBox);
            SubscribeToggleButton(EventCheckBox);
        }

        private void SubscribeUiElement(params UIElement[] elements)
        {
            foreach (var e in elements)
            {
                e.GotFocus += OnRoutedEvent;
                e.LostFocus += OnRoutedEvent;

                e.PreviewGotKeyboardFocus += OnKeyboardFocus;
                e.GotKeyboardFocus += OnKeyboardFocus;
                e.PreviewLostKeyboardFocus += OnKeyboardFocus;
                e.LostKeyboardFocus += OnKeyboardFocus;

                e.PreviewKeyDown += OnKey;
                e.KeyDown += OnKey;
                e.PreviewKeyUp += OnKey;
                e.KeyUp += OnKey;

                e.PreviewTextInput += OnTextInput;
                e.TextInput += OnTextInput;

                e.IsKeyboardFocusedChanged += OnDpChanged;
                e.IsKeyboardFocusWithinChanged += OnDpChanged;

                e.PreviewMouseDown += OnMouseButton;
                e.MouseDown += OnMouseButton;
                e.PreviewMouseUp += OnMouseButton;
                e.MouseUp += OnMouseButton;

                e.PreviewMouseLeftButtonDown += OnMouseButton;
                e.MouseLeftButtonDown += OnMouseButton;
                e.PreviewMouseLeftButtonUp += OnMouseButton;
                e.MouseLeftButtonUp += OnMouseButton;

                e.PreviewMouseRightButtonDown += OnMouseButton;
                e.MouseRightButtonDown += OnMouseButton;
                e.PreviewMouseRightButtonUp += OnMouseButton;
                e.MouseRightButtonUp += OnMouseButton;

                e.PreviewMouseWheel += OnMouseWheel;
                e.MouseWheel += OnMouseWheel;

                e.MouseEnter += OnMouseEvent;
                e.MouseLeave += OnMouseEvent;
                e.GotMouseCapture += OnMouseEvent;
                e.LostMouseCapture += OnMouseEvent;

                e.IsMouseCapturedChanged += OnDpChanged;
                e.IsMouseCaptureWithinChanged += OnDpChanged;
                e.IsMouseDirectlyOverChanged += OnDpChanged;
            }
        }

        private void SubscribeControl(params Control[] controls)
        {
            foreach (var c in controls)
            {
                c.PreviewMouseDoubleClick += OnMouseButton;
                c.MouseDoubleClick += OnMouseButton;
            }
        }

        private void SubscribeTextBox(TextBox tb)
        {
            tb.SelectionChanged += OnRoutedEvent;
            tb.TextChanged += OnTextChanged;
        }

        private void SubscribeButton(Button b)
        {
            b.Click += OnRoutedEvent;
        }

        private void SubscribeToggleButton(ToggleButton tb)
        {
            tb.Click += OnRoutedEvent;
            tb.Checked += OnRoutedEvent;
            tb.Unchecked += OnRoutedEvent;
        }

        private void OnRoutedEvent(object sender, RoutedEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            if (e.RoutedEvent == ToggleButton.CheckedEvent || e.RoutedEvent == ToggleButton.UncheckedEvent)
            {
                _itw.WriteLine(".Toggle()");
            }
            else
            {
                _itw.WriteLine(".RaiseRoutedEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            }

            CodeBox.Text = _sw.ToString();
        }

        private void OnDpChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            string s = string.Format(".SetProp({0}, {1})", e.Property.OwnerType.Name + "." + e.Property.Name + "Property", e.NewValue);
            if (e.Property.ReadOnly)
            {
                _itw.WriteLine(@"// " + s);
            }
            else
            {
                _itw.WriteLine(s);
            }

            CodeBox.Text = _sw.ToString();
        }

        private void OnKey(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            var code = string.Format(".RaiseKeyboardEvent({0}, Key.{1})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event", e.Key);

            if (e.RoutedEvent == UIElement.PreviewKeyDownEvent)
            {
                _itw.WriteLine();
                var name = ToFirstCharLower(sender.GetType().Name);
                _itw.WriteLine(name + code);
                _itw.Indent = 3;
            }
            else
            {
                _itw.WriteLine(code);
            }
            CodeBox.Text = _sw.ToString();
        }

        private void OnKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            //RaiseKeyboardFocusEvent
            _itw.WriteLine(".RaiseKeyboardFocusEvent({0}) // old: {1} new: {2}", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event", e.OldFocus, e.NewFocus);
            CodeBox.Text = _sw.ToString();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseTextChangedEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }

        private void OnTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseTextInputEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }

        private void OnMouseButton(object sender, MouseButtonEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            var code = string.Format(".RaiseMouseButton({0}, {1}, {2})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event", "MouseButton." + e.ChangedButton, e.ClickCount);

            if (e.RoutedEvent == PreviewMouseLeftButtonDownEvent||
                e.RoutedEvent == PreviewMouseDoubleClickEvent || 
                e.RoutedEvent == PreviewMouseRightButtonDownEvent)
            {
                _itw.WriteLine();
                var name = ToFirstCharLower(sender.GetType().Name);
                _itw.WriteLine(name + code);
                _itw.Indent = 3;
            }
            else
            {
                _itw.WriteLine(code);

            }
            CodeBox.Text = _sw.ToString();
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseMouseWheel({0}, {1})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event", e.Delta);
            CodeBox.Text = _sw.ToString();
        }

        private void OnMouseEvent(object sender, MouseEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            if (e.RoutedEvent == UIElement.MouseEnterEvent || e.RoutedEvent == UIElement.MouseLeaveEvent)
            {
                _itw.Indent = 0;
            }
            _itw.WriteLine(".RaiseMouseEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            _sw.Dispose();
            _sw = new StringWriter();
            _itw = new IndentedTextWriter(_sw);
            CodeBox.Text = "";
        }

        private bool IsSampling(object sender)
        {
            if (SampleCheckBox.IsChecked == true && ReferenceEquals(sender, EventCheckBox))
            {
                return true;
            }
            if (SampleTextBox.IsChecked == true && ReferenceEquals(sender, EventTextBox))
            {
                return true;
            }
            return false;
        }

        private static string ToFirstCharLower(string s)
        {
            return new string(s.Take(1).Select(char.ToLower).Concat(s.Skip(1)).ToArray());
        }
    }
}
