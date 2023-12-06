using PizzaManagement.Application.Common.Interfaces;

namespace PizzaManagement.Infrastructure.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public IUserRepository UserRepository { get; private set; }
        public IRecipeRepository RecipeRepository { get; private set; }
        public IIngredientRepository IngredientRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            // Repositories
            UserRepository = new UserRepository(context);
            RecipeRepository = new RecipeRepository(context);
            IngredientRepository = new IngredientRepository(context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}