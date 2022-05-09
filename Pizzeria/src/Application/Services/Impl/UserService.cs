using Pizzeria.Application.Common.DTOs;
using Pizzeria.Domain.Entities;
using Pizzeria.Infrastructure.Repositories;

namespace Pizzeria.Application.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Pizza> _pizzaRepository;

        public UserService(IRepository<Order> orderRepository, IRepository<OrderItem> orderItemRepository, IRepository<User> userRepository, IRepository<Pizza> pizzaRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _userRepository = userRepository;
            _pizzaRepository = pizzaRepository;
        }

        public async Task CreateOrder(OrderDto order)
        {
            var user = await _userRepository.GetByIdAsync(order.UserId);

            var addedOrder = await _orderRepository.AddAsync(new Order
            {
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Description = order.Description,
                Address = order.Address,
                UserName = user.Name,
            });

            foreach (var orderItem in order.OrderItems)
            {
                var pizza = await _pizzaRepository.GetByIdAsync(orderItem.PizzaId);

                await _orderItemRepository.AddAsync(new OrderItem
                {
                    OrderId = addedOrder.Id,
                    PizzaId = orderItem.PizzaId,
                    Price = orderItem.Price,
                    Quantity = orderItem.Quantity,
                    PizzaName = pizza.Name,
                });
            }
        }
    }
}
