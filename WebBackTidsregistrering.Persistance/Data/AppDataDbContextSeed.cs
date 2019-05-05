namespace WebBackTidsregistrering.Persistance.Data
{
    public class AppDataDbContextSeed
    {
        public static void Seed(AppDataDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}