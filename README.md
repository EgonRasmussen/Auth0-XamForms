# 4. Roles

## Beskrivelse
Demonstrerer hvordan man opretter Roles i Auth0 ved at samle Permissions. I Auth0 kan man desuden mappe
Roles ind i Identity Token, som app'en kan benytte til at styre hvad brugeren har adgang til lokalt.
Bem�rk at *IdentityToken** intet har at g�re med hvad applicationen har adgang til i API'et p� vegne af brugeren - dette 
er udelukkende styret af hvilke scopes *AccessToken* kan pr�sentere overfor API'et. 

Se detaljer i ReadMe for branchen *2.Authorization_WebApi*.

&nbsp;

## Oprettelse af Permissions og Roles i Auth0

I Auth0 dashpanel for *Apis* udf�res f�lgende:

#### Settings
- Enable RBAC
- Add Permissions in the Access Token

#### Permissions og Roles
- Under fanen *Permissions* tilf�jes en Permission (Scope) kaldet `read:weatherforecast`, hvis det ikke allerede er sket
- Under *Users | Roles* oprettes rollen `ReadWeatherRole` og tilf�j `read:weatherforecast` permission til rollen
- Under fanen *Users* tilf�jes den �nskede bruger til medlem af rollen `ReadWeatherRole`

Og nu kan man gentage, hvis man �nsker flere roller:
- Opret en Permission kaldet `write:weatherforecast` 
- Opret rollen `ReadWriteWeatherRole` og tilf�j b�de `read:weatherforecast` og `write:weatherforecast` permissions
- Under fanen *Users* tilf�jes den �nskede bruger til medlem af rollen `ReadWriteRole`

&nbsp;

### Map Roles ind i IDtoken

I Auth0's dashpanel v�lges: *Action | Flows | Login*. I h�jre side klikkes p� *Add Action | Custom | Create Action*. Kald den **RolesToIDtokenAction**. 
Kopier script ind herfra: [Add user roles to ID and Access tokens](https://auth0.com/docs/customize/actions/flows-and-triggers/login-flow#add-user-roles-to-id-and-access-tokens)
Inds�t f�lgende som namespace: `http://schemas.microsoft.com/ws/2008/06/identity/claims/role`

```js
exports.onExecutePostLogin = async (event, api) => {
  const namespace = 'http://schemas.microsoft.com/ws/2008/06/identity/claims';
  if (event.authorization) {
    api.idToken.setCustomClaim(`${namespace}/role`, event.authorization.roles);
  }
}
```

V�lg *Deploy*. Tr�k *Action* ind i flowet.

Desv�rre fungerer metoden `IsInRole(string rolename)` ikke i Xamarin Forms, s� her skal man selv lave
noget logik, der s�ger efter om den p�g�ldende rolle findes som en claim i IdentityToken. Et simpelt
eksempel er vist i *LoginViewModel* i eksemplet.

&nbsp;






