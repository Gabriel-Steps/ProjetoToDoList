# Etapa 1: build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore BackendToDoList.sln
RUN dotnet publish BackendToDoList.Api/BackendToDoList.Api.csproj -c Release -o /app/publish

# Etapa 2: imagem runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "BackendToDoList.Api.dll"]
