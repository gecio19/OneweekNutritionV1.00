namespace OneweekNutrition.Models
{
    public class RecipComponent
    {


        public int Weight { get; set; }


        public int RecipId { get; set; }

        public Recipe Recipe { get; set; }

        public int ComponentID { get; set; }


        public Component Component { get; set; }

    }
}
