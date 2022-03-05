# Remnant Dependency Autofac
Autofac dependency injection adapter


## Nuget package:

        Install-Package Remnant.Dependency.Autofac -Version 1.0.0
        
```csharp
var autofac = new ContainerBuilder();
Container.Create("MyContainer", new AutofacAdapter(autofac));
autofac.Build();
var container = Container.Instance;
```
The 'Build' must be called on the autofac container builder, for it performs a callback which then constructs the container.
