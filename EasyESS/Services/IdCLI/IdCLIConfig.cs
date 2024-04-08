namespace EasyESS.Services.IdCLI
{
    internal class IdCLIConfig
    {
        public Connectionstrings ConnectionStrings { get; set; }
        public Passwordoptions PasswordOptions { get; set; }
        public Useroptions UserOptions { get; set; }
    }

    internal class Connectionstrings
    {
        public string Database { get; set; }
    }

    internal class Passwordoptions
    {
        public int RequiredLength { get; set; }
        public int RequiredUniqueChars { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireDigit { get; set; }
    }

    internal class Useroptions
    {
        public string PasswordMaxAge { get; set; }
        public string DefaultUserPassword { get; set; }
        public string DefaultUserDomain { get; set; }
        public bool UseTwoFactorAuthentication { get; set; }
        public bool AllowCyrillicUserNames { get; set; }
    }

}
