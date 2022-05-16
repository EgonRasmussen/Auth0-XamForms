namespace Auth0XamForms
{
    public static class AuthConfig
    {
        public const string Domain = "https://demo.duendesoftware.com";    // Her indsættes Domain Name fra Auth0
        public const string ClientId = "interactive.public";  // Her indsættes Client ID fra Auth0
        public const string Audience = "https://test";  // Her indsættes Audience fra Auth0
        public const string Scopes = "openid profile email api offline_access";
        public const string RedirectUri = "com.companyname.auth0xamforms://callback";

        public const string WebApi = "https://demo.duendesoftware.com";
    }
}
