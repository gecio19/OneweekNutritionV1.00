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

                    if(_mealTime.Recipes.Count != 0)
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
       
     




    }
}
