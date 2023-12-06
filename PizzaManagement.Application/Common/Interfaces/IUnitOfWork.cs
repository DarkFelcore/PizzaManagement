namespace PizzaManagement.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IRecipeRepository RecipeRepository { get; }
        IIngredientRepository IngredientRepository { get; }
        Task CompleteAsync();
    }
}