FROM mcr.microsoft.com/dotnet/sdk:7.0

ARG NUGETORG_API_KEY
ENV NUGETORG_API_KEY=${NUGETORG_API_KEY}

# Install DotNet 6.0 SDK for backwards compatibale libraries
COPY --from=mcr.microsoft.com/dotnet/sdk:6.0 /usr/share/dotnet/shared /usr/share/dotnet/shared

COPY . .

WORKDIR .