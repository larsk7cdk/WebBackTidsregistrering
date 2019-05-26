# Introduktion
Dette projekt indeholder eksamensopgave for faget Webudvikling Backend


# Kom igang
For at komme igang med at afvikle projektet skal connectstring til SQL Server tilrettes og databasen skal oprettes


#### For at opdatere databasen
1. Start package manager console
2. Skift Default project WebBackTidsregistrering.Persistance
3. cd .\WebBackTidsregistrering.Persistance
4. dotnet ef database update -c AppIdentityDbContext
5. dotnet ef database update -c AppDataDbContext

### Start MVC projekt
1. Start en kommando prompt 
2. Skift til mappen .\WebBackTidsregistrering\WebBackTidsregistrering.WebUI
3. Skriv dotnet restore og tryk enter
4. Skriv dotnet run og tryk enter
5. Åbn en webbrowser og indtast url'en http://localhost:5001


### Start WEB API projekt
1. Start en kommando prompt 
2. Skift til mappen .\WebBackTidsregistrering\WebBackTidsregistrering.WebAPI
3. Skriv dotnet restore og tryk enter
4. Skriv dotnet run og tryk enter
5. Åbn en webbrowser og indtast url'en http://localhost:5003
6. Dette vil åbne swagger
