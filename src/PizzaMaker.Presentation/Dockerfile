FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PizzaMaker.Presentation/PizzaMaker.Presentation.csproj", "src/PizzaMaker.Presentation/"]
RUN dotnet restore "src/PizzaMaker.Presentation/PizzaMaker.Presentation.csproj"
COPY . .
WORKDIR "/src/src/PizzaMaker.Presentation"
RUN dotnet build "PizzaMaker.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PizzaMaker.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PizzaMaker.Presentation.dll"]
