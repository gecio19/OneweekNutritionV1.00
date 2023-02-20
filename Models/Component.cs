using System.ComponentModel.DataAnnotations;

namespace OneweekNutrition.Models
{
    public class Component
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Podaj Nazwe")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Podaj ilosc")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid id greater than 0")]
        public float Protein { get; set; }

        [Required(ErrorMessage = "Podaj ilosc")]

        public float Carbohydrates { get; set; }


        [Required(ErrorMessage = "Podaj ilosc")]
        public float Calories { get; set; }


        [Required(ErrorMessage = "Wybierz rodzaj")]
        public Types Type { get; set; }

        public string? ImgPath { get; set; }
    }





    public enum Types
    {
        Veg,
        Meat,
        Fruit,
        Other

    }

}
