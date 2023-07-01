using Microsoft.AspNetCore.Components.Web;
using Model.MODEL;

namespace API.Interfaces
{
    public interface ITagsService
    {
        ICollection<Tag> GetTags();
        Tag GetTag(int id);
        bool TagExists(int tag);

        bool AddTagsForRecipe(Tag tag, string recipeName);

        bool UpdateTagsForRecipe(Tag tag, int recipeId);

        bool Save();


    }
}
