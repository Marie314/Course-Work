namespace Pizzeria.Application.Common.DTOs
{
    public class OrderDto
    {
        public OrderDto()
        {
            OrderItems = new List<OrderItemDto>();
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public IList<OrderItemDto> OrderItems { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTimeOffset OrderDate { get; set; }
    }
}
