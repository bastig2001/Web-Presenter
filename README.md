# Web-Presenter

This is an app for holding presentations online.

We use: 
* [ASP.NET](https://github.com/dotnet/aspnetcore) Core and [C#](https://github.com/dotnet/csharplang) 
for server-side code
* [Angular](https://github.com/angular/angular) and [TypeScript](https://github.com/microsoft/TypeScript) 
for client-side code
* [Bootstrap](https://github.com/twbs/bootstrap) 
for layout and styling
* [Postgresql](https://www.postgresql.org/) as database


## Docker
You can get a docker image of the master branch 
[here](https://hub.docker.com/repository/docker/sebastian2410/web-presenter).

You can check out docker-compose.yml for an example usage.

### Environment Variables
You can change certain settings with environment variables.

`ASPNETCORE_URLS` sets the url the app listens to, following the pattern `{scheme}://{address}:{port}`.

`ASPNETCORE_ENVIRONMENT` sets the environment mode of the app (`Development`, `Staging`, `Production`).
If not set, the app starts in production mode.

Other ASP.NET Core and .Net environment variables can be used as well.

`DB__Server` sets the server for the Postgres database. It defaults to `localhost`.

`DB__Port` sets the port the database can be reached on. It defaults to `5432` (Default Port for Postgres).

`DB__Database` sets the name of the database to be used. It defaults to `WebPresenter`.

`DB__User` sets the username for the app shall use to enter the database. It defaults to `postgres`

`DB__Password` sets the password for the username to use. It defaults to `wasistpasstiert`.
