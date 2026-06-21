FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

EXPOSE 8080

COPY ./Restaurant.sln ./

COPY ./Restaurant.Core/Restaurant.Core.csproj ./Restaurant.Core/

COPY ./Restaurant.Data/Restaurant.Data.csproj ./Restaurant.Data/

COPY ./Restaurant.Application/Restaurant.Application.csproj ./Restaurant.Application/

COPY ./Restaurant.API/Restaurant.API.csproj ./Restaurant.API/

RUN ["dotnet", "restore", "Restaurant.sln"]

COPY . .

RUN ["dotnet", "build"]

CMD ["dotnet", "run", "--project", "./Restaurant.API/Restaurant.API.csproj", "--urls=http://0.0.0.0:8080"]

