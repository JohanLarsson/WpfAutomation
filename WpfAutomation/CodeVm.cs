namespace WpfAutomation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Annotations;

    public class CodeVm : INotifyPropertyChanged
    {
        private Type _type;
        private string _subscriptions;
        public CodeVm()
        {
            Type = typeof(UIElement);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public IEnumerable<Type> AllTypes
        {
            get
            {
                var types = typeof(Control).Assembly.GetTypes();
                var enumerable = types.Where(x => x.IsSubclassOf(typeof(UIElement))).ToArray();
                return enumerable
                            .OrderBy(x => x.Name);
            }
        }
        public Type Type
        {
            get { return _type; }
            private set
            {
                if (Equals(value, _type))
                {
                    return;
                }
                _type = value;
                Subscriptions = GeneratedCode(_type);
                OnPropertyChanged();
            }
        }
        public string Subscriptions
        {
            get { return _subscriptions; }
            private set
            {
                if (value == _subscriptions)
                {
                    return;
                }
                _subscriptions = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string GeneratedCode(Type type)
        {
            var xamlBuilder = new StringWriter();
            var codeBuilder = new StringWriter();

            while (type!=typeof(DependencyObject))
            {
                codeBuilder.WriteLine();
                codeBuilder.WriteLine(type.Name);
                var eventInfos = type.GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                     .OrderBy(x => x.Name)
                     .ToArray();

                var methodInfos = typeof(EventsSampler).GetMethods(BindingFlags.DeclaredOnly | BindingFlags.NonPublic);
                foreach (var eventInfo in eventInfos)
                {
                    var argsType = eventInfo.GetArgsType();
                    string method = "On" + argsType.Name;

                    if (eventInfo.EventHandlerType == typeof(EventHandler))
                    {
                        xamlBuilder.WriteLine(@"{0} = ""On{0}Event""", eventInfo.Name);
                        codeBuilder.WriteLine(@"e.{0} += OnEvent", eventInfo.Name);
                    }
                    else
                    {
                        xamlBuilder.WriteLine(eventInfo.Name + @"=""OnEvent""");
                        codeBuilder.WriteLine(@"e.{0} += {1};", eventInfo.Name, method);
                    }
                }
                type = type.BaseType;
            }


            return codeBuilder.ToString();
        }
    }
}
