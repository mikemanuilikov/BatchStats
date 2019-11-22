FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["BatchStats.Api/BatchStats.Api.csproj", "BatchStats.Api/"]
COPY ["BatchStats.Core/BatchStats.Core.csproj", "BatchStats.Core/"]
COPY ["BatchStats.Models/BatchStats.Models.csproj", "BatchStats.Models/"]
RUN dotnet restore "BatchStats.Api/BatchStats.Api.csproj"
COPY . .
WORKDIR "/src/BatchStats.Api"
RUN dotnet build "BatchStats.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BatchStats.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BatchStats.Api.dll"]