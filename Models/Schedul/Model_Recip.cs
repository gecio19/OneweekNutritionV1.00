namespace OneweekNutrition.Models.Schedul
{
    public class Model_Recip
    {
        public string Name { get; set; }

        public string description { get; set; }

        public List<Component> components { get; set; }
        public List<int> weights { get; set; }




    }
}
