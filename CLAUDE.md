# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with
code in this repository.

## Project Overview

.NET Aspire integration for [MailDev](https://github.com/maildev/maildev), a
fake SMTP server with a web UI for email testing. Ships as two NuGet packages:

- **Berrevoets.Aspire.Hosting.MailDev** - Aspire hosting integration
  (adds `builder.AddMailDev("maildev")` to the AppHost)
- **Berrevoets.Aspire.Client.Mail** - Client library for consuming
  the mail resource (currently a stub with no source files yet)

## Build Commands

```bash
dotnet restore                    # Restore all packages
dotnet build                      # Build entire solution
dotnet build --configuration Release  # Release build
dotnet test --no-restore --verbosity normal  # Run tests
```

Build a specific project:

```bash
dotnet build Berrevoets.Aspire.Hosting.MailDev/Berrevoets.Aspire.Hosting.MailDev.csproj
dotnet build Berrevoets.Aspire.Client.Mail/Berrevoets.Aspire.Client.Mail.csproj
```

Run the test AppHost (requires Docker for MailDev container):

```bash
dotnet run --project MailDev.AppHost
```

## Architecture

### NuGet Library Projects (packaged and published)

- **Berrevoets.Aspire.Hosting.MailDev** - Contains two key types:
  - `MailDevResource` (namespace `Aspire.Hosting.ApplicationModel`) - Custom
    container resource implementing `IResourceWithConnectionString`. Exposes
    SMTP (port 1025) and HTTP (port 1080) endpoints. Connection string format:
    `smtp://{host}:{port}`
  - `MailDevResourceBuilderExtensions` (namespace `Aspire.Hosting`) -
    Extension method `AddMailDev()` on `IDistributedApplicationBuilder` that
    configures the `maildev/maildev:2.2.1` Docker container
- **Berrevoets.Aspire.Client.Mail** - Client-side library (no source files
  yet). Intended for `builder.AddMailKitClient("maildev")` using MailKit

### Test/Sample Projects (not published)

- **MailDev.AppHost** - Aspire AppHost for local development/testing
- **Berrevoets.Aspire.TestApp** - Minimal web app used by the AppHost
- **MailDev.ServiceDefaults** - Standard Aspire service defaults
  (OpenTelemetry, health checks, service discovery)

## Conventions

- Hosting resource types use namespace `Aspire.Hosting.ApplicationModel`
- Hosting extension methods use namespace `Aspire.Hosting`
- Both library projects target `net10.0` and generate NuGet packages on build
- Package versions are set in each `.csproj` `<Version>` element
- Container image tags are defined in `MailDevContainerImageTags` class

## CI/CD

Two GitHub Actions workflows:

- **CI-development.yml** - Triggers on push/PR to `Development` branch.
  Publishes `-preview` suffixed NuGet packages
- **CI.yml** - Triggers on push/PR to `main` branch. Publishes final
  (stable) NuGet packages

Both workflows: restore, build Release, run tests, pack with symbols
(`.snupkg`), and publish to NuGet.org.

## Branch Strategy

- `Development` - Default branch for active development (preview releases)
- `main` - Production branch (stable releases)
