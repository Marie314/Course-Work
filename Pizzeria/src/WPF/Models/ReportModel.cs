using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using WPF.Common.Helpers;

namespace WPF.Models
{
    internal class ReportModel : INotifyPropertyChanged
    {
        private DateTimeOffset _startDate;
        private DateTimeOffset _endDate;

        public event PropertyChangedEventHandler PropertyChanged;

        [Required(ErrorMessage = "Must not be empty.")]
        [DataType(DataType.DateTime, ErrorMessage = "Incorrect date format")]
        public DateTimeOffset StartDate
        {
            get => _startDate;
            set
            {
                OnPropertyChanged(nameof(StartDate));
                OnPropertyChanged(nameof(CanClick));

                ValidationHelper.ValidateProperty(value, nameof(StartDate), this);
                _startDate = value;
            }
        }

        [Required(ErrorMessage = "Must not be empty.")]
        [DataType(DataType.DateTime, ErrorMessage = "Incorrect date format")]
        public DateTimeOffset EndDate
        {
            get => _endDate;
            set
            {
                OnPropertyChanged(nameof(EndDate));
                OnPropertyChanged(nameof(CanClick));

                ValidationHelper.ValidateProperty(value, nameof(EndDate), this);
                _endDate = value;
            }
        }

        public bool CanClick => ValidationHelper.Validate(this);

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
