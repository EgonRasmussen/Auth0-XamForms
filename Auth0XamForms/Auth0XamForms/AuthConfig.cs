﻿namespace Auth0XamForms.Auth;

public static class AuthConfig
{
    public const string Domain = "https://eucsyd.eu.auth0.com";    // Her indsættes Domain Name fra Auth0
    public const string ClientId = "SamcgMljPCrHhr0sFIwj5w8zC7Xs8I69";  // Her indsættes Client ID fra Auth0
    public const string Audience = "https://localhost:5000/weatherforecast";  // Her indsættes Audience fra Auth0
    public const string Scopes = "openid profile read:weatherforecast";
    public const string RedirectUri = "com.companyname.auth0xamforms://eucsyd.eu.auth0.com/android/com.companyname.auth0xamforms/callback";

    // WebApi
    public const string BaseUrl = "https://10.0.2.2:5000";
}
