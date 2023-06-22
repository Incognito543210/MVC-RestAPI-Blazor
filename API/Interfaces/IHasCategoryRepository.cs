using Model.MODEL;

namespace API.Interfaces
{
    public interface IHasCategoryRepository
    {
        HasCategory GetHasCategoryForRecipe(int id);
        HasCategory GetHasCategoryForTag(int id);
        bool HasCategoryExistsByRecipe(int id);
        bool HasCategoryExistsByTag(int id);
        bool CreateHasCategory(HasCategory hasCategory);
        bool DeleteHasCategory(HasCategory hasCategory);
        bool Save();
    }
}
