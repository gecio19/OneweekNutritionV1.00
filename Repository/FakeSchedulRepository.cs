using OneweekNutrition.Data;
using OneweekNutrition.Models;
using OneweekNutrition.Models.Schedul;
using System.Security.Claims;



using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;
using static OneweekNutrition.Controllers.AccessController;
using Newtonsoft.Json.Linq;
using System;
using OneweekNutrition.Models.Nowy_folder;

namespace OneweekNutrition.Repository
{
    public class FakeSchedulRepository : ISchedulRepository
    {

        private readonly AppDbContext _context;

        private int HowManyDays = 9;



        public FakeSchedulRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DayCard> GetAll()
        {

            List<DayCard> days = new List<DayCard>(); //to zwracamy


            //Baza dany
            var User_base = _context.Users.Where(x => x.Id == 1).Include(c => c.UserRecipe).ToList();
            var RecipList = User_base[0].UserRecipe;
            //
            for (int i = 0; i < HowManyDays; i++)
            {
                DayCard dayCard = new DayCard();
                dayCard.DayOfMonth = DateTime.Now.AddDays(i).Day;
                dayCard.DayofWeek = DateTime.Now.AddDays(i).ToString("dddd");

                ///


                var DayRecip = RecipList.Where(x => x.EatDate.Day == dayCard.DayOfMonth).ToList();



                List<DishCard> _dishCard = new List<DishCard>();


                for (int j = 0; j < Enum.GetNames(typeof(MealTime)).Length; j++)
                {
                    DishCard _mealTime = new DishCard();
                    _mealTime.MealTime = ((MealTime)j).ToString();

                    var Testowo = DayRecip.Where(x => x.MealTime == (MealTime)j).ToList();

                    List<Recipe> TempListRecip = new List<Recipe>();
                    foreach (var item in Testowo)
                    {
                        TempListRecip.Add(_context.Recipes.FirstOrDefault(x => x.Id == item.RecipId));
                    }
                    _mealTime.Recipes = TempListRecip;
                    _mealTime.Calory = CaloryCalc(TempListRecip);

                    if (_mealTime.Recipes.Count != 0)
                    {
                        _dishCard.Add(_mealTime);
                    }
                }




                //dayCard.DayCalory = CaloryCalc(dayCard.Recipes);


                dayCard.DishCards = _dishCard;

                dayCard.DayCalory = CalcDayCalory(dayCard);

                days.Add(dayCard);
            }


            return days;
        }






        private double CaloryCalc(List<Recipe> List)
        {
            double sum = 0;

            foreach (var item in List)
            {
                sum += item.Calories;
            }
            return sum;
        }

        private double CalcDayCalory(DayCard _daycard)
        {
            double sum = 0;

            foreach (var item in _daycard.DishCards)
            {
                sum += item.Calory;
            }
            return sum;
        }

        public Model_Recip Getrecipe(int id)
        {
            var cos = _context.Recipes.Where(x => x.Id == id).Include(x => x.RecipComponents).FirstOrDefault();

            List<Component> _recipes = new List<Component>();
            List<int> _weights = new List<int>();
            cos.RecipComponents.ForEach(x => _weights.Add(x.Weight));

            foreach (var item in cos.RecipComponents)
            {
                Component test = _context.Component.FirstOrDefault(x => x.Id == item.ComponentID);
                _recipes.Add(test);

            }


            Model_Recip Modal = new Model_Recip();

            Modal.Name = cos.Name;
            Modal.description = cos.Description;
            Modal.components = _recipes;
            Modal.weights = _weights;



            return Modal;
        }

        public List<ShoppingHelper> ShoppngList()
        {

            var AllDetails = _context.Users.Where(x => x.Id == 1).Include(x => x.UserRecipe).FirstOrDefault();

            var reciplist = new List<int>();

            foreach (var item in AllDetails.UserRecipe)
            {
                if (DateTime.Now < item.EatDate && item.EatDate <= DateTime.Now.AddDays(7))
                {
                    reciplist.Add(item.RecipId);
                }
            }




            List<ShoppingHelper> shoppingHelpers = new List<ShoppingHelper>();

            foreach (var item in reciplist)
            {
                var cos = _context.Recipes.Where(x => x.Id == item).Include(x => x.RecipComponents).FirstOrDefault();
                foreach (var details in cos.RecipComponents)
                {
                    ShoppingHelper helper = new ShoppingHelper();
                    helper.Name = _context.Component.FirstOrDefault(x => x.Id == details.ComponentID).Name;
                    helper.weight = details.Weight;

                    shoppingHelpers.Add(helper);


                }
            }


            //var FinalList = OrganizeList(shoppingHelpers);


            return OrganizeList(shoppingHelpers);
        }


        private List<ShoppingHelper> OrganizeList(List<ShoppingHelper> shoppingHelpers)
        {
            for (int i = 0; i < shoppingHelpers.Count; i++)
            {
                for (int j = 0; j < shoppingHelpers.Count; j++)
                {
                    if (shoppingHelpers[i].Name == shoppingHelpers[j].Name && shoppingHelpers[i].weight != shoppingHelpers[j].weight)
                    {
                        shoppingHelpers[i].weight += shoppingHelpers[j].weight;
                        shoppingHelpers.Remove(shoppingHelpers[j]);
                    }
                }
            }



            return shoppingHelpers;



        }



    }

 }
