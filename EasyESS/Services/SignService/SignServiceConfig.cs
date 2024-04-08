namespace EasyESS.Services.SignService
{
    internal class SignServiceConfig
    {
        internal Logging Logging { get; set; }
        internal string AllowedHosts { get; set; }
        internal Connectionstrings ConnectionStrings { get; set; }
        internal Authentication Authentication { get; set; }
        internal Secrets Secrets { get; set; }
        internal Cryptopro CryptoPro { get; set; }
        internal Cloudsigningservice CloudSigningService { get; set; }
        internal Konturproxy KonturProxy { get; set; }
        internal Clientratelimiting ClientRateLimiting { get; set; }
        internal Clientratelimitpolicies ClientRateLimitPolicies { get; set; }
        internal Testchecks TestChecks { get; set; }
    }

    internal class Logging
    {
        internal Loglevel LogLevel { get; set; }
    }

    internal class Loglevel
    {
        internal string Default { get; set; }
        internal string Microsoft { get; set; }
    }

    internal class Connectionstrings
    {
        internal string IdentityService { get; set; }
        internal string StorageService { get; set; }
        internal string CloudSignService { get; set; }
        internal string CloudAuthService { get; set; }
    }

    internal class Authentication
    {
        internal string Audience { get; set; }
        internal Trustedissuer[] TrustedIssuers { get; set; }
    }

    internal class Trustedissuer
    {
        internal string Issuer { get; set; }
        internal string EncryptionKey { get; set; }
        internal string SigningCertificateThumbprint { get; set; }
        internal string SigningCertificatePath { get; set; }
    }

    internal class Secrets
    {
        internal string IdentityServiceUserPassword { get; set; }
    }

    internal class Cryptopro
    {
        internal string ClientId { get; set; }
        internal string SignServiceName { get; set; }
        internal int CaId { get; set; }
        internal string CertificateTemplate { get; set; }
        internal string OperatorLogin { get; set; }
        internal string OperatorPassword { get; set; }
        internal string HealthCheckUrl { get; set; }
        internal string Name { get; set; }
        internal string[] IdentificationTypes { get; set; }
        internal string ProviderId { get; set; }
    }

    internal class Cloudsigningservice
    {
        internal string ClientId { get; set; }
        internal string SignServiceName { get; set; }
        internal int CaId { get; set; }
        internal string CertificateTemplate { get; set; }
        internal string OperatorLogin { get; set; }
        internal string OperatorPassword { get; set; }
        internal string Database { get; set; }
        internal string StatementTemplateFile { get; set; }
        internal bool DisableCertificateIssueStatement { get; set; }
        internal string Name { get; set; }
        internal string[] IdentificationTypes { get; set; }
        internal string ProviderId { get; set; }
        internal string ConfirmationMessageTemplate { get; set; }
        internal string DocumentServiceConnectionString { get; set; }
        internal string MessageBrokerConnectionString { get; set; }
    }

    internal class Konturproxy
    {
        public string Name { get; set; }
        public string[] IdentificationTypes { get; set; }
        public string ProviderId { get; set; }
        public string TenantId { get; set; }
    }

    internal class Clientratelimiting
    {
        public bool EnableEndpointRateLimiting { get; set; }
        public bool StackBlockedRequests { get; set; }
        public int HttpStatusCode { get; set; }
        public object ClientIdHeader { get; set; }
        public bool DisableRateLimitHeaders { get; set; }
        public string[] EndpointWhitelist { get; set; }
        public Generalrule[] GeneralRules { get; set; }
    }

    internal class Generalrule
    {
        public string Endpoint { get; set; }
        public string Period { get; set; }
        public int Limit { get; set; }
    }

    internal class Clientratelimitpolicies
    {
        public object[] ClientRules { get; set; }
    }

    internal class Testchecks
    {
        public Check[] Checks { get; set; }
    }

    internal class Check
    {
        public string name { get; set; }
        public string type { get; set; }
        public Configuration configuration { get; set; }
    }

    internal class Configuration
    {
        public string[] ValidatingFiles { get; set; }
        public Expectedvalues ExpectedValues { get; set; }
        public string configFileName { get; set; }
        public string[] RequiredAuthentications { get; set; }
        public string healthEndpoint { get; set; }
    }

    internal class Expectedvalues
    {
        public string IdsAuthDataLogin { get; set; }
        public string IdsAuthDataPassword { get; set; }
        public string InitializationCloudSigningProviderType { get; set; }
        public string LocationsStorageService { get; set; }
        public string LocationsIdentificationService { get; set; }
        public string LocationsCryptoProSignService { get; set; }
        public string LocationsCryptoProAuthService { get; set; }
        public string CryptoProClientId { get; set; }
        public string CryptoProSignServiceName { get; set; }
        public string CryptoProOperatorLogin { get; set; }
        public string CryptoProOperatorPassword { get; set; }
        public string CryptoProHealthCheckUrl { get; set; }
        public string CloudSigningServiceClientId { get; set; }
        public string CloudSigningServiceSignServiceName { get; set; }
        public string CloudSigningServiceOperatorLogin { get; set; }
        public string CloudSigningServiceOperatorPassword { get; set; }
    }

}
