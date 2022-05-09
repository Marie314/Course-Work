using Caliburn.Micro;
using Pizzeria.Application.Services;
using System.Collections.Generic;
using System.Linq;
using WPF.Common.Factories;
using WPF.Common.Helpers;
using WPF.Models;
using WPF.Views;

namespace WPF.ViewModels
{
    internal class CatalogWindowViewModel : Screen
    {
        private CreateOrderWindowViewModel _createOrderWindowViewModel;
        private PizzaModel _pizza;

        private readonly IPizzaService _pizzaService;
        private readonly IWindowManager _windowManager;
        private readonly IViewModelFactory _viewModelFactory;

        public CatalogWindowViewModel(IPizzaService pizzaService, IWindowManager windowManager, IViewModelFactory viewModelFactory)
        {
            _windowManager = windowManager;
            _pizzaService = pizzaService;
            _viewModelFactory = viewModelFactory;
        }

        public PizzaModel SelectedPizza
        {
            get => _pizza;
            set
            {
                _pizza = value;
                if (!_windowManager.IsWindowOpen<CreateOrderWindowView>())
                {
                    _createOrderWindowViewModel = _viewModelFactory.Create<CreateOrderWindowViewModel>();
                    _windowManager.ShowWindowAsync(_createOrderWindowViewModel).GetAwaiter().GetResult();
                }

                _createOrderWindowViewModel.AddPizzaToOrder(_pizza);
            }
        }

        public List<PizzaModel> Pizzas
        {
            get
            {
                var pizzas = _pizzaService.GetPizzas().Select(pizza => new PizzaModel
                {
                    Id = pizza.Id,
                    Description = pizza.Description,
                    ImagePath = pizza.ImagePath,
                    Name = pizza.Name,
                    Price = pizza.TotalPrice,
                });

                return pizzas.ToList();
            }
        }
    }
}
