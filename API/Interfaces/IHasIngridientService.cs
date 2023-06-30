namespace API.Interfaces
{
    public interface IHasIngridientService       
    {
        String getAmountByRecipeAndIngridient(int recipeId, int ingridientId);

        bool AmountByRecipeAndIngridientExists(int recipeId, int ingridientId);
    }
}
