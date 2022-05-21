# 2.Authorization_WebApi

## Beskrivelse
Demonstrerer authentication og authorization af en Xamarin.Forms App vha. Auth0. Ved hjælp af Access Token kan der hentes data fra et WebApi (https://localhost:5000). 
Desuden vises hvordan man anvender en Permission (et scope fra Api kaldet *read:weatherforecast*) til en bruger i Auth0 og hvordan det bliver authorized i WebApi.

Ref: [ASP.NET Core Web API: Authorization](https://auth0.com/docs/quickstart/backend/aspnet-core-webapi/01-authorization)

Ref: [auth0-aspnetcore-webapi-samples/Quickstart/01-Authorization/](https://github.com/auth0-samples/auth0-aspnetcore-webapi-samples/tree/master/Quickstart/01-Authorization)

Eksemplet er baseret på artiklen: [Authentication with Xamarin Forms and Auth0](https://purple.telstra.com/blog/authentication-with-xamarin-forms-and-auth0)

&nbsp;

## Libraries

Der er installeret følgende Nuget pakker i WebApi projektet:

- Microsoft.AspNetCore.Authentication.JwtBearer

&nbsp;

## Oprettelse i Auth0 uden Scope

Det forudsættes at der allerede er oprettet en **Application** hos Auth0, som Xamarin app'en benytter (demonstreret i branch 1.SimpleLogin), samt en **User**.

I Auth0 under *Applications* vælges *Apis* og et nyt Api oprettes. Som Identifier kan man benytte URL'en til WebApi, f.eks. https://localhost:5000/weatherforecast. 

Under fanen *Quick Start* får man serveret en komplet `Startup` klasse med både *Authority* og *Audience* udfyldt. I koden er disse properties dog lagt ned i appsettings.json.

I controlleren kan man nu tilføje `[Authorize]` til controller-klassen eller til enkelte metoder.

&nbsp;


## Debug af applikationen

I Solutions sættes WebApi til at starte først, efterfulgt af enten Android eller iOS projektet.

#### Android Emulator 

Virker out of the box med 10.0.2.2:5000 som WebApi Url (routes automatisk til localhost, som Api kører på)

#### Fysisk Android/iOS Phone og iOS Emulator

WebApi'ets adresse skal ændres til maskinens IP-nummer, fordi localhost jo er selve telefonen. Det sker i filen launchSettings.json i WebApi. Sæt "applicationUrl": "https://192.168.1.23:5000" eller hvad din maskine nu kører på. Men der skal også laves adgang i Firewall'en:

1. Open Windows 'Start' and type WF.msc.
2. Click 'Inbound Rules' on the left.
3. Click 'New Rules' on the right.
4. Choose 'Port' in the new dialog, then 'Next'.
5. Select TCP, and enter the port from the Remote URL next to 'Specific local ports' (5000), then 'Next'.
6. Next, and next (you may want to disable 'Public'), give it a name like 'WebApi'.


