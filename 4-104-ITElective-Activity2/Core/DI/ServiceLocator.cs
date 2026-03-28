namespace _4_104_ITElective_Activity2.Core.DI
{
    /// <summary>
    /// Global access point to the DI container.
    /// Configure once at startup (Program.cs), then call Get&lt;T&gt;() anywhere.
    ///
    /// Usage:
    ///   // startup
    ///   ServiceLocator.Configure(container);
    ///
    ///   // anywhere in the app
    ///   var svc = ServiceLocator.Get&lt;ProductService&gt;();
    /// </summary>
    public static class ServiceLocator
    {
        private static Container? _container;

        /// <summary>Must be called once in Program.cs before any form is opened.</summary>
        public static void Configure(Container container)
        {
            _container = container;
        }

        /// <summary>Resolves a registered service. Throws if Configure was not called first.</summary>
        public static T Get<T>() where T : class
        {
            if (_container == null)
                throw new InvalidOperationException(
                    "[ServiceLocator] Container has not been configured. " +
                    "Call ServiceLocator.Configure(container) in Program.cs.");

            return _container.Resolve<T>();
        }
    }
}
