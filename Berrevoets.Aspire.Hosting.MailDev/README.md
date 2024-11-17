# Berrevoets.Aspire.Hosting.MailDev library

Provides extension methods and resource definitions for a .NET Aspire AppHost to configure a MailDev Server.

## Getting started

### Install the package

In your AppHost project, install the .NET Aspire MailDev Hosting library with [NuGet](https://www.nuget.org):

```dotnetcli
dotnet add package Berrevoets.Aspire.Hosting.MailDev
```

## Usage example

Then, in the _Program.cs_ file of `AppHost`, add a MongoDB resource and consume the connection using the following methods:

```csharp
var maildev = builder.AddMailDev("maildev");

var myService = builder.AddProject<Projects.MyService>()
                       .WithReference(maildev);
```

## Additional documentation
https://learn.microsoft.com/dotnet/aspire

## Feedback & contributing

https://github.com/bberrevoets/Berrevoets.Aspire.MailDev