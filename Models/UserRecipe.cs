using System.ComponentModel.DataAnnotations;

namespace OneweekNutrition.Models
{
    public class UserRecipe
    {
        [Key]
        public int ID { get; set; }

        public DateTime EatDate { get; set; }

        public MealTime MealTime { get; set; }


        public int UserId { get; set; }

        public User User { get; set; }

        public int RecipId { get; set; }

        public Recipe Recipe { get; set; }




    }
}




public enum MealTime
{
    Sniadanie,
    Obiad,
    Kolacja,
    Przekaska

}