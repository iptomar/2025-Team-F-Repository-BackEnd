# Etapa 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia todos os arquivos para dentro do container
COPY . .

# Vai para a pasta correta com nome EXATO
WORKDIR /app/App-horarios-BackEnd

# Restaura os pacotes do projeto
RUN dotnet restore "app-horarios-BackEnd.csproj"

# Publica a aplicação
RUN dotnet publish "app-horarios-BackEnd.csproj" -c Release -o /out

# Etapa 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "App-horarios-BackEnd.dll"]
