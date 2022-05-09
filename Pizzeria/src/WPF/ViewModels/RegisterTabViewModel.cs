using System.Windows;
using Caliburn.Micro;
using Pizzeria.Application.Common.Responses;
using Pizzeria.Application.Services;
using WPF.Common.Factories;
using WPF.Models;

namespace WPF.ViewModels
{
    internal class RegisterTabViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly IAccountService _accountService;
        private readonly IViewModelFactory _viewModelFactory;
        private RegisterModel _registerModel;

        public RegisterTabViewModel(RegisterModel registerModel, IAccountService accountService, IWindowManager windowManager, IViewModelFactory viewModelFactory)
        {
            _registerModel = registerModel;
            _accountService = accountService;
            _windowManager = windowManager;
            _viewModelFactory = viewModelFactory;
        }

        public RegisterModel RegisterModel
        {
            get => _registerModel;
            set => _registerModel = value;
        }

        public async void Register()
        {
            var result = await _accountService.Register(_registerModel.Name, _registerModel.UserName, _registerModel.Password);

            if (result.Status == OperationStatus.Success)
            {
                MessageBox.Show(result.Message);
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }
    }
}
