using MessagePack;
using Microsoft.EntityFrameworkCore;

namespace OneweekNutrition.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Double Calories { get; set; }
        public Double Carbohydrates { get; set; }
        public Double Protein { get; set; }

        //public List<Component> Components { get; set; }


        public List<RecipComponent> RecipComponents { get; set; }




        public List<UserRecipe> UserRecipe { get; set; }



    }
}
