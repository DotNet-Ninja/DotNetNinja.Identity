# DotNetNinja.Identity

ASP.NET Core Identity Provider Based on IdentityServer4

## Getting Started > Setup

For the most part the application should come ready to run out of the box, you will however need to create a secrets.json file (or use another means such as environment variables) to provide the following values.

- Secrets:SigningCertificate:Path
- Secrets:SigningCertificate:Password
- Secrets:ConnectionStrings:PersistedGrantDb
- Secrets:ConnectionStrings:ConfigurationDb
- Secrets:ConnectionStrings:UserDb

**NOTE:** _If you do not know how to manage secrets in your projects check out the [documentation article](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows) from Microsoft._

secrets.json

```json
{
  "Secrets": {
    "SigningCertificate": {
      "Path": "C:\\Your\\Repository\\Path\\tools\\your.file.name.pfx",
      "Password": "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
  },
  "ConnectionStrings": {
      "PersistedGrantDb": "server=[SERVERNAME]; Database=DotNetNinjaIdentity; User Id=[USERNAME]; Password=[PASSWORD]; Application Name=IS4.PersistedGrant",
      "ConfigurationDb": "server=[SERVERNAME]; Database=DotNetNinjaIdentity; User Id=[USERNAME]; Password=[PASSWORD]; Application Name=IS4.PersistedGrant",
      "UserDb": "server=[SERVERNAME]; Database=DotNetNinjaIdentity; User Id=[USERNAME]; Password=[PASSWORD]; Application Name=IS4.PersistedGrant"
    }
  }
}
```

See the sections below for more information.

### Creating a Signing Certificate

- Ensure you have OpenSSL Installed
  - If not you can install via chocolatey
  ```shell
    choco install openssl
  ```
- Run GenerateCertificate.ps1
  ```shell
    cd .\tools
    .\GenerateCertificate.ps1 -Name ninja.identity
  ```
- Answer the prompts (Be sure to save the password in a secure place!)

### Configuring the Signing Certificate

Assuming you have followed the instructions above, you should now have a .cer, .key & .pfx file in the tools folder of the solution. (Note: There is a rule in place in the .gitignore file to keep from checking these in, but be careful!)

At this point the code simply needs to be able to find the certificate and it needs a password to load it. It will attempt to get those from configuration values from:

- Secrets:SigningCertificate:Path
- Secrets:SigningCertificate:Password

In order to provide those you will want to create a secrets file containing something like this (substitue your values):

```json
{
  "Secrets": {
    "SigningCertificate": {
      "Path": "C:\\Your\\Repository\\Path\\tools\\your.file.name.pfx",
      "Password": "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
    }
  },
  ...
}
```

### Connection Strings

The application needs 3 database connection strings to function.

- Operational Store (PersistedGrantDb => Secrets:ConnectionStrings:PersistedGrantDb)
- Configuration Store (ConfigurationDb => Secrets:ConnectionStrings:ConfigurationDb)
- User Store (UserDb => Secrets:ConnectionStrings:UserDb)

These can all be the same connection string to use a single database for your data (as in my example below), or separate.

```json
{
  ...
    "ConnectionStrings": {
      "PersistedGrantDb": "server=[SERVERNAME]; Database=DotNetNinjaIdentity; User Id=[USERNAME]; Password=[PASSWORD]; Application Name=IS4.PersistedGrant",
      "ConfigurationDb": "server=[SERVERNAME]; Database=DotNetNinjaIdentity; User Id=[USERNAME]; Password=[PASSWORD]; Application Name=IS4.PersistedGrant",
      "UserDb": "server=[SERVERNAME]; Database=DotNetNinjaIdentity; User Id=[USERNAME]; Password=[PASSWORD]; Application Name=IS4.PersistedGrant"
    }
  }
}
```

## Getting Started > Other Configuration

### Enabling/Disabling Automatic Migrations

In the appsettings.json file with a "Migrations" section. This section controls the behavior of EntityFramework migrations. Use the settings for each context under "AutomaticMigrationsEnabled" (Migrations:AutomaticMigrationsEnabled) to control whether the system will automatically perform migrations for each database context at start up.

```json
  "Migrations": {
    "AutomaticMigrationsEnabled": {
      "PersistedGrantDbContext": true,
      "ConfigurationDbContext": true,
      "UserDbContext": true
    },
    ...
  }
```

**Note:** _If any of these settings are not found they will effectively default to false/off._

### Enabling/Disabling Database Seeding

During migrations (if enabled) the system can seed the database(s) with data to bootstrap the system. By default this is enabled an a number of ApiResources, IdentityResources, Clients, and UserAccounts are automatically provisioned. This can be turned on/off using the "EntitySeedingEnabled" section of the config under "Migrations" (Migrations:EntitySeedingEnabled).

**Note:** _Seeding only runs for contexts that have migrations enabled! APIResources, IdentityResources, and Clients are in the ConfigurationDbContext. UserAccounts are in the UserDbContext. Truning on seeding without turning on migrations will result in no seeding occurring._

```json
  "Migrations": {
    ...
    "EntitySeedingEnabled": {
      "ApiResources": true,
      "IdentityResources": true,
      "Clients": true,
      "Users": true
    }
  }
```

**Note:** _If any of these settings are not found they will effectively default to false/off._
