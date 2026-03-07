# Changelog

All notable changes to this project will be documented in this file.

## [10.0.0] - 2026-03-07

### Changed

- Migrate all projects from .NET 9 to .NET 10
- Upgrade Aspire hosting from 9.0.0 to 13.1.2
- Upgrade MailDev container image from 2.1.0 to 2.2.1
- Upgrade Microsoft.Extensions.Http.Resilience to 10.3.0
- Upgrade Microsoft.Extensions.ServiceDiscovery to 10.3.0
- Upgrade all OpenTelemetry packages from 1.9.0 to 1.15.0
- Restructure AppHost to use inline Aspire SDK reference
- Add tracing filter to exclude health check endpoints
- Update CI/CD workflows to use .NET 10 SDK
- Add global.json pinning .NET 10 SDK

## [9.0.0] - 2024-10-11

### Added

- Update target framework to .NET 9.0 and bump package versions to 9.0.0
- Add new Mail client project (`Berrevoets.Aspire.Client.Mail`)
- Update CI pipeline for .NET 9.0 with separate host and client build steps

### Changed

- Remove project reference from Client.Mail (standalone package)

## [8.2.1] - 2024-10-10

### Features

- Initial release
- MailDev hosting integration for .NET Aspire (`Berrevoets.Aspire.Hosting.MailDev`)
- `MailDevResource` with SMTP and HTTP endpoint support
- `AddMailDev()` extension method for `IDistributedApplicationBuilder`
- CI/CD workflows for preview and stable NuGet package publishing
