using PizzaManagement.Domain.Entities.Ingredients;

namespace PizzaManagement.Application.Common.Interfaces
{
    public interface IIngredientRepository : IGenericRepository<Ingredient>
    {
        Task<Ingredient?> GetIngredientByNameAsync(string name);
    }
}