namespace OneweekNutrition.Models
{
    public class RecipComponent
    {
        public int CompId { get; set; }
        public Component Component { get; set; }

        public int RecipId { get; set; }

        public Recipe Recipe { get; set; }

        public int Weight { get; set; }

    }
}
