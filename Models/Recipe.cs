using Microsoft.EntityFrameworkCore;

namespace OneweekNutrition.Models
{
    public class Recipe
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int? Calories { get; set; }
        public int? Carbohydrates { get; set; }
        public int? Protein { get; set; }


        public ICollection<RecipComponent> Components{ get; set; }


    }
}
