using Pizzeria.Application.Services;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using WPF.Models;

namespace WPF.ViewModels
{
    internal class CatalogTabViewModel : Screen
    {
        private readonly IPizzaService _pizzaService;

        private PizzaModel _pizza;

        public CatalogTabViewModel(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        public PizzaModel SelectedPizza
        {
            get => _pizza;
            set
            {
                _pizza = value;
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
