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
        public DbSet<Recipe> Recipes { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RecipComponent>()
                .HasKey(x => new {x.CompId, x.RecipId});
            builder.Entity<RecipComponent>()
                .HasOne(x => x.Component)
                .WithMany(x => x.Recips)
                .HasForeignKey(x => x.CompId);

            builder.Entity<RecipComponent>()
                   .HasOne(x => x.Recipe)
                   .WithMany(x => x.Components)
                   .HasForeignKey(x => x.RecipId);
                
        }




    }
}
