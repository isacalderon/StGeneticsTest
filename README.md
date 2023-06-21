# StGeneticsTest

# StGenetics
Service in .NET to control an animal farm

### To build the services 
first download the prohect 
```
git clone https://github.com/isacalderon/StGeneticsTest
cd STGeneticsService
dotnet build
```

### Run the service 
```
dotnet run
```

### Json Collections 
There is a json collection in the next route. 
```
STGenetics Project/STGeneticsService/StGenetics.postman_collection.json
```

# Database 
I used a local database with a docker 
```
docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=Test@1234" -e "MSSQL_PID=Developer" -e "MSSQL_USER=SA" -p 1433:1433 -d --name=sql mcr.microsoft.com/azure-sql-edge
docker ps
```
The querys are in *Database* folder


