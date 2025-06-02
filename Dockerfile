# Etapa 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .

WORKDIR /app

# Restaura e publica com base no .csproj correto
RUN dotnet restore "app-horarios-BackEnd.csproj"
RUN dotnet publish "app-horarios-BackEnd.csproj" -c Release -o /out

# Etapa 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "app-horarios-BackEnd.dll"]
