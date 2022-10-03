# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
# COPY *.sln .
COPY QnA/. ./QnA/
COPY QnA.BAL/. ./QnA.BAL/
COPY Qna.DAL/. ./Qna.DAL/
COPY QnA.DbModels/. ./QnA.DbModels/
RUN dotnet restore ./QnA

# copy everything else and build app
WORKDIR /source/QnA
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 6060
ENTRYPOINT ["dotnet", "QnA.dll"]