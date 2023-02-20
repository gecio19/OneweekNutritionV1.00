using Microsoft.EntityFrameworkCore;
using OneweekNutrition.Models;

namespace OneweekNutrition.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }



        public DbSet<Component> Component { get; set; }




    }
}
