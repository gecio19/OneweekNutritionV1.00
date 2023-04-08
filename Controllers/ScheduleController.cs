using Microsoft.AspNetCore.Mvc;
using OneweekNutrition.Repository;

namespace OneweekNutrition.Controllers
{
    public class ScheduleController : Controller
    {


        private readonly ISchedulRepository _schedulRepository;

        public ScheduleController(ISchedulRepository schedulRepository)
        {
            _schedulRepository = schedulRepository;
        }



        public IActionResult Index()
        {

            return View(_schedulRepository.GetAll());
        }




        public IActionResult RecipDetails(string id)
        {
            int numberMovie = Int32.Parse(id);
            return PartialView("Modal", _schedulRepository.Getrecipe(numberMovie));
        }







    }
}
