using System;
using System.Collections.Generic;
using System.Text;

namespace _4_104_ITElective_Activity2.core
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> eventHandlers = new Dictionary<Type, List<Delegate>>();
        
        public static void Subscribe<T>(Action<T> handler)
        {
            var eventType = typeof(T);
            if (!eventHandlers.ContainsKey(eventType))
            {
                eventHandlers[eventType] = new List<Delegate>();
            }
            eventHandlers[eventType].Add(handler);
        }
        public static void Unsubscribe<T>(Action<T> handler)
        {
            var eventType = typeof(T);
            if (eventHandlers.ContainsKey(eventType))
            {
                eventHandlers[eventType].Remove(handler);
            }
        }
        public static void Publish<T>(T eventData)
        {
            var eventType = typeof(T);
            if (eventHandlers.ContainsKey(eventType))
            {
                foreach (var handler in eventHandlers[eventType])
                {
                    ((Action<T>)handler)?.Invoke(eventData);
                }
            }
        }
        public static void Clear()
        {
            eventHandlers.Clear();
        }
    }
}
