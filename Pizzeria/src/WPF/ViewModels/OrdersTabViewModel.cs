using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Extensions.Configuration;
using Pizzeria.Application.Common;
using Pizzeria.Application.Services;
using WPF.Common.Factories;
using WPF.Common.Helpers;
using WPF.Models;
using WPF.Views;

namespace WPF.ViewModels
{
    internal class OrdersTabViewModel : Screen
    {
        private readonly IAdministratorService _administratorService;
        private readonly IWindowManager _windowManager;
        private readonly IViewModelFactory _viewModelFactory;

        private bool _filterByPrice;
        private bool _filterByDate;

        public OrdersTabViewModel(IAdministratorService administratorService, IWindowManager windowManager, IViewModelFactory viewModelFactory)
        {
            _administratorService = administratorService;
            _windowManager = windowManager;
            _viewModelFactory = viewModelFactory;
        }

        public override event PropertyChangedEventHandler PropertyChanged;

        public bool FilterByPrice
        {
            get => _filterByPrice;
            set => _filterByPrice = value;
        }

        public bool FilterByDate
        {
            get => _filterByDate;
            set => _filterByDate = value;
        }

        public List<OrderDetailedModel> Orders
        {
            get
            {
                var orders = _administratorService.GetOrders(_filterByDate, _filterByPrice).Select(order => new OrderDetailedModel
                {
                    UserName = order.UserName,
                    Address = order.Address,
                    Description = order.Description,
                    Price = order.TotalPrice,
                    OrderDate = order.OrderDate,
                });

                return orders.ToList();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public void SetFilterByDate()
        {
            _filterByDate = true;
            _filterByPrice = false;

            OnPropertyChanged(nameof(Orders));
        }

        public void SetFilterByPrice()
        {
            _filterByPrice = true;
            _filterByDate = false;

            OnPropertyChanged(nameof(Orders));
        }

        public async Task GenerateReport()
        {
            if (!_windowManager.IsWindowOpen<GenerateReportWindowView>())
            {
                await _windowManager.ShowWindowAsync(_viewModelFactory.Create<GenerateReportWindowViewModel>());
            }
        }
    }
}
