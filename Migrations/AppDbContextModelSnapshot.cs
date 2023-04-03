﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OneweekNutrition.Data;

#nullable disable

namespace OneweekNutrition.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("OneweekNutrition.Models.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("Calories")
                        .HasColumnType("float");

                    b.Property<float>("Carbohydrates")
                        .HasColumnType("float");

                    b.Property<string>("ImgPath")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("Protein")
                        .HasColumnType("float");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Component");
                });

            modelBuilder.Entity("OneweekNutrition.Models.RecipComponent", b =>
                {
                    b.Property<int>("RecipId")
                        .HasColumnType("int");

                    b.Property<int>("ComponentID")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("RecipId", "ComponentID");

                    b.HasIndex("ComponentID");

                    b.ToTable("RecipComponent");
                });

            modelBuilder.Entity("OneweekNutrition.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Calories")
                        .HasColumnType("double");

                    b.Property<double>("Carbohydrates")
                        .HasColumnType("double");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Protein")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("OneweekNutrition.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("PPM")
                        .HasColumnType("double");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OneweekNutrition.Models.UserRecipe", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("EatDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MealTime")
                        .HasColumnType("int");

                    b.Property<int>("RecipId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("RecipId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRecipe");
                });

            modelBuilder.Entity("OneweekNutrition.Models.RecipComponent", b =>
                {
                    b.HasOne("OneweekNutrition.Models.Component", "Component")
                        .WithMany("RecipComponents")
                        .HasForeignKey("ComponentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OneweekNutrition.Models.Recipe", "Recipe")
                        .WithMany("RecipComponents")
                        .HasForeignKey("RecipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Component");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("OneweekNutrition.Models.UserRecipe", b =>
                {
                    b.HasOne("OneweekNutrition.Models.Recipe", "Recipe")
                        .WithMany("UserRecipe")
                        .HasForeignKey("RecipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OneweekNutrition.Models.User", "User")
                        .WithMany("UserRecipe")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OneweekNutrition.Models.Component", b =>
                {
                    b.Navigation("RecipComponents");
                });

            modelBuilder.Entity("OneweekNutrition.Models.Recipe", b =>
                {
                    b.Navigation("RecipComponents");

                    b.Navigation("UserRecipe");
                });

            modelBuilder.Entity("OneweekNutrition.Models.User", b =>
                {
                    b.Navigation("UserRecipe");
                });
#pragma warning restore 612, 618
        }
    }
}
