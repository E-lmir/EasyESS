using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyESS.SignService
{
    public class SignServiceConfig
    {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public Connectionstrings ConnectionStrings { get; set; }
        public Authentication Authentication { get; set; }
        public Secrets Secrets { get; set; }
        public Cryptopro CryptoPro { get; set; }
        public Cloudsigningservice CloudSigningService { get; set; }
        public Konturproxy KonturProxy { get; set; }
        public Clientratelimiting ClientRateLimiting { get; set; }
        public Clientratelimitpolicies ClientRateLimitPolicies { get; set; }
        public Testchecks TestChecks { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
    }

    public class Connectionstrings
    {
        public string IdentityService { get; set; }
        public string StorageService { get; set; }
        public string CloudSignService { get; set; }
        public string CloudAuthService { get; set; }
    }

    public class Authentication
    {
        public string Audience { get; set; }
        public Trustedissuer[] TrustedIssuers { get; set; }
    }

    public class Trustedissuer
    {
        public string Issuer { get; set; }
        public string EncryptionKey { get; set; }
        public string SigningCertificateThumbprint { get; set; }
        public string SigningCertificatePath { get; set; }
    }

    public class Secrets
    {
        public string IdentityServiceUserPassword { get; set; }
    }

    public class Cryptopro
    {
        public string ClientId { get; set; }
        public string SignServiceName { get; set; }
        public int CaId { get; set; }
        public string CertificateTemplate { get; set; }
        public string OperatorLogin { get; set; }
        public string OperatorPassword { get; set; }
        public string HealthCheckUrl { get; set; }
        public string Name { get; set; }
        public string[] IdentificationTypes { get; set; }
        public string ProviderId { get; set; }
    }

    public class Cloudsigningservice
    {
        public string ClientId { get; set; }
        public string SignServiceName { get; set; }
        public int CaId { get; set; }
        public string CertificateTemplate { get; set; }
        public string OperatorLogin { get; set; }
        public string OperatorPassword { get; set; }
        public string Database { get; set; }
        public string StatementTemplateFile { get; set; }
        public bool DisableCertificateIssueStatement { get; set; }
        public string Name { get; set; }
        public string[] IdentificationTypes { get; set; }
        public string ProviderId { get; set; }
        public string ConfirmationMessageTemplate { get; set; }
        public string DocumentServiceConnectionString { get; set; }
        public string MessageBrokerConnectionString { get; set; }
    }

    public class Konturproxy
    {
        public string Name { get; set; }
        public string[] IdentificationTypes { get; set; }
        public string ProviderId { get; set; }
        public string TenantId { get; set; }
    }

    public class Clientratelimiting
    {
        public bool EnableEndpointRateLimiting { get; set; }
        public bool StackBlockedRequests { get; set; }
        public int HttpStatusCode { get; set; }
        public object ClientIdHeader { get; set; }
        public bool DisableRateLimitHeaders { get; set; }
        public string[] EndpointWhitelist { get; set; }
        public Generalrule[] GeneralRules { get; set; }
    }

    public class Generalrule
    {
        public string Endpoint { get; set; }
        public string Period { get; set; }
        public int Limit { get; set; }
    }

    public class Clientratelimitpolicies
    {
        public object[] ClientRules { get; set; }
    }

    public class Testchecks
    {
        public Check[] Checks { get; set; }
    }

    public class Check
    {
        public string name { get; set; }
        public string type { get; set; }
        public Configuration configuration { get; set; }
    }

    public class Configuration
    {
        public string[] ValidatingFiles { get; set; }
        public Expectedvalues ExpectedValues { get; set; }
        public string configFileName { get; set; }
        public string[] RequiredAuthentications { get; set; }
        public string healthEndpoint { get; set; }
    }

    public class Expectedvalues
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
