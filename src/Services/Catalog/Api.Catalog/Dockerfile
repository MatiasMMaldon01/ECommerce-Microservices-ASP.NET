#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Catalog/Api.Catalog/Api.Catalog.csproj", "Services/Catalog/Api.Catalog/"]
COPY ["Services/Catalog/Application.Catalog/Application.Catalog.csproj", "Services/Catalog/Application.Catalog/"]
COPY ["Services/Catalog/Domain.Catalog/Domain.Catalog.csproj", "Services/Catalog/Domain.Catalog/"]
COPY ["Services/Catalog/Infraestructure.Catalog/Infraestructure.Catalog.csproj", "Services/Catalog/Infraestructure.Catalog/"]
RUN dotnet restore "Services/Catalog/Api.Catalog/Api.Catalog.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/Api.Catalog"
RUN dotnet build "Api.Catalog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.Catalog.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.Catalog.dll"]