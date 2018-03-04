Obvs.NoneEndpoint
===
[![NuGet](https://img.shields.io/nuget/v/Obvs.NoneEndpoint.svg?maxAge=3600)](https://www.nuget.org/packages/Obvs.NoneEndpoint/)
[![GitHub license](https://img.shields.io/github/license/idubnori/Obvs.NoneEndpoint.svg)](https://github.com/idubnori/Obvs.NoneEndpoint/blob/master/LICENSE)
[![Build status](https://ci.appveyor.com/api/projects/status/jkiyk1s624u59x75/branch/master?svg=true)](https://ci.appveyor.com/project/idubnori/obvs-noneendpoint/branch/master)

None Endpoint extension for [Obvs](https://github.com/christopherread/Obvs).<br> It provides **`.WithNoneEndpoints()`** for using `IServiceBus` as In-Memory ServiceBus.

### Install via NuGet
Package Manager :<br>
 `PM> Install-Package Obvs.NoneEndpoint`

.NET CLI :<br>
 `dotnet add package Obvs.NoneEndpoint`

### Example

Create `IServiceBus` as in-memory service bus.
```
IServiceBus serviceBus = ServiceBus.Configure()
        .WithNoneEndpoints()
        .PublishLocally().AnyMessagesWithNoEndpointClients()
        .Create();
```

### When use?
##### - In anticipation of microservices design
 Internal architecture/design would be microservices, but these host on one process/service. In this case, use `WithNoneEndpoints()` for initializing `IServiceBus`. 


##### - Unit testing
Without using any MQ services.

### License
[MIT License](./LICENSE)