# Etapa 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia todo o conteúdo do repositório
COPY . .

# Entra no diretório do projeto
WORKDIR /app/App_horarios_BackEnd

# Restaura e publica

RUN dotnet restore "App_horarios_BackEnd.csproj"
RUN dotnet publish "App_horarios_BackEnd.csproj" -c Release -o /out


# Etapa 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "App-horarios-BackEnd.dll"]
