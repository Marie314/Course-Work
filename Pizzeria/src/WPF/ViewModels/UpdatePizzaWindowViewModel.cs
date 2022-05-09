using Caliburn.Micro;
using Pizzeria.Application.Common.DTOs;
using Pizzeria.Application.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WPF.Models;

namespace WPF.ViewModels
{
    internal class UpdatePizzaWindowViewModel : Screen
    {
        private readonly IPizzaService _pizzaService;
        private readonly IEventAggregator _eventAggregator;
        private PizzaModel _pizza;

        public UpdatePizzaWindowViewModel(IPizzaService pizzaService, IEventAggregator eventAggregator)
        {
            _pizzaService = pizzaService;
            _eventAggregator = eventAggregator;
        }

        public override event PropertyChangedEventHandler PropertyChanged;

        public PizzaModel Pizza
        {
            get => _pizza;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public void SetPizza(PizzaModel pizza)
        {
            _pizza = pizza;
            OnPropertyChanged(nameof(Pizza));
        }

        public async Task RemovePizza()
        {
            await _pizzaService.RemovePizzaAsync(new PizzaDto
            {
                Id = _pizza.Id,
                Description = _pizza.Description,
                ImagePath = _pizza.ImagePath,
                Name = _pizza.Name,
                TotalPrice = _pizza.Price,
            });

            await _eventAggregator.PublishOnUIThreadAsync(_pizza);

            await TryCloseAsync();
        }

        public async Task UpdatePizza()
        {
            await _pizzaService.UpdatePizzaAsync(new PizzaDto
            {
                Id = _pizza.Id,
                Description = _pizza.Description,
                ImagePath = _pizza.ImagePath,
                Name = _pizza.Name,
                TotalPrice = _pizza.Price,
            });

            await TryCloseAsync();
        }
    }
}
