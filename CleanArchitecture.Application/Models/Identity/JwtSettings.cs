namespace CleanArchitecture.Application.Models.Identity
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;

        //quien esta enviando el token
        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;

        public double DurationInMinutes { get; set; }
    }
}
