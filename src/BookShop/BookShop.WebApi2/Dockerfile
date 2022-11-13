#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BookShop.WebApi2/BookShop.WebApi2.csproj", "BookShop.WebApi2/"]
RUN dotnet restore "BookShop.WebApi2/BookShop.WebApi2.csproj"
COPY . .
WORKDIR "/src/BookShop.WebApi2"
RUN dotnet build "BookShop.WebApi2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookShop.WebApi2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookShop.WebApi2.dll"]