

# Solution Auth0XamForms

#### Beskrivelse
Demonstrerer hvordan en Xamarin Forms App (Default Flyout Template) kan vise data (WeatherForcast) hentet fra et ASP.NET Api (default template) med Auth0 som Identity provider.

Eksemplet er baseret på artiklen: [Authentication with Xamarin Forms and Auth0](https://purple.telstra.com/blog/authentication-with-xamarin-forms-and-auth0)

#### Bemærk

Testet med Android 10 API 29.

Conveyor by Keyoti benyttes til at give mobilen/emulatoren adgang til webApi, som jo kører på Localhost. Husk at rette IP/portnummer til den benyttede i WeatherForecastViewModel.cs.

Ved Multiple Startup konfigurationen er det bedst at lade Android starte op inden WebApi starter op???

