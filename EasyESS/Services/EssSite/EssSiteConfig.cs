namespace EasyESS.Services.EssSite
{

    public class EssSiteConfig
    {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public Connectionstrings ConnectionStrings { get; set; }
        public General General { get; set; }
        public Globalization Globalization { get; set; }
        public Authentication Authentication { get; set; }
        public object[] SupportContacts { get; set; }
        public Clientconfiguration ClientConfiguration { get; set; }
        public Diagnostics Diagnostics { get; set; }
        public Signprovider[] SignProviders { get; set; }
        public Reverseproxy ReverseProxy { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string Yarp { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class Connectionstrings
    {
        public string IdentityService { get; set; }
        public string OfficeService { get; set; }
    }

    public class General
    {
        public bool EmbeddedMode { get; set; }
        public string CompressionLevel { get; set; }
    }

    public class Globalization
    {
        public string DefaultCulture { get; set; }
        public Availablelanguages AvailableLanguages { get; set; }
    }

    public class Availablelanguages
    {
        public string ru { get; set; }
    }

    public class Authentication
    {
        public string Audience { get; set; }
        public string ReturnUrl { get; set; }
        public string TokenIssuer { get; set; }
        public string EncryptionKey { get; set; }
        public string SigningCertificateThumbprint { get; set; }
    }

    public class Clientconfiguration
    {
        public string ServiceEndpoint { get; set; }
        public string ServiceAudience { get; set; }
        public string NotificationsUpdateInterval { get; set; }
        public string WidgetUpdateInterval { get; set; }
        public string RequestTimeout { get; set; }
        public string SignOutEndpoint { get; set; }
        public string Audience { get; set; }
        public string ReturnUrl { get; set; }
        public string ConfirmationEndpoint { get; set; }
        public string ChangePasswordEndpoint { get; set; }
        public string RefreshCodeInterval { get; set; }
        public string ToastAutoHideTimeout { get; set; }
        public string MaxNumberOfAttempts { get; set; }
        public string PreviewProxyPrefix { get; set; }
        public string StorageProxyPrefix { get; set; }
        public string AutoSignOutDelaySecs { get; set; }
        public string SessionIdleTimeoutSecs { get; set; }
        public string MobileAutoSignOutDelaySecs { get; set; }
        public string MobileSessionIdleTimeoutSecs { get; set; }
        public string AvailableLanguages { get; set; }
        public string SupportContacts { get; set; }
    }

    public class Diagnostics
    {
        public bool EnableConfigLogging { get; set; }
        public string HealthCheckTimeout { get; set; }
    }

    public class Reverseproxy
    {
        public bool Enabled { get; set; }
        public Routes Routes { get; set; }
        public Clusters Clusters { get; set; }
    }

    public class Routes
    {
        public Storageservice StorageService { get; set; }
        public Previewstorage PreviewStorage { get; set; }
        public Officeservice OfficeService { get; set; }
    }

    public class Storageservice
    {
        public string ClusterId { get; set; }
        public Match Match { get; set; }
        public Transform[] Transforms { get; set; }
    }

    public class Match
    {
        public string Path { get; set; }
    }

    public class Transform
    {
        public string PathRemovePrefix { get; set; }
        public string RequestHeaderRemove { get; set; }
    }

    public class Previewstorage
    {
        public string ClusterId { get; set; }
        public Match1 Match { get; set; }
        public Transform1[] Transforms { get; set; }
    }

    public class Match1
    {
        public string Path { get; set; }
    }

    public class Transform1
    {
        public string PathRemovePrefix { get; set; }
    }

    public class Officeservice
    {
        public string ClusterId { get; set; }
        public Match2 Match { get; set; }
        public Transform2[] Transforms { get; set; }
    }

    public class Match2
    {
        public string Path { get; set; }
    }

    public class Transform2
    {
        public string PathRemovePrefix { get; set; }
    }

    public class Clusters
    {
        public Storageservice1 StorageService { get; set; }
        public Previewstorage1 PreviewStorage { get; set; }
        public Officeservice1 OfficeService { get; set; }
    }

    public class Storageservice1
    {
        public Destinations Destinations { get; set; }
    }

    public class Destinations
    {
        public Storageservice2 StorageService { get; set; }
    }

    public class Storageservice2
    {
        public string Address { get; set; }
    }

    public class Previewstorage1
    {
        public Destinations1 Destinations { get; set; }
    }

    public class Destinations1
    {
        public Previewstorage2 PreviewStorage { get; set; }
    }

    public class Previewstorage2
    {
        public string Address { get; set; }
    }

    public class Officeservice1
    {
        public Destinations2 Destinations { get; set; }
    }

    public class Destinations2
    {
        public Officeservice2 OfficeService { get; set; }
    }

    public class Officeservice2
    {
        public string Address { get; set; }
    }

    public class Signprovider
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string ClassName { get; set; }
        public string Path { get; set; }
    }

}
