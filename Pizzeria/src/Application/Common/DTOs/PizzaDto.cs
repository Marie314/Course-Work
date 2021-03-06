namespace Pizzeria.Application.Common.DTOs
{
    public class PizzaDto
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
