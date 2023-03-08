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

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipComponent>()
                .HasKey(bc => new {bc.RecipId, bc.ComponentID });

            modelBuilder.Entity<RecipComponent>()
                .HasOne(bc => bc.Recipe)
                .WithMany(b => b.RecipComponents)
                .HasForeignKey(bc => bc.RecipId);

            modelBuilder.Entity<RecipComponent>()
                .HasOne(bc => bc.Component)
                .WithMany(b => b.RecipComponents)
                .HasForeignKey(bc => bc.ComponentID);



            // User-Recip

            modelBuilder.Entity<UserRecipe>()
                .HasKey(u => new { u.UserId, u.RecipId });

            modelBuilder.Entity<UserRecipe>()
                .HasOne(u => u.User)
                .WithMany(d => d.UserRecipe)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserRecipe>()
                .HasOne(u => u.Recipe)
                .WithMany(d => d.UserRecipe)
                .HasForeignKey(u => u.RecipId);


            








        }
    }
}
