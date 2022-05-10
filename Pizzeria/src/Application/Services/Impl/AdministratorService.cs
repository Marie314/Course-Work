using System.Text;
using Pizzeria.Application.Common.DTOs;
using Pizzeria.Application.Common.ReportModels;
using Pizzeria.Domain.Entities;
using Pizzeria.Infrastructure.Repositories;

namespace Pizzeria.Application.Services.Impl
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;

        public AdministratorService(IRepository<Order> orderRepository, IRepository<OrderItem> orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        public string GenerateReport(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            var filteredOrders = _orderRepository.GetAll().Where(order => order.OrderDate.Date >= fromDate.Date && order.OrderDate.Date <= toDate.Date).ToList();

            var revenue = filteredOrders.Select(order => order.TotalPrice).Sum();
            var daysRevenue = filteredOrders.GroupBy(order => order.OrderDate.Date).Select(group => new DayRevenue
            {
                Date = group.Key,
                Revenue = group.Select(order => order.TotalPrice).Sum(),
            });

            var orderItems = _orderItemRepository.GetAll();

            var filteredOrderDtos = filteredOrders.Select(order => new OrderDto
            {
                OrderItems = orderItems.Where(orderItem => orderItem.OrderId == order.Id).Select(
                    orderItemDto => new OrderItemDto
                    {
                        PizzaId = orderItemDto.PizzaId,
                        Price = orderItemDto.Price,
                        Quantity = orderItemDto.Quantity,
                        PizzaName = orderItemDto.PizzaName,
                    }).ToList()
            }).ToList();

            var filteredOrderItemDtos = new List<OrderItemDto>();
            filteredOrderDtos.ForEach(orderDto => filteredOrderItemDtos.AddRange(orderDto.OrderItems));

            var pizzasRevenue = filteredOrderItemDtos.GroupBy(orderItem => orderItem.PizzaName).Select(group =>
                new PizzaRevenue
                {
                    PizzaName = group.Key,
                    PurchasesNumber = group.Select(orderItem => orderItem.Quantity).Sum(),
                    Revenue = group.Select(orderItem => orderItem.Price).Sum(),
                });

            var report = new StringBuilder();

            report.Append("<div class=\"container\" style=\"width:1500px; height:800px;\">")
                .Append("<div class=\"days-revenue-header\" style=\"border:1px solid; float:right; border-bottom:none; width: 30.6%; height:121px; text-align:center; display:grid; align-content:center;\">DAYS REVENUE</div>")
                .Append($"<div class=\"revenue-header\" style=\"border:1px solid; width: 34%; height:121px; float: left; text-align:center; align-content:center; display:grid;\">TOTAL REVENUE - {revenue} BYN.</div>")
                .Append("<div class=\"pizzas-popularity-header\" style=\"border: 1px solid; float: left; width: 35%; height: 121px; text-align:center; display: grid; align-content:center;\"></div>");

            report.Append("<div class=\"pizzas\" style=\"border:1px solid; width: 34%; border-bottom:none; height:679px; float: left; display:block; text-align:center;\">");

            foreach (var pizzaRevenue in pizzasRevenue)
            {
                report.Append("<div class=\"pizza\" style=\"height:90px; width:100%; text-align:center; float:none; margin-top:31px;\">")
                    .Append($"<div class=\"Name\">NAME - {pizzaRevenue.PizzaName}</div>")
                    .Append($"<div class=\"Revenue\">TOTAL REVENUE - {pizzaRevenue.Revenue} BYN</div>")
                    .Append("</div>");
            }

            report.Append("</div>").Append("<div class=\"days-revenue\" style=\"border:1px solid; float: right; width: 30.6%; height:679px; margin-bottom:100px; display:block; text-align:center;\">");

            foreach (var dayRevenue in daysRevenue)
            {
                report.Append($"<div class=\"day-revenue\" style=\"height:50px; width:100%; text-align: center; float:none; margin-top:31px;\">{dayRevenue.Date} - {dayRevenue.Revenue}</div>");
            }
            report.Append("</div>").Append("<div class=\"pizzas-popularity\" style=\"border:1px solid; border-bottom:none; float:right; width: 35%; height:679px; margin-bottom:100px; display:block; text-align:center;\">");

            var orderedPizzas = pizzasRevenue.OrderBy(pizza => pizza.Revenue);

            foreach (var pizza in orderedPizzas)
            {
                report.Append("<div class=\"pizza\" style=\"height:90px; width:100%; text-align:center; float:none; margin-top:31px;\">")
                    .Append($"<div class=\"Name\">NAME - {pizza.PizzaName}</div>")
                    .Append($"<div class=\"Revenue\">TOTAL AMOUNT - {pizza.PurchasesNumber} BYN</div>");
            }

            report.Append("</div></div>");
            
            return report.ToString();
        }

        public IEnumerable<OrderDto> GetOrders(bool filterByDate, bool filterByPrice)
        {
            var orders = _orderRepository.GetAll().Select(order => new OrderDto
            {
                Id = order.Id,
                Address = order.Address,
                Description = order.Description,
                TotalPrice = order.TotalPrice,
                UserId = order.UserId,
                UserName = order.UserName,
                OrderDate = order.OrderDate,
                OrderItems = _orderItemRepository.GetAll().Where(orderItem => orderItem.OrderId == order.Id).Select(
                  orderItem => new OrderItemDto
                  {
                      PizzaId = orderItem.PizzaId,
                      Price = orderItem.Price,
                      Quantity = orderItem.Quantity,
                  }).ToList(),
            });


            if (filterByDate && filterByPrice)
            {
                orders = orders.OrderBy(order => order.TotalPrice).ThenBy(order => order.OrderDate);
            }
            else if (filterByPrice)
            {
                orders = orders.OrderBy(order => order.TotalPrice).Reverse();
            }
            else if (filterByDate)
            {
                orders = orders.OrderBy(order => order.OrderDate);
            }

            return orders;
        }
    }
}
