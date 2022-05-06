using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Domain.Entities
{
    [Table("Pizzas")]
    public class Pizza
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SizeId { get; set; }

        public int TotalPrice { get; set; }
    }
}
