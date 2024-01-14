FROM mcr.microsoft.com/dotnet/sdk:7.0 AS builder 

WORKDIR /Application

COPY . .

ENV ASPNETCORE_ENVIRONMENT=Development

RUN dotnet dev-certs https --trust

RUN dotnet restore

RUN dotnet publish -c Release -o output

FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /StreamingPlatformService

COPY --from=builder /Application/output .

ENTRYPOINT ["dotnet", "StreamingPlatformService.dll"]