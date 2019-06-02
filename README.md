# Introduktion
Dette projekt indeholder eksamensopgave for faget Webudvikling Backend


# Kom igang
For at komme igang med at afvikle projektet skal connectstring til SQL Server tilrettes og databasen skal oprettes


#### For at opdatere databasen
1. Åbn filen appsettings.json i .\WebBackTidsregistrering\WebBackTidsregistrering.WebUI
2. Tilret server under ConnectionStrings
3. Åbn filen appsettings.json i .\WebBackTidsregistrering\WebBackTidsregistrering.WebAPI
4. Tilret server under ConnectionStrings
5. Start package manager console
6. Skift Default project WebBackTidsregistrering.Persistance i dropdown
7. Skift mappe i PM konsol med kommandoen cd .\WebBackTidsregistrering.Persistance og tryk enter
8. Skriv kommandoen dotnet ef database update -c AppIdentityDbContext og tryk enter
9. Skriv kommandoen dotnet ef database update -c AppDataDbContext og tryk enter
10. For at kunne afsende E-mail skal oplysninger for MailKit klienten opdateres
11. Åbn filen .\WebBackTidsregistrering\WebBackTidsregistrering.Infrastructure\Services\EmailService.cs
12. Tilret linie 18 med en gyldig SMTP host
13. Tilret linie 20 med gyldig konto og bruger for SMTP hosten
14. Gem filen


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
