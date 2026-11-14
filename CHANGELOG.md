# Changelog

All notable changes to this project will be documented in this file.

## Attributors

- **Bert Berrevoets** -- project owner
- **Claude Opus 4.6** -- co-author of the .NET 10 / Aspire 13 migration and the project documentation (2026-03)
- **Claude Opus 5.5** -- co-author of the 2026-11 AI agent setup
- **Claude Fable 5.2** -- co-author of the 10.1.1 security release (2026-11)

## [Unreleased]

### Fixed

Author: *Claude Opus 5.5*

- Publishing to NuGet.org uses Trusted Publishing (`NuGet/login`, GitHub OIDC) instead of the stored `NUGET_API_KEY`
  secret, which had expired on 2026-10-21: the publish job of the 10.1.0 merge failed with 403, so 10.1.0 reaches
  NuGet with this change

## [10.1.1] - 2026-11-14

`Berrevoets.Aspire.Hosting.MailDev` only; `Berrevoets.Aspire.Client.Mail` stays 10.0.0.

### Security

Author: *Claude Fable 5.2*

- The package now depends on Aspire.Hosting 13.6.5 (Dependabot security update). Aspire before 13.6.5 is affected by
  GHSA-f7vh-h5g3-7jrh (high: CVE-2026-120006, elevation of privilege). AppHosts that use this package now need
  Aspire.Hosting 13.6.5 or newer
- The sample AppHost uses `Aspire.AppHost.Sdk` 13.6.5: 13.6.4 brought a dashboard affected by GHSA-4g86-54fm-h9h8
  (critical). Dependabot does not update the SDK version in `<Project Sdk="...">`, so it was raised by hand

## [10.1.0] - 2026-11-02

`Berrevoets.Aspire.Hosting.MailDev` only; `Berrevoets.Aspire.Client.Mail` stays 10.0.0.

### Security

Author: *Claude Opus 5.5*

- The package now depends on Aspire.Hosting 13.6.4, which brings MessagePack 2.5.307. Aspire.Hosting 13.1.2 pulled in
  MessagePack 2.5.192 through StreamJsonRpc, affected by GHSA-hv8m-jj95-wg3x (high: LZ4 decompression of bad input),
  GHSA-2f33-pr97-265q and GHSA-2x83-8g95-xh59 (moderate), all fixed in 2.5.301

### Changed

Author: *Bert Berrevoets & Claude Opus 5.5*

- Aspire 13.1.2 → 13.6.4 (`Aspire.Hosting` in the Hosting package, `Aspire.AppHost.Sdk` in the sample AppHost).
  AppHosts that use this package now need Aspire.Hosting 13.6.4 or newer, hence the minor version
- AI agent setup to Bert's standard: `CLAUDE.md` moved to `AGENTS.md` (agent-neutral, for Claude Code, Codex and
  others) with a `CLAUDE.md` stub that imports it; markdownlint line length 120 → 180; repo-init blocks in
  `.gitignore` and `.gitattributes` (the two package icons stay plain git blobs); README Conventions section;
  Attributors and Author lines in this changelog
- `main` is the only long-lived branch: the default branch moved from `Development` to `main` and the preview
  workflow `CI-development.yml` is removed, so `-preview` packages are no longer published. `CI.yml` builds pull
  requests and publishes on every merge to `main` (only a raised `<Version>` reaches NuGet)

## [10.0.0] - 2026-03-07

### Changed

Author: *Bert Berrevoets & Claude Opus 4.6*

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

Author: *Bert Berrevoets*

- Update target framework to .NET 9.0 and bump package versions to 9.0.0
- Add new Mail client project (`Berrevoets.Aspire.Client.Mail`)
- Update CI pipeline for .NET 9.0 with separate host and client build steps

### Changed

Author: *Bert Berrevoets*

- Remove project reference from Client.Mail (standalone package)

## [8.2.1] - 2024-10-10

### Features

Author: *Bert Berrevoets*

- Initial release
- MailDev hosting integration for .NET Aspire (`Berrevoets.Aspire.Hosting.MailDev`)
- `MailDevResource` with SMTP and HTTP endpoint support
- `AddMailDev()` extension method for `IDistributedApplicationBuilder`
- CI/CD workflows for preview and stable NuGet package publishing
