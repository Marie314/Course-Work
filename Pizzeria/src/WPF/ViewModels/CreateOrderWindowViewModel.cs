using Caliburn.Micro;
using Pizzeria.Application.Common.DTOs;
using Pizzeria.Application.Services;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using WPF.Models;

namespace WPF.ViewModels
{
    internal class CreateOrderWindowViewModel : Screen
    {
        private readonly IUserService _userService;
        private readonly IWindowManager _windowManager;

        private IObservableCollection<PizzaModel> _pizzas;
        private OrderModel _orderModel;

        private decimal _totalPrice;

        public CreateOrderWindowViewModel(IUserService userService, IWindowManager windowManager, OrderModel orderModel)
        {
            _userService = userService;
            _pizzas = new BindableCollection<PizzaModel>();
            _orderModel = orderModel;
        }

        public override event PropertyChangedEventHandler PropertyChanged;

        public IObservableCollection<PizzaModel> Pizzas
        {
            get => _pizzas;
        }

        public decimal TotalPrice
        {
            get => _totalPrice;
            set
            {
                _totalPrice = value;
                OnPropertyChanged(nameof(FormattedTotalPrice));
            }
        }

        public OrderModel SelectedOrder
        {
            get => _orderModel;
            set
            {
                _orderModel = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }

        public string FormattedTotalPrice
        {
            get => String.Format($"Total Price: {_totalPrice} BYN");
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public void AddPizzaToOrder(PizzaModel pizza)
        {
            _pizzas.Add(pizza);
            TotalPrice = _pizzas.Select(pizza => pizza.Price).Sum();
        }

        public async Task RemovePizzaFromOrder(PizzaModel pizza)
        {
            _pizzas.Remove(pizza);

            if (_pizzas.Count == 0)
            {
                await TryCloseAsync();
            }
        }

        public async Task Confirm()
        {
            ObjectCache cache = MemoryCache.Default;
            var userId = (int)cache.Get("UserId");

            var order = new OrderDto
            {
                UserId = userId,
                TotalPrice = _totalPrice,
                Address = _orderModel.Address,
                Description = _orderModel.Description,
                OrderDate = DateTimeOffset.Now,
            };

            var duplicatePizzas = _pizzas.GroupBy(pizza => pizza.Id).Where(group => group.Any());

            foreach (var pizza in duplicatePizzas)
            {
                order.OrderItems.Add(new OrderItemDto
                {
                    PizzaId = pizza.Key,
                    Price = pizza.ToList().Select(pizza => pizza.Price).Sum(),
                    Quantity = pizza.ToList().Count,
                });
            }

            await _userService.CreateOrder(order);

            await TryCloseAsync();

            MessageBox.Show("ORDER WAS CREATED.");
        }
    }
}
