using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Domain.Entities
{
    [Table("Pizzas")]
    public class Pizza
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
