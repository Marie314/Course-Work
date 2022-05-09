using FluentAssertions;
using Moq;
using NUnit.Framework;
using Pizzeria.Application.Common.DTOs;
using Pizzeria.Application.Services.Impl;
using Pizzeria.Domain.Entities;
using Pizzeria.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pizzeria.Unit.Tests.Services
{
    [TestFixture]
    internal class UserServiceTests
    {
        [Test]
        public async Task CreateOrder_WhenCreatingOrder_ShouldGetListWithCreatedOrder()
        {
            // Arrange
            var orders = new List<Order>();
            var userId = 1;

            var orderDto = new OrderDto
            {
                Description = "TestOrderDescription",
                Address = "TestOrderAddress",
                OrderDate = new DateTimeOffset(new DateTime(2022, 8, 5, 12, 0, 0)),
                TotalPrice = 60,
                UserName = "TestName",
                UserId = userId,
                OrderItems = new List<OrderItemDto>(),
            };

            var orderRepository = new Mock<IRepository<Order>>();
            orderRepository.Setup(stub => stub.AddAsync(It.IsAny<Order>())).ReturnsAsync(new Order
            {
                Description = orderDto.Description,
                Address = orderDto.Address,
                OrderDate = orderDto.OrderDate,
                TotalPrice = orderDto.TotalPrice,
                UserName = orderDto.UserName,
                UserId = orderDto.UserId,
            }).Callback<Order>(order =>
            {
                orders.Add(order);
            });

            var userRepository = new Mock<IRepository<User>>();
            userRepository.Setup(stub => stub.GetByIdAsync(userId)).ReturnsAsync(new User
            {
                Id = userId,
                Name = "TestName",
            });

            var orderItemRepository = new Mock<IRepository<OrderItem>>();
            var pizzaRepository = new Mock<IRepository<Pizza>>();

            var userService = new UserService(orderRepository.Object, orderItemRepository.Object, userRepository.Object, pizzaRepository.Object);

            // Act
            await userService.CreateOrder(orderDto);

            // Assert
            orders.Should().BeEquivalentTo(new List<Order>
            {
                new Order
                {
                    UserId = userId,
                    Address = "TestOrderAddress",
                    OrderDate = new DateTimeOffset(new DateTime(2022, 8, 5, 12, 0, 0)),
                    TotalPrice = 60,
                    UserName = "TestName",
                    Description = "TestOrderDescription",
                }
            });
        }

        [Test]
        public async Task CreateOrder_WhenCreatingOrder_ShouldGetListWithCreatedOrderItems()
        {
            // Arrange
            var orderItems = new List<OrderItem>();
            var userId = 1;

            var firstTestPizza = new Pizza { Description = "TestPizzaDescription1", Id = 1, ImagePath = "Test/Image/Path", Name = "TestPizzaName1", TotalPrice = 30 };
            var secondTestPizza = new Pizza { Description = "TestPizzaDescription2", Id = 2, ImagePath = "Test/Image/Path", Name = "TestPizzaName2", TotalPrice = 30 };

            var orderDto = new OrderDto
            {
                Description = "TestOrderDescription",
                Address = "TestOrderAddress",
                OrderDate = new DateTimeOffset(new DateTime(2022, 8, 5, 12, 0, 0)),
                TotalPrice = 60,
                UserName = "TestName",
                UserId = userId,
                OrderItems = new List<OrderItemDto>
                {
                    new OrderItemDto { PizzaId = firstTestPizza.Id, PizzaName = firstTestPizza.Name, Price = 30, Quantity = 1 },
                    new OrderItemDto { PizzaId = secondTestPizza.Id, PizzaName = secondTestPizza.Name, Price = 30, Quantity = 1 },
                },
            };

            var orderRepository = new Mock<IRepository<Order>>();
            orderRepository.Setup(stub => stub.AddAsync(It.IsAny<Order>())).ReturnsAsync(new Order
            {
                Id = 1,
                Description = orderDto.Description,
                Address = orderDto.Address,
                OrderDate = orderDto.OrderDate,
                TotalPrice = orderDto.TotalPrice,
                UserName = orderDto.UserName,
                UserId = orderDto.UserId,
            });

            var userRepository = new Mock<IRepository<User>>();
            userRepository.Setup(stub => stub.GetByIdAsync(userId)).ReturnsAsync(new User
            {
                Id = userId,
                Name = "TestName",
            });

            var orderItemRepository = new Mock<IRepository<OrderItem>>();
            orderItemRepository.Setup(stub => stub.AddAsync(It.IsAny<OrderItem>())).Callback<OrderItem>(orderItem => orderItems.Add(orderItem));

            var pizzaRepository = new Mock<IRepository<Pizza>>();
            pizzaRepository.Setup(stub => stub.GetByIdAsync(firstTestPizza.Id)).ReturnsAsync(firstTestPizza);
            pizzaRepository.Setup(stub => stub.GetByIdAsync(secondTestPizza.Id)).ReturnsAsync(secondTestPizza);

            var userService = new UserService(orderRepository.Object, orderItemRepository.Object, userRepository.Object, pizzaRepository.Object);

            // Act
            await userService.CreateOrder(orderDto);

            // Assert
            orderItems.Should().BeEquivalentTo(new List<OrderItem>
            {
                new OrderItem { OrderId = 1, PizzaId = firstTestPizza.Id, PizzaName = firstTestPizza.Name, Price = 30, Quantity = 1 },
                new OrderItem { OrderId = 1, PizzaId = secondTestPizza.Id, PizzaName = secondTestPizza.Name, Price = 30, Quantity = 1 },
            });
        }
    }
}
