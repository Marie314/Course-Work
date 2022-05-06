using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Domain.Entities
{
    [Table("PizzaToppings")]
    public class PizzaTopping
    {
        public int Id { get; set; }

        public int PizzaId { get; set;}

        public int ToppingId { get; set; }
    }
}
