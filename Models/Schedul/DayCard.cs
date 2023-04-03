namespace OneweekNutrition.Models.Schedul
{
    public class DayCard
    {

        public string DayofWeek { get; set; }
        public int DayOfMonth { get; set; }
        public Double DayCalory { get; set; }
        public List<DishCard> DishCards { get; set; }  // lista karty np jedna karta sniadanie druga obiad
    }
}
