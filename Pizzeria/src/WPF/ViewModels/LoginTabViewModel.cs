using System;
using Caliburn.Micro;
using Pizzeria.Application.Services;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Windows;
using Pizzeria.Application.Common.Responses;
using Pizzeria.Domain.Entities;
using Pizzeria.Domain.Enums;
using WPF.Common.Factories;
using WPF.Models;
using WPF.Views;
using Role = Pizzeria.Domain.Enums.Role;

namespace WPF.ViewModels
{
    internal class LoginTabViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly IAccountService _accountService;
        private readonly IViewModelFactory _viewModelFactory;
        private LoginModel _loginModel;

        public LoginTabViewModel(LoginModel loginModel, IAccountService accountService, IWindowManager windowManager, IViewModelFactory viewModelFactory)
        {
            _loginModel = loginModel;
            _accountService = accountService;
            _windowManager = windowManager;
            _viewModelFactory = viewModelFactory;
        }

        public LoginModel LoginModel
        {
            get => _loginModel;
            set => _loginModel = value;
        }

        public async Task Login()
        {
            var response = await  _accountService.Login(_loginModel.UserName, _loginModel.Password);

            if (response.Status == OperationStatus.Success)
            {
                var role = response.Result.RoleName;

                ObjectCache cache = MemoryCache.Default;
                cache.Set("Role", role, ObjectCache.InfiniteAbsoluteExpiration);
                cache.Set("UserId", response.Result.Id, ObjectCache.InfiniteAbsoluteExpiration);

                if (role == Enum.GetName(typeof(Role), Role.User))
                {
                    await _windowManager.ShowWindowAsync(_viewModelFactory.Create<CatalogWindowViewModel>());
                } else if (role == Enum.GetName(typeof(Role), Role.Administrator))
                {
                    await _windowManager.ShowWindowAsync(_viewModelFactory.Create<AdministratorWindowViewModel>());
                }

                await ((Screen)Parent).TryCloseAsync();
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }
    }
}
