# Tidsregistrering


### Entity Framework

##### For at oprette migrations
1. Start package manager console
2. Skift Default project WebBackTidsregistrering.Persistance
3. cd .\WebBackTidsregistrering.Persistance
4. dotnet ef migrations add initialcreate -c AppIdentityDbContext -o Identity/Migrations


##### For at opdatere databasen
1. Start package manager console
2. Skift Default project WebBackTidsregistrering.Persistance
3. cd .\WebBackTidsregistrering.Persistance
4. dotnet ef database update -c AppIdentityDbContext
