using Microsoft.EntityFrameworkCore;

using PizzaManagement.Domain.Entities.Ingredients;
using PizzaManagement.Domain.Entities.Ratings;
using PizzaManagement.Domain.Entities.Recipes;
using PizzaManagement.Domain.Entities.Recipes.Enums;
using PizzaManagement.Domain.Entities.Recipes.ValueObjects;
using PizzaManagement.Domain.Entities.RecipesIngredients;
using PizzaManagement.Domain.Entities.Steps;
using PizzaManagement.Domain.Entities.Users;

namespace PizzaManagement.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Step> Steps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Step>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Ingredient>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<RecipeIngredient>()
                .Property(x => x.Quantity)
                .HasColumnType("decimal(18, 2)");

            // Recipes - Steps / one - many
            modelBuilder.Entity<Recipe>()
                .HasMany(x => x.Steps)
                .WithOne(x => x.Recipe)
                .HasForeignKey(x => x.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Recipe - Ingredient / Many to many
            modelBuilder.Entity<RecipeIngredient>().HasKey(ri => new { ri.RecipeId, ri.IngredientId });
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipesIngredients)
                .HasForeignKey(ri => ri.IngredientId);
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipesIngredients)
                .HasForeignKey(ri => ri.RecipeId);

            // Recipe - User / Many to many
            modelBuilder.Entity<Rating>().HasKey(ra => new { ra.RecipeId, ra.UserId });
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId);
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Recipe)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.RecipeId);

            // Owned Propeties
            modelBuilder.Entity<Recipe>()
                .OwnsOne(x => x.PreparationTime);
        }
    }
}