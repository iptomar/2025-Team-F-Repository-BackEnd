# Etapa 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia o arquivo da solução (caso exista)
COPY *.sln .

# Copia o projeto ASP.NET
COPY app-horarios-BackEnd/*.csproj ./app-horarios-BackEnd/
RUN dotnet restore ./app-horarios-BackEnd/app-horarios-BackEnd.csproj

# Copia o restante do código
COPY . .

WORKDIR /app/app-horarios-BackEnd
RUN dotnet publish -c Release -o /out

# Etapa 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Comando para iniciar a API
ENTRYPOINT ["dotnet", "app-horarios-BackEnd.dll"