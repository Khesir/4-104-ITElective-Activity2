namespace _4_104_ITElective_Activity2.Core.DI
{
    /// <summary>
    /// Simple DI container supporting singleton and transient lifetimes.
    ///
    /// Usage:
    ///   container.Singleton&lt;MyService&gt;(() => new MyService(...));
    ///   container.Transient&lt;MyRepo&gt;(() => new MyRepo(...));
    ///   var svc = container.Resolve&lt;MyService&gt;();
    /// </summary>
    public class Container
    {
        private readonly Dictionary<Type, Func<object>> _registrations = new();
        private readonly Dictionary<Type, object>       _singletons    = new();

        // ── Registration ─────────────────────────────────────────────────────

        /// <summary>
        /// Registers a type as a singleton.
        /// The factory is called once; the same instance is returned every time.
        /// </summary>
        public Container Singleton<T>(Func<T> factory) where T : class
        {
            _registrations[typeof(T)] = () =>
            {
                if (!_singletons.TryGetValue(typeof(T), out var cached))
                {
                    cached = factory();
                    _singletons[typeof(T)] = cached;
                }
                return cached;
            };
            return this; // fluent
        }

        /// <summary>
        /// Registers a type as transient.
        /// A new instance is created on every Resolve call.
        /// </summary>
        public Container Transient<T>(Func<T> factory) where T : class
        {
            _registrations[typeof(T)] = () => factory();
            return this; // fluent
        }

        // ── Resolution ───────────────────────────────────────────────────────

        /// <summary>Resolves a registered type. Throws if not registered.</summary>
        public T Resolve<T>() where T : class
        {
            if (!_registrations.TryGetValue(typeof(T), out var factory))
                throw new InvalidOperationException(
                    $"[Container] '{typeof(T).Name}' is not registered. " +
                    $"Did you forget to add it in Program.cs?");

            return (T)factory();
        }

        /// <summary>Returns true if the type has been registered.</summary>
        public bool IsRegistered<T>() => _registrations.ContainsKey(typeof(T));
    }
}
