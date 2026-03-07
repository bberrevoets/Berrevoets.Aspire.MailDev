# Changelog

All notable changes to this project will be documented in this file.

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
