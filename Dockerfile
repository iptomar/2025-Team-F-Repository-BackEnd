# Etapa 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo
COPY . .

# Restaura os pacotes
RUN dotnet restore "app-horarios-BackEnd.csproj"

# Publica
RUN dotnet publish "app-horarios-BackEnd.csproj" -c Release -o /app/publish

# Etapa 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "app-horarios-BackEnd.dll"]
