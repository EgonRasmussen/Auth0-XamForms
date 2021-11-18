# 2.Authorization_WebApi

## Beskrivelse
Demonstrerer authentication og authorization af en Xamarin.Forms App vha. Auth0. Ved hj�lp af Access Token kan der hentes data fra et WebApi (https://localhost:5000). 
Desuden vises hvordan man anvender en Permission (et scope fra Api kaldet *read:weatherforecast*) til en bruger i Auth0 og hvordan det bliver authorized i WebApi.

Ref: [ASP.NET Core Web API: Authorization](https://auth0.com/docs/quickstart/backend/aspnet-core-webapi/01-authorization)

Ref: [auth0-aspnetcore-webapi-samples/Quickstart/01-Authorization/](https://github.com/auth0-samples/auth0-aspnetcore-webapi-samples/tree/master/Quickstart/01-Authorization)

Eksemplet er baseret p� artiklen: [Authentication with Xamarin Forms and Auth0](https://purple.telstra.com/blog/authentication-with-xamarin-forms-and-auth0)

&nbsp;

## Libraries

Der er installeret f�lgende Nuget pakker i WebApi projektet:

- Microsoft.AspNetCore.Authentication.JwtBearer

&nbsp;

## Oprettelse i Auth0 uden Scope

Det foruds�ttes at der allerede er oprettet en **Application** hos Auth0, som Xamarin app'en benytter (demonstreret i branch 1.SimpleLogin), samt en **User**.

I Auth0 under *Applications* v�lges *Apis* og et nyt Api oprettes. Som Identifier kan man benytte URL'en til WebApi, f.eks. https://localhost:5000/weatherforecast. 

Under fanen *Quick Start* f�r man serveret en komplet `Startup` klasse med b�de *Authority* og *Audience* udfyldt. I koden er disse properties dog lagt ned i appsettings.json.

I controlleren kan man nu tilf�je `[Authorize]` til controller-klassen eller til enkelte metoder.

&nbsp;

## Oprettelse i Auth0 med Scope

I konfigurationen af Api hos Auth0, skal man enable *RBAC* samt *Add Permissions in the Access token*.

Desuden skal man ind under fanen *Permissions*. Skriv et navn p� *Permission (Scope)*, f.eks. `read:weatherforecast` og en evt. beskrivelse. Tryk *Add*.

&nbsp;

## Debug af applikationen

I Solutions s�ttes WebApi til at starte f�rst, efterfulgt af Android App'en.




