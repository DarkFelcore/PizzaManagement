using Microsoft.Extensions.Logging;

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
    public static class ApplicationDbContextSeed
    {
        public async static Task SeedAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                // Users
                if (!context.Users.Any())
                {
                    var user = new User(
                        id: Guid.Parse("53884d16-46a3-4347-9c3a-addf427d38b4"),
                        firstName: "Anthony",
                        lastName: "Deville",
                        email: "adev08@outlook.com",
                        passwordHash: BCrypt.Net.BCrypt.HashPassword("Password123!!")
                    );

                    await context.AddAsync(user);
                    await context.SaveChangesAsync();
                }

                // Recipes
                if (!context.Recipes.Any())
                {
                    var recipe = new Recipe(
                        id: Guid.Parse("f127a49d-167b-4726-ba53-6dba1880c485"),
                        title: "Pizza Margarita a la Italiano",
                        description: "Margherita pizza is known for its ingredients representing the colours of the Italian flag. These ingredients include red tomato sauce, white mozzarella and fresh green basil. When all of these delicious flavours are combined on a hand-kneaded pizza base, a universally-adored pizza is created.",
                        image: null,
                        preparationTime: new PreparationTime(
                            duration: 10,
                            timeMeasurement: TimeMeasurement.Minutes
                        )
                    );
                    await context.AddAsync(recipe);
                    await context.SaveChangesAsync();
                }

                // Ratings
                if (!context.Ratings.Any())
                {
                    var rating = new Rating(
                        userId: Guid.Parse("53884d16-46a3-4347-9c3a-addf427d38b4"),
                        recipeId: Guid.Parse("f127a49d-167b-4726-ba53-6dba1880c485"),
                        stars: 3,
                        comment: "Excellent Magarita Pizza. Lots of flavours. I recommend you this!"
                    );

                    await context.AddAsync(rating);
                    await context.SaveChangesAsync();
                }

                if(!context.Steps.Any())
                {
                    var steps = new List<Step>();
                    var step_1 = new Step(
                        id: Guid.Parse("dd4e9161-09cd-4a84-bcf0-886d2496eb7d"),
                        number: 1,
                        description: "Preheat the oven to 250°C and line 2 baking trays with baking paper or brush them with olive oil. Roll out the pizza dough and cut out 4 bases of about 25cm diameter from it. Place the pizza bases on the baking sheets.",
                        recipeId: Guid.Parse("f127a49d-167b-4726-ba53-6dba1880c485")
                    );
                    var step_2 = new Step(
                        id: Guid.Parse("55e4eeb2-d934-4430-8928-0b472eb381e1"),
                        number: 2,
                        description: "Generously brush the bottoms with tomato sauce, then sprinkle grated cheese and basil leaves over the pizza. Season with salt and pepper and drizzle with olive oil.",
                        recipeId: Guid.Parse("f127a49d-167b-4726-ba53-6dba1880c485")
                    );
                    var step_3 = new Step(
                        id: Guid.Parse("1228cb95-9af9-42f0-b11c-02b064b16600"),
                        number: 3,
                        description: "Bake the pizzas for about 25 minutes in the center of the oven. Slice the pizza with a pizza roller and serve. Tip: Sprinkle some dried oregano over the tomato paste for a spicier pizza.",
                        recipeId: Guid.Parse("f127a49d-167b-4726-ba53-6dba1880c485")
                    );
                    steps.Add(step_1);
                    steps.Add(step_2);
                    steps.Add(step_3);
                    
                    await context.Steps.AddRangeAsync(steps);
                    await context.SaveChangesAsync();
                }

                if(!context.Ingredients.Any())
                {
                    var ingredients = new List<Ingredient>();
                    var ingredient_1 = new Ingredient(
                        id: Guid.Parse("fc40bb6e-e70e-44ad-be43-98c9309a13dd"),
                        name: "Pizza dough"
                    );
                    var ingredient_2 = new Ingredient(
                        id: Guid.Parse("ef52ff7e-73f9-46fb-81b6-09bf9dd01f6e"),
                        name: "Basil"
                    );
                    var ingredient_3 = new Ingredient(
                        id: Guid.Parse("67612bb4-c872-42f5-b77d-14afbc131f25"),
                        name: "Olive oil"
                    );
                    var ingredient_4 = new Ingredient(
                        id: Guid.Parse("861aef3d-78ae-4b6d-a3ba-e0bbc59f1098"),
                        name: "Tomato passata"
                    );
                    var ingredient_5 = new Ingredient(
                        id: Guid.Parse("a85c90bd-ad97-4248-8f7f-01c651292ba9"),
                        name: "Grated mozzarella"
                    );
                    var ingredient_6 = new Ingredient(
                        id: Guid.Parse("c7d85c5b-60a8-41ab-90f0-a65518a3d54a"),
                        name: "Salt"
                    );
                    var ingredient_7 = new Ingredient(
                        id: Guid.Parse("a66b4fe5-2725-4a28-a454-d8617addfa54"),
                        name: "Pepper"
                    );
                    ingredients.Add(ingredient_1);
                    ingredients.Add(ingredient_2);
                    ingredients.Add(ingredient_3);
                    ingredients.Add(ingredient_4);
                    ingredients.Add(ingredient_5);
                    ingredients.Add(ingredient_6);
                    ingredients.Add(ingredient_7);

                    await context.Ingredients.AddRangeAsync(ingredients);
                    await context.SaveChangesAsync();
                }

                if(!context.RecipeIngredients.Any())
                {
                    var recipeIngredients = new List<RecipeIngredient>();
                    var recipeIngredient_1 = new RecipeIngredient(
                        recipeId: Guid.Parse("f127a49d-167b-4726-ba53-6dba1880c485"),
                        ingredientId: Guid.Parse("fc40bb6e-e70e-44ad-be43-98c9309a13dd"),
                        quantity: 1
                    );
                    var recipeIngredient_2 = new RecipeIngredient(
                        recipeId: Guid.Parse("f127a49d-167b-4726-ba53-6dba1880c485"),
                        ingredientId: Guid.Parse("ef52ff7e-73f9-46fb-81b6-09bf9dd01f6e"),
                        quantity: 1,
                        measurement: "bunch"
                    );
                    var recipeIngredient_3 = new RecipeIngredient(
                        recipeId: Guid.Parse("f127a49d-167b-4726-ba53-6dba1880c485"),
                        ingredientId: Guid.Parse("67612bb4-c872-42f5-b77d-14afbc131f25")
                    );
                    var recipeIngredient_4 = new RecipeIngredient(
                        recipeId: Guid.Parse("f127a49d-167b-4726-ba53-6dba1880c485"),
                        ingredientId: Guid.Parse("861aef3d-78ae-4b6d-a3ba-e0bbc59f1098"),
                        quantity: 500,
                        measurement: "ml"
                    );
                    var recipeIngredient_5 = new RecipeIngredient(
                        recipeId: Guid.Parse("f127a49d-167b-4726-ba53-6dba1880c485"),
                        ingredientId: Guid.Parse("a85c90bd-ad97-4248-8f7f-01c651292ba9"),
                        quantity: 300,
                        measurement: "g"
                    );
                    var recipeIngredient_6 = new RecipeIngredient(
                        recipeId: Guid.Parse("f127a49d-167b-4726-ba53-6dba1880c485"),
                        ingredientId: Guid.Parse("c7d85c5b-60a8-41ab-90f0-a65518a3d54a")
                    );
                    var recipeIngredient_7 = new RecipeIngredient(
                        recipeId: Guid.Parse("f127a49d-167b-4726-ba53-6dba1880c485"),
                        ingredientId: Guid.Parse("a66b4fe5-2725-4a28-a454-d8617addfa54")
                    );

                    recipeIngredients.Add(recipeIngredient_1);
                    recipeIngredients.Add(recipeIngredient_2);
                    recipeIngredients.Add(recipeIngredient_3);
                    recipeIngredients.Add(recipeIngredient_4);
                    recipeIngredients.Add(recipeIngredient_5);
                    recipeIngredients.Add(recipeIngredient_6);
                    recipeIngredients.Add(recipeIngredient_7);

                    await context.RecipeIngredients.AddRangeAsync(recipeIngredients);
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
                logger.LogInformation(ex.Message);
            }
        }
    }
}