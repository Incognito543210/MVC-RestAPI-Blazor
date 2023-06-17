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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HasCategory>()
                 .HasKey(hc => new { hc.RecipeID, hc.TagID });
            modelBuilder.Entity<HasCategory>()
                .HasOne(r => r.Recipe)
                .WithMany(hc => hc.HasCategories)
                .HasForeignKey(r => r.RecipeID);
            modelBuilder.Entity<HasCategory>()
               .HasOne(t => t.Tag)
               .WithMany(hc => hc.HasCategories)
               .HasForeignKey(t => t.TagID);
            modelBuilder.Entity<HasIngridient>()
                .HasKey(hi => new { hi.RecipeID, hi.IngridientID });
            modelBuilder.Entity<HasIngridient>()
               .HasOne(r => r.Recipe)
               .WithMany(hc => hc.HasIngridients)
               .HasForeignKey(r => r.RecipeID);
            modelBuilder.Entity<HasIngridient>()
               .HasOne(i => i.Ingridient)
               .WithMany(hc => hc.HasIngridients)
               .HasForeignKey(t => t.IngridientID);
        }

    }
}
