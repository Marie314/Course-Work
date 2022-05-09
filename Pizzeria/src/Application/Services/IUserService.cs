using Pizzeria.Application.Common.DTOs;

namespace Pizzeria.Application.Services
{
    /// <summary>
    /// Represents a wrapper, in other words - service, for customer operations, such as "Order Creation".
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Creates a customer order, which contains all the necessary info, i.e
        ///  OrderDate, UserId, OrderItems, etc.
        /// </summary>
        /// <param name="order">The order to be processed.</param>
        public Task CreateOrder(OrderDto order);
    }
}
