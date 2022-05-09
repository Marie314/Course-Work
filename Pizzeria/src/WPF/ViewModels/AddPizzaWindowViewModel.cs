using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using Microsoft.Win32;
using Pizzeria.Application.Common.DTOs;
using Pizzeria.Application.Services;
using WPF.Common.Factories;
using WPF.Common.Models;
using WPF.Models;

namespace WPF.ViewModels
{
    internal class AddPizzaWindowViewModel : Screen
    {
        private readonly IPizzaService _pizzaService;
        private readonly IEventAggregator _eventAggregator;
        private readonly SaveImageOptions _saveImageOptions;
        private PizzaModel _pizza;

        public AddPizzaWindowViewModel(
            IPizzaService pizzaService, IEventAggregator eventAggregator,
            SaveImageOptions saveImageOptions, PizzaModel pizza)
        {
            _pizzaService = pizzaService;
            _eventAggregator = eventAggregator;
            _saveImageOptions = saveImageOptions;
            _pizza = pizza;
        }

        public PizzaModel Pizza
        {
            get => _pizza;
        }
        
        public async Task AddPizza()
        {
            await _pizzaService.AddPizzaAsync(new PizzaDto
            {
                Description = _pizza.Description,
                ImagePath = _pizza.ImagePath,
                Name = _pizza.Name,
                TotalPrice = _pizza.Price,
            });

            await _eventAggregator.PublishOnUIThreadAsync(_pizza);

            await TryCloseAsync();
        }

        public void Browse()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = _saveImageOptions.MultiSelect;
            openFileDialog.Filter = _saveImageOptions.FileExtensionsFilter;

            var result = openFileDialog.ShowDialog();

            if (result == true)
            {
                var destFileName = Path.Combine($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent}", $"{_saveImageOptions.ImagesFolderPath}", openFileDialog.SafeFileName);

                File.Copy(openFileDialog.FileName, destFileName);

                _pizza.ImagePath = Path.Combine(_saveImageOptions.ImagesFolderPath, openFileDialog.SafeFileName);
            }
        }
    }
}
