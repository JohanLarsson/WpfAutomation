namespace WpfAutomation
{
    using System;
    using System.CodeDom.Compiler;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
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
            SubscribeUiElement(EventTextBox, EventCheckBox, EventButton);
            SubscribeFrameworkElement(EventTextBox, EventCheckBox, EventButton);
            SubscribeControl(EventTextBox, EventCheckBox, EventButton);
            SubscribeTextBox(EventTextBox);
            SubscribeToggleButton(EventCheckBox);
            SubscribeButton(EventButton, EventCheckBox);
        }

        private void SubscribeUiElement(params UIElement[] elements)
        {
            foreach (var e in elements)
            {
                e.DragEnter += OnDragEventArgs;
                e.DragLeave += OnDragEventArgs;
                e.DragOver += OnDragEventArgs;
                e.Drop += OnDragEventArgs;
                e.FocusableChanged += OnDependencyPropertyChangedEventArgs;
                e.GiveFeedback += OnGiveFeedbackEventArgs;
                e.GotFocus += OnRoutedEventArgs;
                e.GotKeyboardFocus += OnKeyboardFocusChangedEventArgs;
                e.GotMouseCapture += OnMouseEventArgs;
                e.GotStylusCapture += OnStylusEventArgs;
                e.GotTouchCapture += OnTouchEventArgs;
                e.IsEnabledChanged += OnDependencyPropertyChangedEventArgs;
                e.IsHitTestVisibleChanged += OnDependencyPropertyChangedEventArgs;
                e.IsKeyboardFocusedChanged += OnDependencyPropertyChangedEventArgs;
                e.IsKeyboardFocusWithinChanged += OnDependencyPropertyChangedEventArgs;
                e.IsMouseCapturedChanged += OnDependencyPropertyChangedEventArgs;
                e.IsMouseCaptureWithinChanged += OnDependencyPropertyChangedEventArgs;
                e.IsMouseDirectlyOverChanged += OnDependencyPropertyChangedEventArgs;
                e.IsStylusCapturedChanged += OnDependencyPropertyChangedEventArgs;
                e.IsStylusCaptureWithinChanged += OnDependencyPropertyChangedEventArgs;
                e.IsStylusDirectlyOverChanged += OnDependencyPropertyChangedEventArgs;
                e.IsVisibleChanged += OnDependencyPropertyChangedEventArgs;
                e.KeyDown += OnKeyEventArgs;
                e.KeyUp += OnKeyEventArgs;
                //e.LayoutUpdated += OnEvent;
                e.LostFocus += OnRoutedEventArgs;
                e.LostKeyboardFocus += OnKeyboardFocusChangedEventArgs;
                e.LostMouseCapture += OnMouseEventArgs;
                e.LostStylusCapture += OnStylusEventArgs;
                e.LostTouchCapture += OnTouchEventArgs;
                e.ManipulationBoundaryFeedback += OnManipulationBoundaryFeedbackEventArgs;
                e.ManipulationCompleted += OnManipulationCompletedEventArgs;
                e.ManipulationDelta += OnManipulationDeltaEventArgs;
                e.ManipulationInertiaStarting += OnManipulationInertiaStartingEventArgs;
                e.ManipulationStarted += OnManipulationStartedEventArgs;
                e.ManipulationStarting += OnManipulationStartingEventArgs;
                e.MouseDown += OnMouseButtonEventArgs;
                e.MouseEnter += OnMouseEventArgs;
                e.MouseLeave += OnMouseEventArgs;
                e.MouseLeftButtonDown += OnMouseButtonEventArgs;
                e.MouseLeftButtonUp += OnMouseButtonEventArgs;
                //e.MouseMove += OnMouseEventArgs;
                e.MouseRightButtonDown += OnMouseButtonEventArgs;
                e.MouseRightButtonUp += OnMouseButtonEventArgs;
                e.MouseUp += OnMouseButtonEventArgs;
                e.MouseWheel += OnMouseWheelEventArgs;
                e.PreviewDragEnter += OnDragEventArgs;
                e.PreviewDragLeave += OnDragEventArgs;
                e.PreviewDragOver += OnDragEventArgs;
                e.PreviewDrop += OnDragEventArgs;
                e.PreviewGiveFeedback += OnGiveFeedbackEventArgs;
                e.PreviewGotKeyboardFocus += OnKeyboardFocusChangedEventArgs;
                e.PreviewKeyDown += OnKeyEventArgs;
                e.PreviewKeyUp += OnKeyEventArgs;
                e.PreviewLostKeyboardFocus += OnKeyboardFocusChangedEventArgs;
                e.PreviewMouseDown += OnMouseButtonEventArgs;
                e.PreviewMouseLeftButtonDown += OnMouseButtonEventArgs;
                e.PreviewMouseLeftButtonUp += OnMouseButtonEventArgs;
                //e.PreviewMouseMove += OnMouseEventArgs;
                e.PreviewMouseRightButtonDown += OnMouseButtonEventArgs;
                e.PreviewMouseRightButtonUp += OnMouseButtonEventArgs;
                e.PreviewMouseUp += OnMouseButtonEventArgs;
                e.PreviewMouseWheel += OnMouseWheelEventArgs;
                //e.PreviewQueryContinueDrag += OnQueryContinueDragEventArgs;
                e.PreviewStylusButtonDown += OnStylusButtonEventArgs;
                e.PreviewStylusButtonUp += OnStylusButtonEventArgs;
                e.PreviewStylusDown += OnStylusDownEventArgs;
                e.PreviewStylusInAirMove += OnStylusEventArgs;
                e.PreviewStylusInRange += OnStylusEventArgs;
                e.PreviewStylusMove += OnStylusEventArgs;
                e.PreviewStylusOutOfRange += OnStylusEventArgs;
                e.PreviewStylusSystemGesture += OnStylusSystemGestureEventArgs;
                e.PreviewStylusUp += OnStylusEventArgs;
                e.PreviewTextInput += OnTextCompositionEventArgs;
                e.PreviewTouchDown += OnTouchEventArgs;
                e.PreviewTouchMove += OnTouchEventArgs;
                e.PreviewTouchUp += OnTouchEventArgs;
                e.QueryContinueDrag += OnQueryContinueDragEventArgs;
                //e.QueryCursor += OnQueryCursorEventArgs;
                e.StylusButtonDown += OnStylusButtonEventArgs;
                e.StylusButtonUp += OnStylusButtonEventArgs;
                e.StylusDown += OnStylusDownEventArgs;
                e.StylusEnter += OnStylusEventArgs;
                e.StylusInAirMove += OnStylusEventArgs;
                e.StylusInRange += OnStylusEventArgs;
                e.StylusLeave += OnStylusEventArgs;
                e.StylusMove += OnStylusEventArgs;
                e.StylusOutOfRange += OnStylusEventArgs;
                e.StylusSystemGesture += OnStylusSystemGestureEventArgs;
                e.StylusUp += OnStylusEventArgs;
                e.TextInput += OnTextCompositionEventArgs;
                e.TouchDown += OnTouchEventArgs;
                e.TouchEnter += OnTouchEventArgs;
                e.TouchLeave += OnTouchEventArgs;
                e.TouchMove += OnTouchEventArgs;
                e.TouchUp += OnTouchEventArgs;

            }
        }

        private void SubscribeFrameworkElement(params FrameworkElement[] elements)
        {
            foreach (var e in elements)
            {
                e.ContextMenuClosing += OnContextMenuEventArgs;
                e.ContextMenuOpening += OnContextMenuEventArgs;
                e.DataContextChanged += OnDependencyPropertyChangedEventArgs;
                e.Initialized += OnInitialized;
                e.Loaded += OnRoutedEventArgs;
                e.RequestBringIntoView += OnRequestBringIntoViewEventArgs;
                e.SizeChanged += OnSizeChangedEventArgs;
                e.SourceUpdated += OnDataTransferEventArgs;
                e.TargetUpdated += OnDataTransferEventArgs;
                e.ToolTipClosing += OnToolTipEventArgs;
                e.ToolTipOpening += OnToolTipEventArgs;
                e.Unloaded += OnRoutedEventArgs;
            }
        }

        private void SubscribeControl(params Control[] controls)
        {
            foreach (var c in controls)
            {
                c.PreviewMouseDoubleClick += OnMouseButtonEventArgs;
                c.MouseDoubleClick += OnMouseButtonEventArgs;
            }
        }

        private void SubscribeTextBox(TextBox tb)
        {
            tb.SelectionChanged += OnRoutedEventArgs;
            tb.TextChanged += OnTextChanged;
        }

        private void SubscribeButton(params ButtonBase[] buttons)
        {
            foreach (var b in buttons)
            {
                b.Click += OnRoutedEventArgs;
            }
        }

        private void SubscribeToggleButton(ToggleButton tb)
        {
            tb.Checked += OnRoutedEventArgs;
            tb.Indeterminate += OnRoutedEventArgs;
            tb.Unchecked += OnRoutedEventArgs;
        }

        private void OnDataTransferEventArgs(object sender, DataTransferEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseDataTransferEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }
        private void OnRequestBringIntoViewEventArgs(object sender, RequestBringIntoViewEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseRequestBringIntoViewEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }
        private void OnInitialized(object sender, EventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseInitialized()");
            CodeBox.Text = _sw.ToString();
        }
        private void OnContextMenuEventArgs(object sender, ContextMenuEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseContextMenuEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }
        private void OnSizeChangedEventArgs(object sender, SizeChangedEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseSizeChangedEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }
        private void OnToolTipEventArgs(object sender, ToolTipEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseToolTipEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }

        private void OnRoutedEventArgs(object sender, RoutedEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            if (e.RoutedEvent == ToggleButton.CheckedEvent || e.RoutedEvent == ToggleButton.UncheckedEvent)
            {
                _itw.WriteLine(".Toggle() // " + GetValue(sender));
            }
            else
            {
                _itw.WriteLine(".RaiseRoutedEvent({0}) // {1}", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event", GetValue(sender));
            }

            CodeBox.Text = _sw.ToString();
        }

        private void OnDependencyPropertyChangedEventArgs(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            string s = string.Format(".SetProp({0}, {1}) // {2}", e.Property.OwnerType.Name + "." + e.Property.Name + "Property", e.NewValue, GetValue(sender));
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

        private void OnKeyEventArgs(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            var code = string.Format(".RaiseKeyboardEvent({0}, key) // {1}", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event", GetValue(sender));

            if (e.RoutedEvent == UIElement.PreviewKeyDownEvent)
            {
                _itw.WriteLine();
                _itw.Indent = 0;
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

        private void OnManipulationStartingEventArgs(object sender, ManipulationStartingEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseManipulationStartingEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }

        private void OnManipulationStartedEventArgs(object sender, ManipulationStartedEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseManipulationStartedEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }
        private void OnManipulationInertiaStartingEventArgs(object sender, ManipulationInertiaStartingEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseManipulationInertiaStartingEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }

        private void OnManipulationDeltaEventArgs(object sender, ManipulationDeltaEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseManipulationDeltaEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }

        private void OnManipulationCompletedEventArgs(object sender, ManipulationCompletedEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseManipulationCompletedEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }

        private void OnManipulationBoundaryFeedbackEventArgs(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseManipulationBoundaryFeedbackEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }
        private void OnGiveFeedbackEventArgs(object sender, GiveFeedbackEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseGiveFeedbackEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }
        private void OnQueryContinueDragEventArgs(object sender, QueryContinueDragEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseQueryContinueDragEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }
        private void OnQueryCursorEventArgs(object sender, QueryCursorEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseQueryCursorEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }

        private void OnDragEventArgs(object sender, DragEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseDragEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }

        private void OnStylusDownEventArgs(object sender, StylusDownEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseStylusDownEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }

        private void OnStylusSystemGestureEventArgs(object sender, StylusSystemGestureEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseStylusSystemGestureEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }

        private void OnStylusButtonEventArgs(object sender, StylusButtonEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseStylusButtonEvent({0})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event");
            CodeBox.Text = _sw.ToString();
        }

        private void OnKeyboardFocusChangedEventArgs(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
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

            _itw.WriteLine(".RaiseTextChangedEvent({0}, undoAction) // undoAction: {1} {2}", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event", e.UndoAction, GetValue(sender));
            CodeBox.Text = _sw.ToString();
        }

        private void OnTextCompositionEventArgs(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseTextInputEvent({0}, {1}) // {2}", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event", e.Text, GetValue(sender));
            CodeBox.Text = _sw.ToString();
        }

        private void OnMouseButtonEventArgs(object sender, MouseButtonEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            var code = string.Format(".RaiseMouseButton({0}, {1}, {2}) // {3}", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event", "MouseButton." + e.ChangedButton, e.ClickCount, GetValue(sender));

            if (e.RoutedEvent == PreviewMouseLeftButtonDownEvent ||
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

        private void OnMouseWheelEventArgs(object sender, MouseWheelEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseMouseWheel({0}, {1})", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event", e.Delta);
            CodeBox.Text = _sw.ToString();
        }

        private void OnMouseEventArgs(object sender, MouseEventArgs e)
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

        private void OnTouchEventArgs(object sender, TouchEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseTouch({0}) // {1}", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event", e.TouchDevice);
            CodeBox.Text = _sw.ToString();
        }
        private void OnStylusEventArgs(object sender, StylusEventArgs e)
        {
            if (!IsSampling(sender))
            {
                return;
            }
            _itw.WriteLine(".RaiseStylus({0}) // {1}", e.RoutedEvent.OwnerType.Name + "." + e.RoutedEvent.Name + "Event", e.StylusDevice);
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
            if (SampleButton.IsChecked == true && ReferenceEquals(sender, EventButton))
            {
                return true;
            }
            if (SampleTextBox.IsChecked == true && ReferenceEquals(sender, EventTextBox))
            {
                return true;
            }
            return false;
        }

        private string GetValue(object sender)
        {
            var tb = sender as TextBox;
            if (tb != null)
            {
                return "value: " + tb.Text;
            }
            var toggleButton = sender as ToggleButton;
            if (toggleButton != null)
            {
                return "value: " + (toggleButton.IsChecked == null ? "null" : ToFirstCharLower((toggleButton.IsChecked == true).ToString()));
            }
            var selector = sender as Selector;
            if (selector != null)
            {
                return "value: " + selector.SelectedItem;
            }
            return "";
        }

        private static string ToFirstCharLower(string s)
        {
            return new string(s.Take(1).Select(char.ToLower).Concat(s.Skip(1)).ToArray());
        }
    }
}
