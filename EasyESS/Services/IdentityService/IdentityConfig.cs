namespace EasyESS.Services.IdentityService
{
    internal class IdentityConfig
    {
        public Logging Logging { get; set; }
        public Tracing Tracing { get; set; }
        public Iissettings iisSettings { get; set; }
        public Connectionstrings ConnectionStrings { get; set; }
        public General General { get; set; }
        public Diagnostics Diagnostics { get; set; }
        public Cachevalidationservice CacheValidationService { get; set; }
        public Retrypolicy RetryPolicy { get; set; }
        public Globalization Globalization { get; set; }
        public Accountmanagement AccountManagement { get; set; }
        public Tokenissuer TokenIssuer { get; set; }
        public object[] TrustedIssuers { get; set; }
        public Useraccounts UserAccounts { get; set; }
        public Passwords Passwords { get; set; }
        public Authentication Authentication { get; set; }
        public Uservalidation UserValidation { get; set; }
        public Accountenrichment AccountEnrichment { get; set; }
    }

    internal class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    internal class Loglevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

    internal class Tracing
    {
        public bool Enabled { get; set; }
        public string ServiceName { get; set; }
        public string AgentHost { get; set; }
        public int AgentPort { get; set; }
    }

    internal class Iissettings
    {
        public bool windowsAuthentication { get; set; }
        public bool anonymousAuthentication { get; set; }
    }

    internal class Connectionstrings
    {
        public string Database { get; set; }
        public string MessagingService { get; set; }
        public string ShortPathService { get; set; }
    }

    internal class General
    {
        public bool EnableDemoMode { get; set; }
        public string ServiceEndpoint { get; set; }
        public string UserPhotoDirectory { get; set; }
        public bool EmbeddedMode { get; set; }
        public bool UseShortPaths { get; set; }
        public object ShortPathExpirationPeriod { get; set; }
    }

    internal class Diagnostics
    {
        public bool EnableRequestProfiling { get; set; }
        public bool EnableSettingsLogging { get; set; }
        public bool EnableAuditLogging { get; set; }
        public Filelogoutput FileLogOutput { get; set; }
    }

    internal class Filelogoutput
    {
        public string Format { get; set; }
        public string Directory { get; set; }
        public string File { get; set; }
    }

    internal class Cachevalidationservice
    {
        public int ProcessDelayMs { get; set; }
    }

    internal class Retrypolicy
    {
        public string[] RetryPolicyIntervals { get; set; }
    }

    internal class Globalization
    {
        public string DefaultCulture { get; set; }
    }

    internal class Accountmanagement
    {
        public bool AllowAccountRegister { get; set; }
        public bool AllowPhoneNumberUpdates { get; set; }
        public bool AllowEmailAddressUpdates { get; set; }
        public bool AllowUserPhotoUpdates { get; set; }
    }

    internal class Tokenissuer
    {
        public string Issuer { get; set; }
        public string SigningCertificateThumbprint { get; set; }
        public int LifetimeMins { get; set; }
    }

    internal class Useraccounts
    {
        public string DefaultAuthentication { get; set; }
        public string DefaultDomain { get; set; }
        public string DefaultMessagingProvider { get; set; }
        public bool LockoutEnabled { get; set; }
        public string LockoutInterval { get; set; }
        public int MaxFailedAccessAttempts { get; set; }
        public bool UsePersistentCookie { get; set; }
        public bool AllowCyrillicUserNames { get; set; }
        public string PasswordMaxAge { get; set; }
        public bool UseTwoFactorAuthentication { get; set; }
        public string DefaultTwoFactorTokenProvider { get; set; }
        public int TwoFactorTimeStep { get; set; }
        public int TwoFactorValidTime { get; set; }
        public int TwoFactorDigitsNumber { get; set; }
        public string PasswordExpirationPrompt { get; set; }
        public string[] IdentityCredentials { get; set; }
        public bool PreventPasswordReuse { get; set; }
        public int NumberOfPasswordsToRemember { get; set; }
        public int MaxCodeSendingAttempts { get; set; }
        public string CodeSendingLockoutInterval { get; set; }
        public string CodeResendingInterval { get; set; }
    }

    internal class Passwords
    {
        public int RequiredLength { get; set; }
        public int RequiredUniqueChars { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireDigit { get; set; }
    }

    internal class Authentication
    {
        public object[] Providers { get; set; }
    }

    internal class Uservalidation
    {
        public object[] Validators { get; set; }
    }

    internal class Accountenrichment
    {
        public Provider[] Providers { get; set; }
    }

    internal class Provider
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string[] Audiences { get; set; }
        public string ClaimsPrefix { get; set; }
        public Configuration Configuration { get; set; }
    }

    internal class Configuration
    {
        public string IntegrationServiceEndpoint { get; set; }
        public string ServiceUsername { get; set; }
        public string ServiceUserPassword { get; set; }
        public string EssPlatformVersion { get; set; }
    }

}
