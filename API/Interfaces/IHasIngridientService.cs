using Model.MODEL;

namespace API.Interfaces
{
    public interface IHasIngridientService       
    {
        HasIngridient GetHasIngridientByRecipeAndIngridient(int recipeId, int ingridientId);

        bool HasIngridientByRecipeAndIngridientExists(int recipeId, int ingridientId);

        bool DelateHasIngridient(HasIngridient hasIngridient);

    }
}
