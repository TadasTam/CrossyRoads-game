namespace CrossyRoad2D.Server.Utils
{
    public static class DependencyInjectionUtils
    {
        public static IServiceCollection Decorate<TInterface, TDecorator>(this IServiceCollection services)
            where TInterface : class
            where TDecorator : class, TInterface
        {
            var objectFactory = ActivatorUtilities.CreateFactory(
                typeof(TDecorator),
                new[] { typeof(TInterface) });

            // Save all descriptors that needs to be decorated into a list.
            var descriptorsToDecorate = services
                .Where(s => s.ServiceType == typeof(TInterface))
                .ToList();

            if (descriptorsToDecorate.Count == 0)
            {
                throw new InvalidOperationException($"Attempted to Decorate services of type {typeof(TInterface)}, " +
                                                    "but no such services are present in ServiceCollection");
            }

            foreach (var descriptor in descriptorsToDecorate)
            {
                // Create new descriptor with prepared object factory.
                var decorated = ServiceDescriptor.Describe(
                    typeof(TInterface),
                    s => (TInterface)objectFactory(s, new[] { s.CreateInstance(descriptor) }),
                    descriptor.Lifetime);

                services.Remove(descriptor);
                services.Add(decorated);
            }

            return services;
        }

        private static object CreateInstance(this IServiceProvider services, ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationInstance != null)
            {
                return descriptor.ImplementationInstance;
            }

            if (descriptor.ImplementationFactory != null)
            {
                return descriptor.ImplementationFactory(services);
            }

            return ActivatorUtilities.GetServiceOrCreateInstance(services, descriptor.ImplementationType);
        }
    }
}
