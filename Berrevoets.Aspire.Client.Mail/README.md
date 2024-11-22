# Berrevoets.Aspire.Client.Mail library

Registers a mail client

## Getting started

### Install the package

In your Application project, install the .NET Aspire Mail client library with [NuGet](https://www.nuget.org):

```dotnetcli
dotnet add package Berrevoets.Aspire.Client.Mail
```

## Usage example

Then, in the _Program.cs_ file of Application, add a Mail resource and use the client using the following methods:

```csharp
builder.AddMailKitClient("maildev");

async (MailKitClientFactory factory, string email) =>
            {
                var client = await factory.GetSmtpClientAsync();

                using var message = new MailMessage("newsletter@yourcompany.com", email)
                {
                    Subject = "Welcome to our newsletter!",
                    Body    = "Thank you for subscribing to our newsletter!"
                };

                await client.SendAsync(MimeMessage.CreateFromMailMessage(message));
            });
```

## Additional documentation
https://learn.microsoft.com/dotnet/aspire

## Feedback & contributing

https://github.com/bberrevoets/Berrevoets.Aspire.MailDev
