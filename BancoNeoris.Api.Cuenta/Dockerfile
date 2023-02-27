#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["BancoNeoris.Api.Cuenta/BancoNeoris.Api.Cuenta.csproj", "BancoNeoris.Api.Cuenta/"]
RUN dotnet restore "BancoNeoris.Api.Cuenta/BancoNeoris.Api.Cuenta.csproj"
COPY . .
WORKDIR "/src/BancoNeoris.Api.Cuenta"
RUN dotnet build "BancoNeoris.Api.Cuenta.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BancoNeoris.Api.Cuenta.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BancoNeoris.Api.Cuenta.dll"]