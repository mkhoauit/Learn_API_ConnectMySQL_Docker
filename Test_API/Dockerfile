FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Test_API/Test_API.csproj", "Test_API/"]
RUN dotnet restore "Test_API/Test_API.csproj"
COPY . .
WORKDIR "/src/Test_API"
RUN dotnet build "Test_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Test_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Test_API.dll"]
