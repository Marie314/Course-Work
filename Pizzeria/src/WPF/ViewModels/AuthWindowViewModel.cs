using Caliburn.Micro;
using System.Threading;
using System.Threading.Tasks;
using WPF.Common.Factories;

namespace WPF.ViewModels
{
    internal class AuthWindowViewModel : Conductor<Screen>.Collection.AllActive
    {
        private readonly IViewModelFactory _viewModelFactory;

        public AuthWindowViewModel(
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
            Items.Add(_viewModelFactory.Create<LoginTabViewModel>());
            Items[0].DisplayName = "SIGN IN";
            Items.Add(_viewModelFactory.Create<RegisterTabViewModel>());
            Items[1].DisplayName = "SIGN UP";
            Items.Add(_viewModelFactory.Create<CatalogTabViewModel>());
            Items[2].DisplayName = "CATALOG";
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);
        }
    }
}
