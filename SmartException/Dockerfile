FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SmartException/SmartException.csproj", "SmartException/"]
RUN dotnet restore "SmartException/SmartException.csproj"
COPY . .
WORKDIR "/src/SmartException"
RUN dotnet build "SmartException.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartException.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartException.dll"]
