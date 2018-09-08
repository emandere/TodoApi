FROM microsoft/dotnet:sdk AS build-env
WORKDIR /TodoApi

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /TodoApi
COPY --from=build-env /TodoApi/out .
ENTRYPOINT ["dotnet", "TodoApi.dll"]