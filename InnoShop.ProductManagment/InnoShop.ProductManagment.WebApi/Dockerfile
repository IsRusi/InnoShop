FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "InnoShop.ProductManagment/InnoShop.ProductManagment.WebApi/InnoShop.ProductManagment.WebApi.csproj"
RUN dotnet publish "InnoShop.ProductManagment/InnoShop.ProductManagment.WebApi/InnoShop.ProductManagment.WebApi.csproj" -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "InnoShop.ProductManagment.WebApi.dll"]