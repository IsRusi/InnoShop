FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "InnoShop.UserManager/InnoShop.UserManager.WebAPI/InnoShop.UserManager.WebAPI.csproj"
RUN dotnet publish "InnoShop.UserManager/InnoShop.UserManager.WebAPI/InnoShop.UserManager.WebAPI.csproj" -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

COPY --from=build /out .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "InnoShop.UserManager.WebAPI.dll"]