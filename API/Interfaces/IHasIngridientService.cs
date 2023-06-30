using Model.MODEL;

namespace API.Interfaces
{
    public interface IHasIngridientService       
    {
        HasIngridient getAmountByRecipeAndIngridient(int recipeId, int ingridientId);

        bool AmountByRecipeAndIngridientExists(int recipeId, int ingridientId);
    }
}
