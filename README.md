# 2.Authorization_WebApi

## Beskrivelse
Demonstrerer authentication og authorization af en Xamarin.Forms App vha. **Auth0**. Ved hj�lp af Access Token kan der hentes data (WeatherForecast) fra et WebApi (https://localhost:5000). 
Desuden vises hvordan man anvender *permissions* (et scope fra Api kaldet *read:weatherforecast*) til en bruger i Auth0 og hvordan det bliver authorized i WebApi.

Ref: [ASP.NET Core Web API: Authorization](https://auth0.com/docs/quickstart/backend/aspnet-core-webapi/01-authorization)

Ref: [auth0-aspnetcore-webapi-samples/Quickstart/01-Authorization/](https://github.com/auth0-samples/auth0-aspnetcore-webapi-samples/tree/master/Quickstart/01-Authorization)

Eksemplet er baseret p� artiklen: [Authentication with Xamarin Forms and Auth0](https://purple.telstra.com/blog/authentication-with-xamarin-forms-and-auth0)

&nbsp;

## Libraries

#### Core projektet

Tilf�j Nuget pakken:
- IdentityModel.OidcClient, version 5.0.0

&nbsp;

#### Android projektet

Tilf�j Nuget pakken: 
- Plugin.CurrentActivity, version 2.1.0.4

Tilf�j`LaunchMode = LaunchMode.SingleTask` til MainActivity attributes. 

Tilf�j f�lgende til OnCreate(): `DependencyService.Register<ChromeCustomTabsBrowser>();` og
`CrossCurrentActivity.Current.Init(this, savedInstanceState);`

Tilf�j klassen `ChromeCustomTabsBrowser`, der implementerer `IBrowser`

Tilf�j klassen `OidcCallbackActivity`, der nedarver fra `Activity`. Husk at tilrette *DataScheme* s� det passer med `package` fra manifestet!

&nbsp;

#### iOS projektet

Tilf�j Nuget pakkerne:
- System.Text.Encodings.Web, version 6.0.0
- IdentityModel.OidcClient, version 5.0.0

Tilf�j klassen `ASWebAuthenticationSessionBrowser`, der implementerer `IBrowser`

Tilf�j f�lgende til AppDelegatee.cs:  `DependencyService.Register<ASWebAuthenticationSessionBrowser>();`

I Info.plist tilf�jes f�lgende i slutningen af `</dict>`, hvor package navnet fra manifestet benyttes:

```xml
<key>CFBundleURLTypes</key>
<array>
	<dict>
		<key>CFBundleURLSchemes</key>
		<array>
			<string>auth0xamforms</string>
		</array>
	</dict>
</array>
```

&nbsp;

#### Api projektet

Der er installeret f�lgende Nuget pakker i Api projektet:

- Microsoft.AspNetCore.Authentication.JwtBearer

&nbsp;

## Oprettelse i Auth0 uden Scope

Det foruds�ttes at der allerede er oprettet en **Application** hos Auth0, som Xamarin app'en benytter (demonstreret i branch 1.SimpleLogin), samt en **User**.

I Auth0 under *Applications* v�lges *Apis* og et nyt **Api** oprettes. Som *Identifier* kan man benytte URL'en til WebApi, f.eks. https://localhost:5000/weatherforecast. 

Under fanen *Quick Start* f�r man serveret en komplet `Startup` klasse med b�de *Authority* og *Audience* udfyldt. I koden er disse properties dog lagt ned i *appsettings.json*.

I controlleren kan man nu tilf�je `[Authorize]` til controller-klassen eller til enkelte metoder.

&nbsp;


## Debug af applikationen

I Solutions s�ttes *Api* til at starte f�rst, efterfulgt af enten *Android* eller *iOS* projektet.

#### Android Emulator 

Virker out of the box med `10.0.2.2:5000` som Api Url (routes automatisk til localhost, som Api k�rer p�)

#### Fysisk Android/iOS Phone og iOS Emulator

Api'ets adresse skal �ndres til maskinens IP-nummer, fordi *localhost* jo er selve telefonen. Det sker i filen *launchSettings.json* i *Api*. S�t `"applicationUrl": "https://192.168.1.23:5000"` eller hvad din maskine nu k�rer p�. Men der skal ogs� laves adgang i Firewall'en:

1. Open Windows 'Start' and type WF.msc.
2. Click 'Inbound Rules' on the left.
3. Click 'New Rules' on the right.
4. Choose 'Port' in the new dialog, then 'Next'.
5. Select TCP, and enter the port from the Remote URL next to 'Specific local ports' (5000), then 'Next'.
6. Next, and next (you may want to disable 'Public'), give it a name like 'WebApi'.


