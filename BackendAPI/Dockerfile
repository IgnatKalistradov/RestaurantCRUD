FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

EXPOSE 8080

COPY ./BackendAPI.csproj .

RUN ["dotnet", "restore", "BackendAPI.csproj"]

COPY . .

CMD ["dotnet", "run", "--urls=http://0.0.0.0:8080"]

