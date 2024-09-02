# Set the base image as the .NET 7.0 SDK (this includes the runtime)
FROM mcr.microsoft.com/dotnet/sdk:6.0@sha256:d32bd65cf5843f413e81f5d917057c82da99737cb1637e905a1a4bc2e7ec6c8d as build-env

# Copy everything and publish the release (publish implicitly restores and builds)
WORKDIR /app
COPY *.csproj ./
# RUN dotnet restore

# Copy the rest of the application code to the container
COPY . ./
RUN dotnet publish -c Release -o out

# Relayer the .NET SDK, anew with the build output
FROM mcr.microsoft.com/dotnet/sdk:6.0@sha256:d32bd65cf5843f413e81f5d917057c82da99737cb1637e905a1a4bc2e7ec6c8d
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "/API.dll" ]
