using Microsoft.AspNetCore.Mvc;


using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using OneweekNutrition.Models;
using OneweekNutrition.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;

namespace OneweekNutrition.Controllers
{
    public class AccessController : Controller
    {

        private readonly AppDbContext _context;


        public AccessController(AppDbContext context)
        {
            _context = context;



        }


        public IActionResult Login()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;

            if (claimsUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");



            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User modelLogin)
        {

            var LUser = _context.Users
                .Where(n => n.Login == modelLogin.Login)
                .FirstOrDefault();
            if (LUser == null)
            {
                ViewData["ValidateMessage"] = "user not found";
                return View();
            }



            if (modelLogin.Login == LUser.Login &&
                    modelLogin.Password == LUser.Password
                )
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Login),

                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);


                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true  // Chyba chodzi o to czy ma sie wylogować // modelLogin.keepLoogedIn

                };


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");

            }


            ViewData["ValidateMessage"] = "user not found";
            return View();
        }

        public JsonResult RegisterUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();



            return Json("OK");
        }



        public JsonResult ActualUser()
        {

            var userlogin = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var _user = _context.Users.Where(n => n.Login == userlogin).FirstOrDefault();


            return Json(_user);
        }



        public JsonResult UploadRecip(string day, string hour, string RecipId, bool _thisweek, bool _nextweek)
        {







            var userlogin = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var _usertoUpdate = _context.Users.Where(n => n.Login == userlogin)
                .Include(c => c.UserRecipe)
                .FirstOrDefault();




            User UpdatedUser = new User();
            UpdatedUser.Id = _usertoUpdate.Id;
            UpdatedUser.Login = _usertoUpdate.Login;
            UpdatedUser.Password = _usertoUpdate.Password;
            UpdatedUser.Name = _usertoUpdate.Name;
            UpdatedUser.PPM = _usertoUpdate.PPM;

            UpdatedUser.UserRecipe = _usertoUpdate.UserRecipe;








            //////////////////////////Do zrobienia w Funkcji to żeby na next week dało sie wybierac i na obecny///////////////////////////////////
            var DayofWeek_format = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), day);
            var EatDay_Closest = GetNextWeekday(DayofWeek_format);


            DateTime Final_date = new DateTime(EatDay_Closest.Year, EatDay_Closest.Month, EatDay_Closest.Day, Int32.Parse(hour.Split(":")[0]), 0, 0);

            /////////////////////////////////////////




            UpdatedUser.UserRecipe.Add(new UserRecipe
            {
                User = _context.Users.FirstOrDefault(x => x.Id == UpdatedUser.Id),
                Recipe = _context.Recipes.FirstOrDefault(x => x.Id == Int32.Parse(RecipId)),
                EatDate = Final_date
            });



            _context.Entry(_usertoUpdate).CurrentValues.SetValues(UpdatedUser);
            _context.SaveChanges();






            return Json("OK");
        }


        public void DateReturn(string day, string hour, bool _thisweek, bool _nextweek)
        {
            string _ActualWeek = (_thisweek == true) ? "1" : "0";
            string _NextWeek = (_nextweek == true) ? "1" : "0";

            string DateTime = _ActualWeek + _NextWeek;

            //


            switch (DateTime)
            {
                case "10":





                    break;

                case "11":

                    break;

                case "01":

                    break;

            }



        }

        public DateTime GetNextWeekday(DayOfWeek day)
        {
            DateTime result = DateTime.Now.AddDays(1);
            while (result.DayOfWeek != day)
                result = result.AddDays(1);
            return result;
        }





        public JsonResult Clear_outdated()
        {
            /* var userlogin = User.FindFirstValue(ClaimTypes.NameIdentifier);

             var _userDB = _context.Users.Where(n => n.Login == userlogin)
                 .Include(c => c.UserRecipe)
                 .FirstOrDefault().UserRecipe;

             foreach (var item in _userDB)
             {

                 if(item.EatDate.AddDays(7) <= DateTime.Now )
                 {
                     int a = 3;

                     _context.Remove(item);


                 }



             }

             _context.SaveChanges();*/



            return Json("JEstak");
        }





        #region Schedul

        public IActionResult Schedule()
        {
            var userlogin = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var _userDB = _context.Users.Where(n => n.Login == userlogin)
                .Include(c => c.UserRecipe)
                .FirstOrDefault();


            List<Tuple<string, Recipe>> Zapis = new List<Tuple<string, Recipe>>();


            foreach (var item in _userDB.UserRecipe)
            {
                // item.EatDate
                var Day = item.EatDate.DayOfWeek;
                var Hour = item.EatDate.Hour;
                var Day_Hour = Day.ToString() + Hour.ToString();

                //Recip

                Recipe _recip = _context.Recipes
                    .Where(x => x.Id == item.RecipId)
                    .FirstOrDefault();

                Tuple<string, Recipe> Tup_DR = new Tuple<string, Recipe>(Day_Hour, _recip);



                Zapis.Add(Tup_DR);
            };





            return View(Zapis);
        }



        public JsonResult SchedulItems()
        {



            var userlogin = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var _userDB = _context.Users.Where(n => n.Login == userlogin)
                .Include(c => c.UserRecipe)
                .FirstOrDefault();

            List<string> Listka = new List<string>();

            List<Tuple<string, string>> Tuplaetest = new List<Tuple<string, string>>();



            foreach (var item in _userDB.UserRecipe)
            {
                // item.EatDate
                var Day = item.EatDate.DayOfWeek;
                var Hour = item.EatDate.Hour;
                var Day_Hour = Day.ToString() + Hour.ToString();

                //Nazwa dania

                var DishName = _context.Recipes.FirstOrDefault(x => x.Id == item.RecipId).Name;





                Tuple<string, string> TEstowa = new Tuple<string, string>(Day_Hour, DishName);


                Tuplaetest.Add(TEstowa);
                //Listka.Add(Day_Hour);




            };

            List<string> Listka2 = new List<string>();
            string cos = "cos";
            Listka2.Add(cos);


            return Json(Tuplaetest);
        }










        #endregion



    }
}
