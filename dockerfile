# Etapa Base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Configura a URL de inicialização do ASP.NET Core
ENV ASPNETCORE_URLS=http://+:8080

# Etapa Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

COPY Domain/Domain.sln ./Domain/
COPY VitalAPI/VitalAPI.csproj ./VitalAPI/
COPY Application/Application.csproj ./Application/
COPY Domain/Domain.csproj ./Domain/
COPY Infraestructure/Infraestructure.csproj ./Infraestructure/

# Restaura as dependências
RUN dotnet restore "/src/Domain/Domain.sln"

# Copia o restante do código do projeto
COPY . .

# Compila o projeto
WORKDIR /src/VitalAPI
RUN dotnet build -c $configuration -o /app/build

# Etapa Publish
FROM build AS publish

# Publica o projeto
RUN dotnet publish -c $configuration -o /app/publish /p:UseAppHost=false

# Etapa Final
FROM base AS final
WORKDIR /app

# Copia os arquivos publicados para o contêiner final
COPY --from=publish /app/publish .

COPY Application/Helpers/calendarcredential.json ./Application/Helpers/
COPY Application/CalendarAuthToken/Google.Apis.Auth.OAuth2.Responses.TokenResponse-user ./Application/CalendarAuthToken/

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "./VitalAPI.dll"]