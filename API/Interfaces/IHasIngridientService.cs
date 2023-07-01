using Model.MODEL;

namespace API.Interfaces
{
    public interface IHasIngridientService       
    {
        HasIngridient GetHasIngridientByRecipeAndIngridient(int recipeId, int ingridientId);

        bool HasIngridientByRecipeAndIngridientExists(int recipeId, int ingridientId);

        bool DelateHasIngridient(HasIngridient hasIngridient);

        ICollection<HasIngridient> GetHasIngridientsByRecipe(int recipeID);
        bool Save();

        bool DeleteIngridientsForRecipe(List<HasIngridient> hasIngridients);
    }
}
