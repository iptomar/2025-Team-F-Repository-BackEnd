# Etapa 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia apenas a pasta do projeto backend
COPY App-horarios-BackEnd/ ./App-horarios-BackEnd/

# Restaura pacotes
WORKDIR /app/App-horarios-BackEnd
RUN dotnet restore "app-horarios-BackEnd.csproj"

# Publica
RUN dotnet publish "app-horarios-BackEnd.csproj" -c Release -o /out

# Etapa 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "App-horarios-BackEnd.dll"]
