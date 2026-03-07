# Berrevoets.Aspire.MailDev

.NET Aspire integration for [MailDev](https://github.com/maildev/maildev),
a fake SMTP server with a web UI for email testing during development.

## Packages

| Package | Description |
|---|---|
| [Berrevoets.Aspire.Hosting.MailDev](Berrevoets.Aspire.Hosting.MailDev/README.md) | Aspire hosting integration for MailDev |
| [Berrevoets.Aspire.Client.Mail](Berrevoets.Aspire.Client.Mail/README.md) | Client library for consuming the mail resource |

## Quick Start

### AppHost setup

In your AppHost project, add the hosting package:

```dotnetcli
dotnet add package Berrevoets.Aspire.Hosting.MailDev
```

Then register the MailDev resource:

```csharp
var maildev = builder.AddMailDev("maildev");

var myService = builder.AddProject<Projects.MyService>()
                       .WithReference(maildev);
```

### Client setup

In your service project, add the client package:

```dotnetcli
dotnet add package Berrevoets.Aspire.Client.Mail
```

Then use the mail client:

```csharp
builder.AddMailKitClient("maildev");
```

## Building

```bash
dotnet restore
dotnet build
dotnet test
```

## License

This project is licensed under the GNU GPLv3 License. See the
[LICENSE](LICENSE) file for details.

## Feedback & Contributing

<https://github.com/bberrevoets/Berrevoets.Aspire.MailDev>
