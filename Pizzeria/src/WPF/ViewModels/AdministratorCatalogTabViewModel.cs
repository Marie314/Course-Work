using Caliburn.Micro;
using Pizzeria.Application.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using WPF.Common.Factories;
using WPF.Common.Helpers;
using WPF.Models;
using WPF.Views;

namespace WPF.ViewModels
{
    internal class AdministratorCatalogTabViewModel : Screen, IHandle<object>
    {
        private UpdatePizzaWindowViewModel _updatePizzaWindowViewModel;
        private PizzaModel _pizza;
        private IObservableCollection<PizzaModel> _pizzas;

        private readonly IPizzaService _pizzaService;
        private readonly IWindowManager _windowManager;
        private readonly IViewModelFactory _viewModelFactory;

        public AdministratorCatalogTabViewModel(IPizzaService pizzaService, IWindowManager windowManager, IViewModelFactory viewModelFactory, IEventAggregator eventAggregator)
        {
            _windowManager = windowManager;
            _pizzaService = pizzaService;
            _viewModelFactory = viewModelFactory;
            eventAggregator.SubscribeOnUIThread(this);
        }

        public override event PropertyChangedEventHandler PropertyChanged;

        public PizzaModel SelectedPizza
        {
            get => _pizza;
            set
            {
                if (value != null)
                {
                    _pizza = value;
                    if (!_windowManager.IsWindowOpen<UpdatePizzaWindowView>())
                    {
                        _updatePizzaWindowViewModel = _viewModelFactory.Create<UpdatePizzaWindowViewModel>();
                        _windowManager.ShowWindowAsync(_updatePizzaWindowViewModel).GetAwaiter().GetResult();
                    }

                    _updatePizzaWindowViewModel.SetPizza(_pizza);
                }
            }
        }

        public IObservableCollection<PizzaModel> Pizzas
        {
            get
            {
                var bindableCollection = new BindableCollection<PizzaModel>();
                var pizzas = _pizzaService.GetPizzas();

                foreach (var pizza in pizzas)
                {
                    bindableCollection.Add(new PizzaModel
                    {
                        Id = pizza.Id,
                        Description = pizza.Description,
                        ImagePath = pizza.ImagePath,
                        Name = pizza.Name,
                        Price = pizza.TotalPrice,
                    });
                }

                return bindableCollection;
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public async Task AddPizza()
        {
            if (!_windowManager.IsWindowOpen<AddPizzaWindowView>())
            {
                await _windowManager.ShowWindowAsync(_viewModelFactory.Create<AddPizzaWindowViewModel>());
            }
        }

        public async Task HandleAsync(object message, CancellationToken cancellationToken)
        {
            OnPropertyChanged(nameof(Pizzas));

            await Task.FromResult(true);
        }
    }
}
