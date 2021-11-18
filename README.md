# 1.SimpleLogin

## Beskrivelse
Demonstrerer den simpleste måde at benytte Auth0 til at lave login på en Xamarin Form app.

Når brugeren er logget ind, vises en velkomst, ID Token, Access Token og brugerens claims Bemærk at alle data også udskrives til Output vinduet når
man kører i Debug mode. Det giver mulighed for at kopiere Access Token over i f.eks [jwt.io](jwt.io) for nærmere inspektion.

Eksemplet kommer herfra: https://github.com/auth0-samples/auth0-xamarin-oidc-samples/tree/master/Samples/Forms_NS

&nbsp;

## Libraries

Følgende ekstra Nuget pakker er installeret i Core-projektet:

- IdentityModel.OidcClient

Følgende ekstra Nuget pakker er installeret i Android-projektet:

- Auth0.OidcClient.Android
- Xamarin.Android.SupportCustomTabs

&nbsp;

## Oprettelse i Auth0

Opret en ny Application og giv den et navn.

Vælg *Application Type* som **Native**

Under *Quick Start* kan man vælge **Xamarin**, hvor man kan få hjælp til konfigurationen. Her finder man bl.a. hvordan *Callback URL* skal formateres, her vist for Android:

`YOUR_ANDROID_PACKAGE_NAME://YOUR_DOMAIN_NAME/android/YOUR_ANDROID_PACKAGE_NAME/callback`

Man finder YOUR_ANDROID_PACKAGE_NAME i manifestet. YOUR_DOMAIN_NAME bliver automatisk indsat i Quick start.

Der er også en knap DOWNLOAD SAMPLE og VIEW ON GITHUB, men disse eksempler går på native Xamarin og ikke Xamarin.Forms. Benyt lærerens eksempel i stedet for.



