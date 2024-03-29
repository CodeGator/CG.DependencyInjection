﻿
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IServiceCollection"/>
    /// type.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method attempts to register a factory with the specified 
        /// service lifetime.
        /// </summary>
        /// <typeparam name="TService">The service type to use for the operation.</typeparam>
        /// <typeparam name="TImplementation">The implementation type to use for
        /// the operation.</typeparam>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="implementationFactory">The implementation factory to
        /// use for the operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the 
        /// operation.</param>
        /// <returns>True if the factory was registered; false otherwise.</returns>
        public static bool TryAdd<TService, TImplementation>(
            this IServiceCollection serviceCollection,
            Func<IServiceProvider, TImplementation> implementationFactory,
            ServiceLifetime serviceLifetime
            ) where TService : class
              where TImplementation : class, TService
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
                .ThrowIfNull(implementationFactory, nameof(implementationFactory));

            // Is the factory already registered?
            if (serviceCollection.Contains(
                new ServiceDescriptor(
                    typeof(TService),
                    implementationFactory,
                    serviceLifetime
                    ))
                )
            {
                // We didn't register the factory.
                return false;
            }

            // Defer to the overload.
            serviceCollection.Add<TService, TImplementation>(
                implementationFactory,
                serviceLifetime
                );

            // We registered the factory.
            return true;
        }

        // *******************************************************************

        /// <summary>
        /// This method registers a factory with the specified service lifetime.
        /// </summary>
        /// <typeparam name="TService">The service type to use for the operation.</typeparam>
        /// <typeparam name="TImplementation">The implementation type to use for
        /// the operation.</typeparam>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="implementationFactory">The implementation factory to
        /// use for the operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the 
        /// operation.</param>
        /// <returns>The value of the <paramref name="serviceCollection"/>
        /// parameter, for chaining calls together.</returns>
        public static IServiceCollection Add<TService, TImplementation>(
            this IServiceCollection serviceCollection,
            Func<IServiceProvider, TImplementation> implementationFactory,
            ServiceLifetime serviceLifetime
            ) where TService : class
              where TImplementation : class, TService
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
                .ThrowIfNull(implementationFactory, nameof(implementationFactory));

            // Register the factory.
            switch (serviceLifetime)
            {
                case ServiceLifetime.Scoped:
                    serviceCollection.AddScoped<TService, TImplementation>(implementationFactory);
                    break;
                case ServiceLifetime.Singleton:
                    serviceCollection.AddSingleton<TService, TImplementation>(implementationFactory);
                    break;
                case ServiceLifetime.Transient:
                    serviceCollection.AddTransient<TService, TImplementation>(implementationFactory);
                    break;
            }

            // Return the service collection.
            return serviceCollection;
        }

        // *******************************************************************

        /// <summary>
        /// This method attempt to register a factory with the specified service 
        /// lifetime.
        /// </summary>
        /// <typeparam name="TService">The service type to use for the operation.</typeparam>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="implementationFactory">The implementation factory to
        /// use for the operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the 
        /// operation.</param>
        /// <returns>True if the factory was registered; false otherwise.</returns>
        public static bool TryAdd<TService>(
            this IServiceCollection serviceCollection,
            Func<IServiceProvider, TService> implementationFactory,
            ServiceLifetime serviceLifetime
            ) where TService : class
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
                .ThrowIfNull(implementationFactory, nameof(implementationFactory));

            // Is the factory already registered?
            if (serviceCollection.Contains(
                new ServiceDescriptor(
                    typeof(TService),
                    implementationFactory,
                    serviceLifetime
                    ))
                )
            {
                // We didn't register the factory.
                return false;
            }

            // Defer to the overload.
            serviceCollection.Add(
                implementationFactory,
                serviceLifetime
                );

            // We registered the factory.
            return true;
        }

        // *******************************************************************

        /// <summary>
        /// This method registers a factory with the specified service lifetime.
        /// </summary>
        /// <typeparam name="TService">The service type to use for the operation.</typeparam>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="implementationFactory">The implementation factory to
        /// use for the operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the 
        /// operation.</param>
        /// <returns>The value of the <paramref name="serviceCollection"/>
        /// parameter, for chaining calls together.</returns>
        public static IServiceCollection Add<TService>(
            this IServiceCollection serviceCollection,
            Func<IServiceProvider, TService> implementationFactory,
            ServiceLifetime serviceLifetime
            ) where TService : class
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
                .ThrowIfNull(implementationFactory, nameof(implementationFactory));

            // Register the factory.
            switch (serviceLifetime)
            {
                case ServiceLifetime.Scoped:
                    serviceCollection.AddScoped(implementationFactory);
                    break;
                case ServiceLifetime.Singleton:
                    serviceCollection.AddSingleton(implementationFactory);
                    break;
                case ServiceLifetime.Transient:
                    serviceCollection.AddTransient(implementationFactory);
                    break;
            }

            // Return the service collection.
            return serviceCollection;
        }

        // *******************************************************************

        /// <summary>
        /// This method attempt to register a service and implementation the 
        /// specified service lifetime.
        /// </summary>
        /// <typeparam name="TService">The service type to use for the operation.</typeparam>
        /// <typeparam name="TImplementation">The implementation type to use for
        /// the operation.</typeparam>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the 
        /// operation.</param>
        /// <returns>True if the type was registered; false otherwise.</returns>
        public static bool TryAdd<TService, TImplementation>(
            this IServiceCollection serviceCollection,
            ServiceLifetime serviceLifetime
            ) where TService : class
              where TImplementation : class, TService
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection));

            // Is the factory already registered?
            if (serviceCollection.Contains(
                new ServiceDescriptor(
                    typeof(TService),
                    typeof(TImplementation),
                    serviceLifetime
                    ))
                )
            {
                // We didn't register the factory.
                return false;
            }

            // Defer to the overload.
            serviceCollection.Add<TService, TImplementation>(
                serviceLifetime
                );

            // We registered the factory.
            return true;
        }

        // *******************************************************************

        /// <summary>
        /// This method registers a service and implementation the specified 
        /// service lifetime.
        /// </summary>
        /// <typeparam name="TService">The service type to use for the operation.</typeparam>
        /// <typeparam name="TImplementation">The implementation type to use for
        /// the operation.</typeparam>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the 
        /// operation.</param>
        /// <returns>The value of the <paramref name="serviceCollection"/>
        /// parameter, for chaining calls together.</returns>
        public static IServiceCollection Add<TService, TImplementation>(
            this IServiceCollection serviceCollection,
            ServiceLifetime serviceLifetime
            ) where TService : class
              where TImplementation : class, TService
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection));

            // Register the factory.
            switch (serviceLifetime)
            {
                case ServiceLifetime.Scoped:
                    serviceCollection.AddScoped<TService, TImplementation>();
                    break;
                case ServiceLifetime.Singleton:
                    serviceCollection.AddSingleton<TService, TImplementation>();
                    break;
                case ServiceLifetime.Transient:
                    serviceCollection.AddTransient<TService, TImplementation>();
                    break;
            }

            // Return the service collection.
            return serviceCollection;
        }

        //*******************************************************************

        /// <summary>
        /// This method attempt to register a service and implementation the 
        /// specified service lifetime.
        /// </summary>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="serviceType">The service type to use for the operation.</param>
        /// <param name="implementationType">The implementation type to use for 
        /// the operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the 
        /// operation.</param>
        /// <returns>True if the type was registered; false otherwise.</returns>
        public static bool TryAdd(
            this IServiceCollection serviceCollection,
            Type serviceType,
            Type implementationType,
            ServiceLifetime serviceLifetime
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection));

            // Is the factory already registered?
            if (serviceCollection.Contains(
                new ServiceDescriptor(
                    serviceType,
                    implementationType,
                    serviceLifetime
                    ))
                )
            {
                // We didn't register the factory.
                return false;
            }

            // Defer to the overload.
            serviceCollection.Add(
                serviceType,
                implementationType,
                serviceLifetime
                );

            // We registered the factory.
            return true;
        }

        //*******************************************************************

        /// <summary>
        /// This method registers a service and implementation the specified 
        /// service lifetime.
        /// </summary>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="serviceType">The service type to use for the operation.</param>
        /// <param name="implementationType">The implementation type to use for 
        /// the operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the 
        /// operation.</param>
        /// <returns>The value of the <paramref name="serviceCollection"/>
        /// parameter, for chaining calls together.</returns>
        public static IServiceCollection Add(
            this IServiceCollection serviceCollection,
            Type serviceType,
            Type implementationType,
            ServiceLifetime serviceLifetime
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection));

            // Register the factory.
            switch (serviceLifetime)
            {
                case ServiceLifetime.Scoped:
                    serviceCollection.AddScoped(serviceType, implementationType);
                    break;
                case ServiceLifetime.Singleton:
                    serviceCollection.AddSingleton(serviceType, implementationType);
                    break;
                case ServiceLifetime.Transient:
                    serviceCollection.AddTransient(serviceType, implementationType);
                    break;
            }

            // Return the service collection.
            return serviceCollection;
        }

        //*******************************************************************

        /// <summary>
        /// This method attempts to register a service with the specified 
        /// service lifetime.
        /// </summary>
        /// <typeparam name="TService">The service type to use for the operation.</typeparam>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the 
        /// operation.</param>
        /// <returns>True if the type was registered; false otherwise.</returns>
        public static bool TryAdd<TService>(
            this IServiceCollection serviceCollection,
            ServiceLifetime serviceLifetime
            ) where TService : class
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection));

            // Is the factory already registered?
            if (serviceCollection.Contains(
                new ServiceDescriptor(
                    typeof(TService),
                    serviceLifetime
                    ))
                )
            {
                // We didn't register the factory.
                return false;
            }

            // Defer to the overload.
            serviceCollection.Add<TService>(
                serviceLifetime
                );

            // We registered the factory.
            return true;
        }

        //*******************************************************************

        /// <summary>
        /// This method registers a service with the specified service lifetime.
        /// </summary>
        /// <typeparam name="TService">The service type to use for the operation.</typeparam>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the 
        /// operation.</param>
        /// <returns>The value of the <paramref name="serviceCollection"/>
        /// parameter, for chaining calls together.</returns>
        public static IServiceCollection Add<TService>(
            this IServiceCollection serviceCollection,
            ServiceLifetime serviceLifetime
            ) where TService : class
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection));

            // Register the factory.
            switch (serviceLifetime)
            {
                case ServiceLifetime.Scoped:
                    serviceCollection.AddScoped<TService>();
                    break;
                case ServiceLifetime.Singleton:
                    serviceCollection.AddSingleton<TService>();
                    break;
                case ServiceLifetime.Transient:
                    serviceCollection.AddTransient<TService>();
                    break;
            }

            // Return the service collection.
            return serviceCollection;
        }

        //*******************************************************************

        /// <summary>
        /// This method attempt to register a service type with the specified
        /// service lifetime.
        /// </summary>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="serviceType">The service type to use for the operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the 
        /// operation.</param>
        /// <returns>True if the type was registered; false otherwise.</returns>
        public static bool TryAdd(
            this IServiceCollection serviceCollection,
            Type serviceType,
            ServiceLifetime serviceLifetime
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection));

            // Is the factory already registered?
            if (serviceCollection.Contains(
                new ServiceDescriptor(
                    serviceType,
                    serviceLifetime
                    ))
                )
            {
                // We didn't register the factory.
                return false;
            }

            // Defer to the overload.
            serviceCollection.Add(
                serviceType,
                serviceLifetime
                );

            // We registered the factory.
            return true;
        }

        //*******************************************************************

        /// <summary>
        /// This method registers a service type with the specified service 
        /// lifetime.
        /// </summary>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="serviceType">The service type to use for the operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the 
        /// operation.</param>
        /// <returns>The value of the <paramref name="serviceCollection"/>
        /// parameter, for chaining calls together.</returns>
        public static IServiceCollection Add(
            this IServiceCollection serviceCollection,
            Type serviceType,
            ServiceLifetime serviceLifetime
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection));

            // Register the factory.
            switch (serviceLifetime)
            {
                case ServiceLifetime.Scoped:
                    serviceCollection.AddScoped(serviceType);
                    break;
                case ServiceLifetime.Singleton:
                    serviceCollection.AddSingleton(serviceType);
                    break;
                case ServiceLifetime.Transient:
                    serviceCollection.AddTransient(serviceType);
                    break;
            }

            // Return the service collection.
            return serviceCollection;
        }

        // *******************************************************************

        /// <summary>
        /// This method adds a singleton service of the type specified in 
        /// <typeparamref name="TService"/> with an instance specified in the
        /// <paramref name="instance"/> parameter, to the specified 
        /// <see cref="IServiceCollection"/> object.
        /// </summary>
        /// <typeparam name="TService">The type of service to register.</typeparam>
        /// <typeparam name="TImplementation">The type of implementation to use.</typeparam>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="instance">The implementation instance to use for the 
        /// operation.</param>
        /// <returns>True if the type was registered successfully; false otherwise.</returns>
        public static bool TryAddSingleton<TService, TImplementation>(
            this IServiceCollection serviceCollection,
            TImplementation instance
            ) where TService : class
              where TImplementation : class, TService
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
                .ThrowIfNull(instance, nameof(instance));

            // Create a service descriptor for the interface type.
            var serviceDescriptor = new ServiceDescriptor(
                typeof(TService),
                typeof(TImplementation),
                ServiceLifetime.Singleton
                );

            // Is the type already registered?
            if (true == serviceCollection.Contains(serviceDescriptor))
            {
                return false; // Nothing to do.
            }

            // Register the instance.
            serviceCollection.AddSingleton<TService, TImplementation>(
                (sp) => instance
                );

            // Return the results.
            return true;
        }

        #endregion
    }
}
