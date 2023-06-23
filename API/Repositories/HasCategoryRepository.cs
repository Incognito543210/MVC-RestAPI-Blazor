﻿using API.Interfaces;
using DAL;
using Model.MODEL;

namespace API.Repositories
{
    public class HasCategoryRepository : IHasCategoryRepository
    {
        private readonly DataContext _context;

        public HasCategoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateHasCategory(HasCategory hasCategory)
        {
            _context.Add(hasCategory);
            return Save();
        }

        public bool DeleteHasCategory(HasCategory hasCategory)
        {
            _context.Remove(hasCategory);
            return Save();
        }

        public HasCategory GetHasCategoryForRecipe(int id)
        {
            return _context.HasCategories.Where(hc => hc.RecipeID == id).FirstOrDefault();
        }
        public HasCategory GetHasCategoryForTag(int id)
        {
            return _context.HasCategories.Where(hc => hc.TagID == id).FirstOrDefault();
        }

        public bool HasCategoryExistsByRecipe(int id)
        {
            return _context.HasCategories.Any(hc => hc.RecipeID == id);
        }
        public bool HasCategoryExistsByTag(int id)
        {
            return _context.HasCategories.Any(hc => hc.TagID == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateHasCategory(HasCategory hasCategory)
        {
            _context.Update(hasCategory);
            return Save();
        }
    }
}