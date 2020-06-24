#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["EducationSystem.GroupManagement.Api/EducationSystem.GroupManagement.Api.csproj", "EducationSystem.GroupManagement.Api/"]
COPY ["EducationSystem.StudentManagement.Infrastructure/EducationSystem.StudentManagement.Infrastructure.csproj", "EducationSystem.StudentManagement.Infrastructure/"]
COPY ["EducationSystem.Common.ValueObjects/EducationSystem.Common.ValueObjects.csproj", "EducationSystem.Common.ValueObjects/"]
COPY ["EducationSystem.Common.Utils/EducationSystem.Common.Utils.csproj", "EducationSystem.Common.Utils/"]
COPY ["EducationSystem.Common.Abstractions/EducationSystem.Common.Abstractions.csproj", "EducationSystem.Common.Abstractions/"]
COPY ["EducationSystem.StudentManagement.Core/EducationSystem.StudentManagement.Core.csproj", "EducationSystem.StudentManagement.Core/"]
COPY ["EducationSystem.Common.Contracts/EducationSystem.Common.Contracts.csproj", "EducationSystem.Common.Contracts/"]
RUN dotnet restore "EducationSystem.GroupManagement.Api/EducationSystem.GroupManagement.Api.csproj"
COPY . .
WORKDIR "/src/EducationSystem.GroupManagement.Api"
RUN dotnet build "EducationSystem.GroupManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EducationSystem.GroupManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EducationSystem.GroupManagement.Api.dll"]