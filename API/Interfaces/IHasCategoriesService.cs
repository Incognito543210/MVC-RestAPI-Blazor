using Model.MODEL;

namespace API.Interfaces
{
    public interface IHasCategoriesService
    {
        HasCategory GetHasCategoryForRecipe(int id);
        HasCategory GetHasCategoryForTag(int id);
        bool HasCategoryExistsByRecipe(int id);
        bool HasCategoryExistsByTag(int id);
        bool CreateHasCategory(HasCategory hasCategory);
        ICollection<HasCategory> GetHasCategoriesByTag(int tagID);
        ICollection<HasCategory> GetHasCategoriesByRecipe(int recipeID);
        bool DeleteHasCategoryByTag(int tagID);

        bool DelateHasCategory(HasCategory hasCategory);

        public HasCategory GetHasCategoryByRecipeAndTag(int recipeId, int tagId);
        bool HasCategorytByRecipeAndTagExists(int recipeId, int tagId);
    }
}
