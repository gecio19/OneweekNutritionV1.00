using Microsoft.AspNetCore.Mvc;
using OneweekNutrition.Data;
using OneweekNutrition.Models;
using System;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace OneweekNutrition.Controllers
{
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



        public IActionResult Test()
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }

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

    }
}