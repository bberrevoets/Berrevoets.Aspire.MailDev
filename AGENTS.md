# AGENTS.md

Instructions for AI coding agents (Claude Code, Codex and others) working in this repository. `CLAUDE.md` only imports
this file, so edit this one. [README.md](README.md) describes the project for people; this file lists what the code
alone does not tell you.

<!-- repo-init:start -->
## Repository Info

- **Company:** Berrevoets Systems
- **Author:** Bert Berrevoets
- **Contact:** <bert@berrevoets.net>
- **Remote:** <https://github.com/bberrevoets/Berrevoets.Aspire.MailDev> (public)

## Workflow Rules

- **GitHub Flow:** `main` is the default and only long-lived branch. It is protected by the ruleset "Protect main":
  no direct pushes, no deletion, changes only through a pull request, and all PR conversations must be resolved
  before merging. Merged head branches are deleted automatically.
- After cloning, run `git config pull.rebase true` once, so `git pull` rebases instead of creating merge commits.
- Work on a branch per topic, push it, and open a PR to `main` only when Bert asks for it, assigned to him
  (`gh pr create --assignee bberrevoets`). PRs are squash-merged. After a merge: switch to `main` and pull, then delete the
  local branch with `git branch -D <branch>` (`-d` refuses after a squash merge) once
  `gh pr view <branch> --json state,headRefOid` shows `MERGED` and the same commit as `git rev-parse <branch>`.
- **"Update the documentation"** means `AGENTS.md`, the project's own `README.md` files (not vendored third-party ones)
  and `CHANGELOG.md`; then commit and push.
- [CHANGELOG.md](CHANGELOG.md) follows Keep a Changelog: the Attributors list at the top, and one `Author: *Name*`
  line under each section heading (`### Added`, `### Changed`, ...).
- Markdown must pass `markdownlint-cli2 "**/*.md"` with [.markdownlint.json](.markdownlint.json) (180-character
  lines) before every commit.
- Binaries go through Git LFS: every `filter=lfs` pattern in `.gitattributes` — the standard set (PDF, ZIP, PNG,
  JPEG, STEP/STP, STL, 3MF, F3D/F3Z) plus any pattern this repository adds. Add a pattern there before committing
  another binary type (GIF, BMP, ...). The patterns are lowercase and match `X.PNG` only where `core.ignorecase` is
  true (Windows, macOS); on a case-sensitive system check a new binary with `git check-attr filter -- <file>` before
  committing it.
- Commit messages use the conventional style (`feat:`, `fix:`, `docs:`, `chore:`).
<!-- repo-init:end -->

## Project Overview

.NET Aspire integration for [MailDev](https://github.com/maildev/maildev), a
fake SMTP server with a web UI for email testing. Ships as two NuGet packages:

- **Berrevoets.Aspire.Hosting.MailDev** - Aspire hosting integration
  (adds `builder.AddMailDev("maildev")` to the AppHost)
- **Berrevoets.Aspire.Client.Mail** - Client library for consuming
  the mail resource (currently a stub with no source files yet)

Both are **published on NuGet.org** (10.0.0 on 2026-03-07; about 9,500 and 1,500 downloads on 2026-11-02). None of
Bert's own repositories references them, so every user is external: treat a change to `AddMailDev()`, the resource
names, the endpoints or the connection string format as a breaking change for other people's AppHosts.

## Build Commands

```bash
# Agents build and test in Release: Bert keeps the solution open in Visual Studio, which uses Debug
dotnet restore                                          # Restore all packages
dotnet build --configuration Release                    # Build entire solution
dotnet test --configuration Release --no-restore        # Runs nothing yet: there is no test project
npx markdownlint-cli2 "**/*.md"                         # Markdown lint
```

Build a specific project:

```bash
dotnet build Berrevoets.Aspire.Hosting.MailDev/Berrevoets.Aspire.Hosting.MailDev.csproj
dotnet build Berrevoets.Aspire.Client.Mail/Berrevoets.Aspire.Client.Mail.csproj
```

Run the test AppHost (requires Docker for MailDev container; ask Bert first: it keeps running, starts a container and
uses fixed ports, and only one AppHost can run at a time):

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

One GitHub Actions workflow, **CI.yml** (`windows-latest`):

- On every pull request to `main`: restore, build Release, run tests (none yet).
- On a push to `main` (every merge) or a manual `workflow_dispatch`: also pack both packages with symbols (`.snupkg`)
  and push them to NuGet.org.
- **Trusted Publishing, no API key:** the `publish` job gets a NuGet API key valid for 1 hour from `NuGet/login`
  (GitHub OIDC, `id-token: write`). It works only through the policy on nuget.org (account `bberrevoets` → Trusted
  Publishing): repository `bberrevoets/Berrevoets.Aspire.MailDev`, workflow file `CI.yml`, no environment, both
  packages. Renaming `CI.yml`, moving the push to another workflow, adding a GitHub environment or renaming the
  repository breaks publishing until the policy is updated to match. Never add a stored `NUGET_API_KEY` secret again:
  the old one expired on 2026-10-21, and the 10.1.0 publish failed with 403 on 2026-11-02 until this was set up.
- The push uses `--skip-duplicate`: a merge **without** a new `<Version>` publishes nothing, silently. A change that
  must reach NuGet raises `<Version>` in that package's `.csproj` (the two packages are versioned separately) together
  with a CHANGELOG release heading. A merge publishes for real: it cannot be taken back on NuGet (only unlisted).

## Branch Strategy

`main` is the only long-lived branch (since 2026-11-02). Until then `Development` was the default branch and published
`-preview` packages through `CI-development.yml`; it was retired (its last state was identical to `main`) and
previews are no longer published.

## Binaries

The two package icons (`*/assets/icon.png`) are plain git blobs on purpose: an exception line after the repo-init
block in `.gitattributes` keeps them out of LFS. Moving them would rewrite the history the published packages point at
(SourceLink commit SHAs), and the CI checkout does not fetch LFS files.
