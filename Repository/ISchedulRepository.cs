using OneweekNutrition.Models;
using OneweekNutrition.Models.Nowy_folder;
using OneweekNutrition.Models.Schedul;

namespace OneweekNutrition.Repository
{
    public interface ISchedulRepository
    {

        IEnumerable<DayCard> GetAll();
        Model_Recip Getrecipe(int id);

        List<ShoppingHelper> ShoppngList();




    }
}
