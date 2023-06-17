using Microsoft.EntityFrameworkCore;
using Model.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataContext
{
        public class Datacontext:DbContext
    {

        public Datacontext (DbContextOptions options): base (options)
        {

        }

        public DbSet<HasCategory> HasCategories  { get; set; }
        public DbSet<HasIngridient> HasIngridients { get; set; }
        public DbSet<Ingridient> Ingridients { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<RecipeTag> RecipeTags { get; set; }

    }
}
