FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Add dependencies
RUN apk update
RUN apk add npm

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /app

# Add dependencies
RUN apk update
RUN apk add libgdiplus-dev --repository http://dl-3.alpinelinux.org/alpine/edge/testing/

COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "WebPresenter.dll"]
