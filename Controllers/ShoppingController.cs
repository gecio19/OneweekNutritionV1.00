using Microsoft.AspNetCore.Mvc;
using OneweekNutrition.Repository;

namespace OneweekNutrition.Controllers
{
    public class ShoppingController : Controller
    {

        private readonly ISchedulRepository _schedulRepository;


        public ShoppingController(ISchedulRepository schedulRepository)
        {
            _schedulRepository = schedulRepository;
        }


        public IActionResult Shopping()
        {
            var ShopingList = _schedulRepository.ShoppngList();

            return View(ShopingList);
        }



    }
}
