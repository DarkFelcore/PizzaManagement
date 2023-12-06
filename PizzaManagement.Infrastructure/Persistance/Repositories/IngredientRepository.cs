using Microsoft.EntityFrameworkCore;

using PizzaManagement.Application.Common.Interfaces;
using PizzaManagement.Domain.Entities.Ingredients;

namespace PizzaManagement.Infrastructure.Persistance.Repositories
{
    public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Ingredient?> GetIngredientByNameAsync(string name)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }
    }
}