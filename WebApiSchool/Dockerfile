FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["StudentTest/StudentTest.csproj", "StudentTest/"]
RUN dotnet restore "StudentTest/StudentTest.csproj"
COPY . .
WORKDIR "/src/StudentTest"
RUN dotnet build "StudentTest.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "StudentTest.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "StudentTest.dll"]