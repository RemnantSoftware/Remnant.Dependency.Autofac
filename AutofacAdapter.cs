using Autofac;
using System;
using IAutofacContainer = Autofac.IContainer;
using IContainer = Remnant.Dependency.Interface.IContainer;

namespace Remnant.Dependency.Autofac
{
	public class AutofacAdapter : Remnant.Dependency.Interface.IContainer
	{
		private readonly ContainerBuilder _containerBuilder;
		private IAutofacContainer _container;

		public AutofacAdapter(ContainerBuilder containerBuilder)
		{
			_containerBuilder = containerBuilder ?? throw new ArgumentNullException(nameof(containerBuilder));
			containerBuilder.RegisterBuildCallback((container) => { _container = container as IAutofacContainer; });
		}

		public IContainer Clear()
		{
			throw new NotSupportedException("Autofac does not support 'Clear'.");
		}

		public IContainer DeRegister<TType>() where TType : class
		{
			throw new NotSupportedException("Deregister not supported for autofac for now.");
		}

		public IContainer DeRegister(object instance)
		{
			throw new NotSupportedException("Deregister not supported for autofac for now.");
		}

		public IContainer Register<TType>(object instance) where TType : class
		{
			_containerBuilder.RegisterInstance(instance).As(typeof(TType));
			return this;
		}

		public IContainer Register(Type type, object instance)
		{
			_containerBuilder.RegisterInstance(instance).As(type);
			return this;	
		}

		public IContainer Register(object instance)
		{
			_containerBuilder.RegisterInstance(instance).As(instance.GetType());
			return this;
		}

		public IContainer Register<TType>() where TType : class, new()
		{
			_containerBuilder.RegisterType(typeof(TType)).As<TType>().SingleInstance();
			return this;
		}

		public IContainer Register<TType, TObject>()
			where TType : class
			where TObject : class, new()
		{
			_containerBuilder.RegisterType(typeof(TObject)).As<TType>().SingleInstance();
			return this;
		}

		public TType ResolveInstance<TType>() where TType : class
		{
			return _container.Resolve<TType>();
		}

		public TContainer InternalContainer<TContainer>() where TContainer : class
		{
			if (_container == null)
				throw new InvalidOperationException("The Autofac container is null, ensure you have called 'Build' which creates the container via callback.");

			if (_container as TContainer == null)
				throw new InvalidCastException($"The internal container is of type {_container.GetType().FullName} and cannot be cast to {typeof(TContainer).FullName}");

			return _container as TContainer;
		}
	}
}
