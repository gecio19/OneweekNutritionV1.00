using OneweekNutrition.Models.Schedul;

namespace OneweekNutrition.Repository
{
    public interface ISchedulRepository
    {

        IEnumerable<DayCard> GetAll();



    }
}
