using System.Data.Entity;

namespace OrderSystem.Models
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
        }
    }
}

