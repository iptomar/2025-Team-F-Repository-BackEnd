# Etapa 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia tudo
COPY . .

# Restaura pacotes
RUN dotnet restore "app-horarios-BackEnd.csproj"

# Publica
RUN dotnet publish "app-horarios-BackEnd.csproj" -c Release -o /out

# Etapa 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "App-horarios-BackEnd.dll"]
