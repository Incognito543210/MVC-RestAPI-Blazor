using Model.MODEL;

namespace API.Interfaces
{
    public interface IIngridientsService
    {
        ICollection<Ingridient>GetIngridients();
        Ingridient GetIngridient(int id);

        bool IngridientExists(int id);

        bool CreateIngridient(Ingridient ingridient, int recipeId);

        bool Save();

    }

}
