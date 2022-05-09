using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using WPF.Common.Factories;

namespace WPF.ViewModels
{
    internal class AdministratorWindowViewModel : Conductor<Screen>.Collection.AllActive
    {
        private readonly IViewModelFactory _viewModelFactory;

        public AdministratorWindowViewModel(
            IViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public async Task Close()
        {
            await TryCloseAsync();
        }

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);
            Items.Add(_viewModelFactory.Create<AdministratorCatalogTabViewModel>());
            Items[0].DisplayName = "CATALOG";
            Items.Add(_viewModelFactory.Create<OrdersTabViewModel>());
            Items[1].DisplayName = "ORDERS";
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);
        }
    }
}
