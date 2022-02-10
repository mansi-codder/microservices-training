#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG SDK_BASE_IMAGE
ARG SDK_BASE_IMAGE=${SDK_BASE_IMAGE:-artifacts.cf.saxo/docker/base/saxo-dotnet-sdk:5.0-alpine-ci-1.0.39}

ARG RUNTIME_BASE_IMAGE
ARG RUNTIME_BASE_IMAGE=${RUNTIME_BASE_IMAGE:-artifacts.cf.saxo/docker/base/saxo-dotnet-aspnet:5.0-alpine-ci-1.0.39}


# Use SDK image for building - Saxo provided
FROM $SDK_BASE_IMAGE AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM $RUNTIME_BASE_IMAGE AS build
WORKDIR /src
COPY ["Services/Catalog/Catalog.API/Catalog.API.csproj", "Services/Catalog/Catalog.API/"]
RUN dotnet restore "Services/Catalog/Catalog.API/Catalog.API.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/Catalog.API"
RUN dotnet build "Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
