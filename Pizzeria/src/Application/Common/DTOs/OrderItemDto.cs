namespace Pizzeria.Application.Common.DTOs
{
    public class OrderItemDto
    {
        public int PizzaId { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string PizzaName { get; set; }
    }
}
