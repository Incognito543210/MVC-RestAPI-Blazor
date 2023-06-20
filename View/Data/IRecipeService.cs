﻿using Model.MODEL;

namespace View.Data
{
    public interface IRecipeService
    {
        Task AddRecipeAsync(Recipe recipe);
        Task<List<Recipe>> Get();
        Task<List<Recipe>> GetRecipesAsync();
    }
}