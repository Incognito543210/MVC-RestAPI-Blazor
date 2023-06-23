﻿using Model;
using Model.MODEL;

namespace View.Data
{
    public class RecipeService
    {
        private List<Recipe> recipes = new();

        public Task AddRecipeAsync(Recipe recipe)
        {
            recipes.Add(recipe);
            return Task.CompletedTask;
        }

        public async Task<List<Recipe>> GetRecipesAsync()
        {
            return await Task.FromResult(recipes);
        }

        private readonly HttpClient _httpClient;

        public RecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Recipe>> GetRecipeListAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Recipe>>("/api/Recipe");
        }

        public async Task<Recipe> GetRecipeAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Recipe>("/api/RecipeId");
        }
    }
}
