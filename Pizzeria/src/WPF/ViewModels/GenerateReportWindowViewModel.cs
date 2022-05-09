using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Extensions.Configuration;
using Pizzeria.Application.Services;
using WPF.Models;

namespace WPF.ViewModels
{
    internal class GenerateReportWindowViewModel : Screen
    {
        private readonly IAdministratorService _administratorService;
        private readonly string _reportFileName;

        private ReportModel _reportModel;

        public GenerateReportWindowViewModel(IAdministratorService administratorService, IConfiguration configuration, ReportModel reportModel)
        {
            _administratorService = administratorService;
            _reportModel = reportModel;
            _reportFileName = configuration.GetSection("FileNames:ReportFileName").Value;
        }

        public ReportModel ReportModel
        {
            get => _reportModel;
            set => _reportModel = value;
        }

        public async Task Generate()
        {
            var report = _administratorService.GenerateReport(_reportModel.StartDate, _reportModel.EndDate);

            await File.WriteAllTextAsync(_reportFileName, report);

            await TryCloseAsync();
            MessageBox.Show("Report was generated.");
        }
    }
}
