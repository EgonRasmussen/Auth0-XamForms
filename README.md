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


## Permissions og Roles

I Auth0 dashpanel for WebAPI udføres følgende:

#### Settings
- Enable RBAC
- Add Permissions in the Access Token

#### Permissions og Roles
- Opret en Permission kaldet `todo:read` 
- Opret rollen `ReadOnlyRole` og tilføj `todo:read` permission 
- Gør brugeren ecr@live.dk til medlem af rollen `ReadOnlyRole`
- 
- Opret en Permission kaldet `todo:write` 
- Opret rollen `ReadWriteRole` og tilføj både `todo:read` og `todo:write` permissions
- Gør brugeren ecr@live.dk til medlem af rollen `ReadWriteRole`

&nbsp;

### Map Roles ind i IDtoken

I Auth0's dashpanel vælges: *Action | Flows | Login*. I højre side klikkes på *Add Action | Custom | Create Action*. Kald den **RolesToIDtokenAction**. 
Kopier script ind herfra: [Add user roles to ID and Access tokens](https://auth0.com/docs/customize/actions/flows-and-triggers/login-flow#add-user-roles-to-id-and-access-tokens)
Indsæt følgende som namespace: http://schemas.microsoft.com/ws/2008/06/identity/claims/role

```js
exports.onExecutePostLogin = async (event, api) => {
  const namespace = 'http://schemas.microsoft.com/ws/2008/06/identity/claims';
  if (event.authorization) {
    api.idToken.setCustomClaim(`${namespace}/role`, event.authorization.roles);
  }
}
```

Deploy. Træk Action ind i flowet.

Desværre fungerer metoden `IsInRole(string rolename)` ikke i Xamarin Forms, så her skal man selv lave
noget logik, der søger efter om den pågældende rolle findes som en claim i IdentityToken. Et simpelt
eksempel er vist i LoginViewModel i eksemplet.

&nbsp;

## Debug af applikationen

I Solutions sættes WebApi til at starte først, efterfulgt af Android App'en.




