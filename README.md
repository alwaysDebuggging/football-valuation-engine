# Football Valuation Engine
A C#/.NET project that estimates a football player's market value from their real match statistics, built with Clean Architecture.

Pulls a player's stats (appearances, minutes, goals, passes, tackles, duels, etc.) from [API-Football](https://www.api-football.com/) and eventually, turns them into a market value estimate based on position-specific formulas a forward's value leans on goals and shots, a defender's on tackles, interceptions, and duels won, and so on.

## Getting started
 
```bash
git clone https://github.com/<your-username>/football-valuation-engine.git
cd football-valuation-engine
dotnet restore
dotnet build
dotnet test
```
 
You'll need an API-Football key to eventually run the data-fetching parts (free tier works fine for development capped at 100 requests/day). Once the Infrastructure layer is in place, it'll be configured with .NET User Secrets locally:
 
```bash
cd src/FootballValuationEngine.Api
dotnet user-secrets set "FootballApi:ApiKey" "your-key-here"
```

## Status
 
Early days.
