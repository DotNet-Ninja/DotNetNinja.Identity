# DotNetNinja.Identity

ASP.NET Core Identity Provider Based on IdentityServer4

## Creating a Signing Certificate

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

## Configuring the Signing Certificate

Assuming you have followed the instructions above, you should now have a .cer, .key & .pfx file in the tools folder of the solution. (Note: There is a rule in place in the .gitignore file to keep from checking these in, but be careful!)

At this point the code simply needs to be able to find the certificate and it needs a password to load it. It will attempt to get those from configuration values from:

- Secrets:SigningCertificate:Path
- Secrets:SigningCertificate:Password

In order to provide those you will want to create a secrets file containing something like this (substitue your values):

```json
{
  "Secrets": {
    "SigningCertificate": {
      "Path": "C:\\Your\\Repository\\PAth\\tools\\your.file.name.pfx",
      "Password": "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
    }
  }
}
```
