using _4_104_ITElective_Activity2.core;

namespace _4_104_ITElective_Activity2.Core
{
    /// <summary>
    /// Global singleton state store.
    /// Usage:
    ///   AppStore.Set(new SomeState { ... });   // write
    ///   var s = AppStore.Get<SomeState>();     // read (null if not set)
    ///   AppStore.Subscribe<SomeState>(s => …); // react to changes
    /// </summary>
    public static class AppStore
    {
        private static readonly Dictionary<Type, object> _state = new();

        // ── Write ────────────────────────────────────────────────────────────

        /// <summary>
        /// Stores a state slice and publishes a StateChanged&lt;T&gt; event so
        /// any subscriber can react to the new value.
        /// </summary>
        public static void Set<T>(T value) where T : class
        {
            _state[typeof(T)] = value;
            EventBus.Publish(new StateChanged<T>(value));
        }

        /// <summary>Removes a state slice and publishes a StateChanged&lt;T&gt; with null.</summary>
        public static void Clear<T>() where T : class
        {
            _state.Remove(typeof(T));
            EventBus.Publish(new StateChanged<T>(null));
        }

        // ── Read ─────────────────────────────────────────────────────────────

        /// <summary>Returns the current state slice, or null if not set.</summary>
        public static T? Get<T>() where T : class
        {
            _state.TryGetValue(typeof(T), out var value);
            return value as T;
        }

        /// <summary>Returns true when the state slice has been set.</summary>
        public static bool Has<T>() where T : class => _state.ContainsKey(typeof(T));

        // ── Subscribe ────────────────────────────────────────────────────────

        /// <summary>
        /// Subscribes to changes of a specific state slice.
        /// The handler receives the new value (null when cleared).
        /// </summary>
        public static void Subscribe<T>(Action<T?> handler) where T : class
            => EventBus.Subscribe<StateChanged<T>>(e => handler(e.Value));

        /// <summary>Unsubscribes from changes of a specific state slice.</summary>
        public static void Unsubscribe<T>(Action<T?> handler) where T : class
            => EventBus.Unsubscribe<StateChanged<T>>(e => handler(e.Value));

        /// <summary>Clears all stored state (useful on logout or app reset).</summary>
        public static void Reset() => _state.Clear();
    }

    /// <summary>Event payload emitted whenever a state slice changes.</summary>
    public sealed class StateChanged<T>
    {
        public T? Value { get; }
        public StateChanged(T? value) => Value = value;
    }
}
