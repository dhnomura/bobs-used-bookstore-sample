# Next Steps

## Issues resolved
- Transformed Bookstore.Domain.csproj to net8.0
- Transformed Bookstore.Data.csproj to net8.0
- Transformed Bookstore.Web.csproj to net8.0
- Transformed Bookstore.Cdk.csproj to net8.0
- Transformed Bookstore.Domain.Tests.csproj to net8.0

## Overview

The solution has been transformed with no build errors across all projects:

- `Bookstore.Data`
- `Bookstore.Domain.Tests`
- `Bookstore.Cdk`
- `Bookstore.Web`
- `Bookstore.Domain`

Since no build errors were detected, the transformation appears to have been successful. The following steps outline how to validate, test, and deploy the migrated solution.

---

## 1. Restore and Build the Solution

Run the following commands from the root of the solution to confirm a clean restore and build:

```bash
dotnet restore
dotnet build --configuration Release
```

Ensure there are no warnings that could indicate compatibility issues, deprecated APIs, or missing references that were silently ignored during transformation.

---

## 2. Run the Unit Tests

Execute the test project to confirm all existing tests pass under the new runtime:

```bash
dotnet test app/Bookstore.Domain.Tests/Bookstore.Domain.Tests.csproj --configuration Release --verbosity normal
```

Review the test output carefully. Pay attention to:
- Any tests that were previously passing but now fail.
- Any tests that are skipped or marked as inconclusive.
- Differences in behavior related to platform-specific APIs that may have been used in the original project.

---

## 3. Validate the Data Layer

The `Bookstore.Data` project likely contains database access logic, migrations, or a data context. Verify the following:

- If using **Entity Framework Core**, confirm that all migrations are present and up to date:
  ```bash
  dotnet ef migrations list --project app/Bookstore.Data
  ```
- Apply migrations against a test database to confirm schema integrity:
  ```bash
  dotnet ef database update --project app/Bookstore.Data
  ```
- Confirm that connection strings are correctly configured for the target environment in `appsettings.json` or environment variables.

---

## 4. Validate the Web Project

Run the `Bookstore.Web` project locally to confirm it starts and functions correctly:

```bash
dotnet run --project app/Bookstore.Web --configuration Release
```

Check the following:
- The application starts without runtime exceptions.
- All routes resolve correctly.
- Static assets load as expected.
- Any authentication or authorization middleware behaves as intended.
- Review `Program.cs` and `Startup.cs` (if present) to confirm middleware configuration is compatible with the target .NET version.

---

## 5. Validate the CDK Project

The `Bookstore.Cdk` project likely defines infrastructure. Confirm it synthesizes correctly:

```bash
cd app/Bookstore.Cdk
dotnet build --configuration Release
```

If this project uses the AWS CDK, run a synthesis check to validate the infrastructure definitions:

```bash
cdk synth
```

Ensure that any environment-specific configuration values (account IDs, regions, resource names) are correctly set before deploying infrastructure.

---

## 6. Review Target Framework Compatibility

Open each `.csproj` file and confirm the `<TargetFramework>` element is set to the intended cross-platform .NET version (e.g., `net8.0`). For example:

```xml
<PropertyGroup>
  <TargetFramework>net8.0</TargetFramework>
</PropertyGroup>
```

Also check for any remaining references to Windows-specific NuGet packages or APIs (e.g., `Microsoft.Win32`, `System.Windows.Forms`) that may cause runtime failures on non-Windows platforms even if they compile successfully.

---

## 7. Check NuGet Package Versions

Confirm all NuGet packages are up to date and compatible with the target framework:

```bash
dotnet list package --outdated
```

Update packages where necessary, particularly any that were carried over from the legacy project and may have newer cross-platform compatible versions available.

---

## 8. Deploy the Application

Once all validation steps pass, deploy the `Bookstore.Web` project to the target environment:

```bash
dotnet publish app/Bookstore.Web --configuration Release --output ./publish
```

Copy the contents of the `./publish` directory to the target host and start the application using the appropriate hosting mechanism (e.g., IIS, Kestrel behind a reverse proxy, or a managed app service).