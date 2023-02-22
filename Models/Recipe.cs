namespace OneweekNutrition.Models
{
    public class Recipe
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public int? Calories { get; set; }
        public int? Carbohydrates { get; set; }
        public int? Protein { get; set; }

        public List<Component> CompList { get; set; }



    }
}
