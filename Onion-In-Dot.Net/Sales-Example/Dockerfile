#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sales-Example/Sales-Example.csproj", "Sales-Example/"]
RUN dotnet restore "Sales-Example/Sales-Example.csproj"
COPY . .
WORKDIR "/src/Sales-Example"
RUN dotnet build "Sales-Example.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sales-Example.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sales-Example.dll"]