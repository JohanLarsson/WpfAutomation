namespace WpfAutomation
{
    using System;
    using System.Reflection;

    public static class EventInfoExt
    {
        public static Type GetArgsType(this EventInfo info)
        {
            Type eventHandlerType = info.EventHandlerType;
            MethodInfo method = eventHandlerType.GetMethod("Invoke");
            ParameterInfo parameter = method.GetParameters()[1];
            return parameter.ParameterType;
        }
    }
}
