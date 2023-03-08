using Microsoft.AspNetCore.Mvc;
using OneweekNutrition.Data;
using OneweekNutrition.Models;
using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;







namespace OneweekNutrition.Controllers
{

    [Authorize] // You can only enter Home if you are loggged in

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _context;
        private IWebHostEnvironment webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IWebHostEnvironment IWebHostEnvironment)
        {
            _logger = logger;
            _context = context;
            webHostEnvironment = IWebHostEnvironment;
        }

        



        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Access");
        }





        public IActionResult Test()
        {
            return View();
        }


        public IActionResult Index()
        {

            return View();
        }




        #region GetAllRecips


        [HttpGet]
        public JsonResult ShowAllRecips()
        {
            var Recips = _context.Recipes.ToList();









            return Json(Recips);
        }








        #endregion






        #region Componentcreating

        public IActionResult Create()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public JsonResult SaveComponent(Component component)
        {
            var NowyComp = component;

            //To git
            string Img_Path = Replace_Img(NowyComp.Name).Replace(@"\", @"/"); ;


            NowyComp.ImgPath = Img_Path;

            _context.Component.Add(NowyComp);
            _context.SaveChanges();

            return Json("Component saved successfully");
        }


        private string Replace_Img(string Img_Name)
        {

            string filename = "1.jpeg";


            string imagepath = GetActualpath(filename);

            string R_imagepath = imagepath.Replace("Temporary", "Constant").Replace("1.jpeg", Img_Name + ".jpeg");

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Copy(imagepath, R_imagepath);
                System.IO.File.Delete(imagepath);
            }


            return R_imagepath;

        }

        #endregion

        #region Images

        [HttpPost]
        public async Task<ActionResult> Temp_UploadImage()
        {
            string Result = string.Empty;

            var Files = Request.Form.Files;
            foreach (IFormFile source in Files)
            {
                string FileName = source.Name + ".jpeg";

                string imagepath = String.Empty;

                imagepath = GetActualpath(FileName);


                //sprawdzanie czy plik istnieje w folderze ezeli tak to usuwamy

                try
                {
                    if (System.IO.File.Exists(imagepath)) { System.IO.File.Delete(imagepath); }

                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await source.CopyToAsync(stream);
                        Result = "pass";

                    }
                }
                catch (Exception ex)
                {

                }


            }


            return Ok(Result);

        }



        public string GetActualpath(string FileName)
        {


            string _path = Path.Combine(webHostEnvironment.WebRootPath + "\\Uploads\\Temporary\\", FileName);

            return _path;
        }




        #endregion

        #region GetComponent

        public JsonResult GetAllComponents()
        {
            var Componentts = _context.Component.ToList();

            /*string DoZmian = "C:\\Users\\Czesio\\Desktop\\OneweekNutrition-master\\wwwroot\\Uploads\\Constant\\TEST.jpeg";

            string Pozmianie = DoZmian.Replace('\\', '/');*/



            /* foreach (var item in Componentts)
             {
                 if(item.ImgPath != null)
                 item.ImgPath.Replace('\\', '/');

             }*/

            return Json(Componentts);
        }





        #endregion

        #region CreatingRecip

        public JsonResult Calc_Recip(string[] CompIdArr)
        {


            int[] ListCompID = new int[CompIdArr.Length];


            for (int i = 0; i < CompIdArr.Length; i++)
            {
                ListCompID[i] = Int32.Parse(CompIdArr[i]);
            }



            var Components = new List<Component>();

            for (int i = 0; i < CompIdArr.Length; i++)
            {
                var element = _context.Component.Where(b => b.Id == ListCompID[i]).FirstOrDefault();
                Components.Add(element);

            }




            return Json(Components);
        }












        #endregion


        #region UpdateRecip


        public JsonResult UploadRecip(string recipName, string recipDes, string[] compIds, string[] compweights, string recipCalory, string recipprotein, string recipcarbo)
        {

            Recipe recipe = new Recipe();
            recipe.Name = recipName;
            recipe.Description = recipDes;



            /// Do poprawy zmiana formy ze string 
           recipe.Calories =  Convert.ToDouble(recipCalory.Replace('.',','));
           recipe.Protein = Convert.ToDouble(recipprotein.Replace('.', ','));
           recipe.Carbohydrates = Convert.ToDouble(recipcarbo.Replace('.', ','));


          

            


            #region ToRepair
            List<RecipComponent> Recips_connect = new List<RecipComponent>();



            for (int i = 0; i < compIds.Length; i++)
            {
                RecipComponent S_connect = new RecipComponent();
                int indeks = Int32.Parse(compIds[i]);
                S_connect.Recipe = recipe;
                S_connect.Component = _context.Component.FirstOrDefault(x => x.Id == indeks);


                int weight = Int32.Parse(compweights[i]);

                S_connect.Weight = weight;


                Recips_connect.Add(S_connect);
            }
            Replace_Recip_Img(recipe.Name);

            recipe.RecipComponents = Recips_connect;

            _context.Recipes.Add(recipe);
            _context.SaveChanges();


            #endregion

            //Przenoszenia zdjecia dodać !!!!!!!!


            return Json("Tescik");
        }





        private string Replace_Recip_Img(string Img_Name)
        {

            string filename = "2.jpeg";


            string imagepath = GetActualpath(filename);

            string R_imagepath = imagepath.Replace("Temporary", "Recips").Replace("2.jpeg", Img_Name + ".jpeg");

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Copy(imagepath, R_imagepath);
                System.IO.File.Delete(imagepath);
            }


            return R_imagepath;

        }





        #endregion

    }
}