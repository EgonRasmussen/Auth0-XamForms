namespace Auth0XamForms.Auth;

public static class AuthConfig
{
    public const string Domain = "eucsyd.eu.auth0.com";    // Her indsættes Domain name fra Auth0
    public const string ClientId = "SamcgMljPCrHhr0sFIwj5w8zC7Xs8I69";  // Her indsættes Client ID fra Auth0 (Application)
    public const string Audience = "https://localhost:5000/weatherforecast";  // Her indsættes Audience fra Auth0 (API)
    public const string Scopes = "openid profile read:weatherforecast"; // Her indsættes de ekstra scopes fra Permissions fra Auth0 (API)
    public const string PackageName = "com.companyname.auth0xamforms";  // Her indsættes <package> fra AndroidManifestet

    // WebApi
    public const string BaseUrl = "https://10.0.2.2:5000";
}
