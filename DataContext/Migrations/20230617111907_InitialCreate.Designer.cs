﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230617111907_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.7");

            modelBuilder.Entity("IngridientRecipe", b =>
                {
                    b.Property<int>("IngridientsIngridientID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RecipesRecipeID")
                        .HasColumnType("INTEGER");

                    b.HasKey("IngridientsIngridientID", "RecipesRecipeID");

                    b.HasIndex("RecipesRecipeID");

                    b.ToTable("IngridientRecipe");
                });

            modelBuilder.Entity("Model.MODEL.HasCategory", b =>
                {
                    b.Property<int>("HasCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IngridientID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RecipeID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TagID")
                        .HasColumnType("INTEGER");

                    b.HasKey("HasCategoryID");

                    b.HasIndex("IngridientID");

                    b.HasIndex("RecipeID");

                    b.HasIndex("TagID");

                    b.ToTable("HasCategories");
                });

            modelBuilder.Entity("Model.MODEL.HasIngridient", b =>
                {
                    b.Property<int>("HasIngridientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Amonut")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IngridientID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RecipeID")
                        .HasColumnType("INTEGER");

                    b.HasKey("HasIngridientID");

                    b.HasIndex("IngridientID");

                    b.HasIndex("RecipeID");

                    b.ToTable("HasIngridients");
                });

            modelBuilder.Entity("Model.MODEL.Ingridient", b =>
                {
                    b.Property<int>("IngridientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IngridientID");

                    b.ToTable("Ingridients");
                });

            modelBuilder.Entity("Model.MODEL.Opinion", b =>
                {
                    b.Property<int>("OpinionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PostData")
                        .HasColumnType("TEXT");

                    b.Property<int>("Rate")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RecipeID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("OpinionID");

                    b.HasIndex("RecipeID");

                    b.HasIndex("UserID");

                    b.ToTable("Opinions");
                });

            modelBuilder.Entity("Model.MODEL.Recipe", b =>
                {
                    b.Property<int>("RecipeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Difficulty")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Portions")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PostData")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PrepareTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("RecipeID");

                    b.HasIndex("UserID");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Model.MODEL.Tag", b =>
                {
                    b.Property<int>("TagID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TagID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Model.MODEL.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RecipeTag", b =>
                {
                    b.Property<int>("RecipesRecipeID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("tagsTagID")
                        .HasColumnType("INTEGER");

                    b.HasKey("RecipesRecipeID", "tagsTagID");

                    b.HasIndex("tagsTagID");

                    b.ToTable("RecipeTag");
                });

            modelBuilder.Entity("IngridientRecipe", b =>
                {
                    b.HasOne("Model.MODEL.Ingridient", null)
                        .WithMany()
                        .HasForeignKey("IngridientsIngridientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.MODEL.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesRecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Model.MODEL.HasCategory", b =>
                {
                    b.HasOne("Model.MODEL.Ingridient", "Ingridient")
                        .WithMany()
                        .HasForeignKey("IngridientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.MODEL.Recipe", "Recipe")
                        .WithMany("HasCategories")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.MODEL.Tag", null)
                        .WithMany("HasCategories")
                        .HasForeignKey("TagID");

                    b.Navigation("Ingridient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Model.MODEL.HasIngridient", b =>
                {
                    b.HasOne("Model.MODEL.Ingridient", "Ingridient")
                        .WithMany("HasIngridients")
                        .HasForeignKey("IngridientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.MODEL.Recipe", "Recipe")
                        .WithMany("HasIngridients")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingridient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Model.MODEL.Opinion", b =>
                {
                    b.HasOne("Model.MODEL.Recipe", "Recipe")
                        .WithMany("Opinions")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.MODEL.User", "User")
                        .WithMany("Opinions")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.MODEL.Recipe", b =>
                {
                    b.HasOne("Model.MODEL.User", "User")
                        .WithMany("Recipes")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeTag", b =>
                {
                    b.HasOne("Model.MODEL.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesRecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.MODEL.Tag", null)
                        .WithMany()
                        .HasForeignKey("tagsTagID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Model.MODEL.Ingridient", b =>
                {
                    b.Navigation("HasIngridients");
                });

            modelBuilder.Entity("Model.MODEL.Recipe", b =>
                {
                    b.Navigation("HasCategories");

                    b.Navigation("HasIngridients");

                    b.Navigation("Opinions");
                });

            modelBuilder.Entity("Model.MODEL.Tag", b =>
                {
                    b.Navigation("HasCategories");
                });

            modelBuilder.Entity("Model.MODEL.User", b =>
                {
                    b.Navigation("Opinions");

                    b.Navigation("Recipes");
                });
#pragma warning restore 612, 618
        }
    }
}