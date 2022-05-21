namespace Auth0XamForms.Auth;

public static class AuthConfig
{
    public const string Domain = "";    // Her indsættes Domain name fra Auth0
    public const string ClientId = "";  // Her indsættes Client ID fra Auth0 (Application)
    public const string Audience = "https://localhost:5000/weatherforecast";  // Her indsættes Audience fra Auth0 (API)
    public const string Scopes = "openid profile read:weatherforecast"; // Her indsættes de ekstra scopes fra Permissions fra Auth0 (API)
    public const string PackageName = "com.companyname.auth0xamforms";  // Her indsættes <package> fra AndroidManifestet eller BundleIdentifier fra Info.plist (iOS)
    public const string CallbackUrl = $"{PackageName}://{Domain}/android/{PackageName}/callback";
    public const string PostLogoutRedirectUri = $"{PackageName}://{Domain}/android/{PackageName}/callback"; // Anvendes ved Logout

    // WebApi
    public const string BaseUrl = "https://10.0.2.2:5000";  // Benyttes med Emulator
    //public const string BaseUrl = "https://192.168.1.23:45520";   // Benyttes med Device i forbindelse med Conveyor by Keyoti (tilret portnummer)
}
