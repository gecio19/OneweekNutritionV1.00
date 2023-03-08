namespace OneweekNutrition.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public Double PPM { get; set; } // chyba zapotrzebowanie kaloryczne

        public List<UserRecipe> UserRecipe { get; set; }

    }
}
