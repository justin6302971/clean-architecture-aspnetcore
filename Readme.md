# Clean Architecture For dot net core application

## Purposes
create a sample application using clean architecture for these purposes: 
 * independent of framework, ui, database and anything external
 * testable


## Project Overview
here are the project structure

### Domain
mainly contain enterprise-wide logics and types,
This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer. 

### Application
mainly contain bussiness logics and types,
This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure
mainly contain all external concerns,
This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.
noted: for current project structure, also implement application services in this layer

### Api
This layer is a asp net core web api project. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only Startup.cs should reference Infrastructure.

### Project dependencies
web api(client) -> infrastructure -> application -> domain


## Technologies
* asp net core 
* ef core
* postgreSQL
* swagger
* docker
* automapper
* nunit
* 


## important Trade-off
have to recreate interfaces for dbcontext to follow the clean architecture project structure and principles
1. [Make IDbContext](https://github.com/dotnet/efcore/issues/16470)
2. [Resolve your DbContext as an interface](https://www.jerriepelser.com/blog/resolve-dbcontext-as-interface-in-aspnet5-ioc-container/)


## commands
``` bash
#build the images with docker cli
docker build --no-cache -t daily-challenge-service-webapi:alpine .

# check images
docker images

# check container
docker ps


# https local cert settings

# clean
dotnet dev-certs https --clean

# setup(windows)
dotnet dev-certs https --trust -ep $env:USERPROFILE\.aspnet\https\aspnetapp.pfx -p SECRETPASSWORD

# setup(mac)
dotnet dev-certs https --trust -ep ~/.aspnet/https/aspnetapp.pfx -p temppassword

# run the container (mac)
docker run --rm -it \
           -p 5000:5000 \
           -p 5001:5001  \
           -e ASPNETCORE_ENVIRONMENT=Development \
           -e ASPNETCORE_Kestrel__Certificates__Default__Password=temppassword \
           -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx \
           -v ~/.aspnet/https/:/https/ \
            --name daily-challenge-service-webapi \
           daily-challenge-service-webapi:alpine

#check the running container with sh
docker exec -it daily-challenge-service-webapi sh

#stop the container
docker stop daily-challenge-service-webapi


#with docker-compose(optional)

#check docker-compose files
docker-compose config

#force build image and run container 
docker-compose up --force-recreate --build 

# run service background
docker-compose up -d

#shutdown service 
docker-compose down



#db commands
#check dbcontext
dotnet ef dbcontext list -p ./src/CleanArchitecture.WebApi

dotnet ef dbcontext info -p ./src/CleanArchitecture.WebApi

#add migrations 
dotnet ef migrations add firstmigration -c CleanArchitecture.Infrastructure.Persistence.TodoListDBContext -s ./src/CleanArchitecture.WebApi/CleanArchitecture.WebApi.csproj -p ./src/CleanArchitecture.Infrastructure/CleanArchitecture.Infrastructure.csproj -o Persistence/Migrations 

#list migrations 
dotnet ef migrations list -p ./src/CleanArchitecture.WebApi

#update certain migration to db
dotnet ef database update 20220623175632_testmigration -c CleanArchitecture.Infrastructure.Persistence.TodoListDBContext -s ./src/CleanArchitecture.WebApi/CleanArchitecture.WebApi.csproj -p ./src/CleanArchitecture.Infrastructure/CleanArchitecture.Infrastructure.csproj
```

## TODO 
- [X] project structure setup
  - [ ] di settings
  - [ ] local db environment(postgreSQL)
- [X] authentication with identity service
- [X] serilog setting(local and aws s3 sink) 
- [X] cors setting
- [X] exeception handling (with extended controller, considering using middleware pattern)
- [X] dockerize this app
- [ ] setup ci pipeline(with azure devops or aws)


## references
1. [microsoft doc](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
2. [Clean Architecture Solution Template - jasontaylordev](https://github.com/jasontaylordev/CleanArchitecture/tree/netcore3.1)
3. [clean architecture speech - jason taylor](https://www.youtube.com/watch?v=dK4Yb6-LxAk)
4. [Implementing A Clean Architecture In ASP.NET Core 6](https://www.c-sharpcorner.com/article/implementing-a-clean-architecture-in-asp-net-core-6/)
5. [Clean Architecture for ASP.NET Core Solution: A Case Study](https://blog.ndepend.com/clean-architecture-for-asp-net-core-solution/)
6. [tutorials](https://www.c-sharpcorner.com/article/implementing-cqrs-and-mediator-patterns-with-asp-net-core-web-api/)
7. [tutorials - 1](https://code-maze.com/cqrs-mediatr-in-aspnet-core/)