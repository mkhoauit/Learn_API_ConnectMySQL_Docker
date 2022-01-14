FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Practice_API_2/Practice_API_2.csproj", "Practice_API_2/"]
RUN dotnet restore "Practice_API_2/Practice_API_2.csproj"
COPY . .
WORKDIR "/src/Practice_API_2"
RUN dotnet build "Practice_API_2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Practice_API_2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Practice_API_2.dll"]
