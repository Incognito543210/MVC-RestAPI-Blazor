using DAL;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model.MODEL;

namespace API.Interfaces
{
    public interface IRecipeRepository 
    {

        ICollection<Recipe> GetRecipes();


    }
}
