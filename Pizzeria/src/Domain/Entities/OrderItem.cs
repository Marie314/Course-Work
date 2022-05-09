using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Domain.Entities
{
    [Table("OrderItems")]
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int PizzaId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string PizzaName { get; set; }
    }
}
