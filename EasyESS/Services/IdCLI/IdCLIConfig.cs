namespace EasyESS.Services.IdCLI
{
    public class IdCLIConfig
    {
        public Connectionstrings ConnectionStrings { get; set; }
        public Passwordoptions PasswordOptions { get; set; }
        public Useroptions UserOptions { get; set; }
    }

    public class Connectionstrings
    {
        public string Database { get; set; }
    }

    public class Passwordoptions
    {
        public int RequiredLength { get; set; }
        public int RequiredUniqueChars { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireDigit { get; set; }
    }

    public class Useroptions
    {
        public string PasswordMaxAge { get; set; }
        public string DefaultUserPassword { get; set; }
        public string DefaultUserDomain { get; set; }
        public bool UseTwoFactorAuthentication { get; set; }
        public bool AllowCyrillicUserNames { get; set; }
    }

}
