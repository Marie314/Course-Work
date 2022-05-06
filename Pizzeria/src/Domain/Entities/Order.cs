using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Domain.Entities
{
    [Table("Orders")]
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
