namespace PizzaManagement.Application.Helpers
{
    public class RecipeSpecParams
    {
        private string _search = null!;
        public string? Search
        {
            get => _search;
            set => _search = value!.ToLower();
        }
    }
}