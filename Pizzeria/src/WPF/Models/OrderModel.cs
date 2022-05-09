using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using WPF.Common.Helpers;

namespace WPF.Models
{
    internal class OrderModel : INotifyPropertyChanged
    {
        private string _address;
        private string _description;

        public event PropertyChangedEventHandler PropertyChanged;


        [Required(ErrorMessage = "Must not be empty")]
        [MinLength(5, ErrorMessage = "Min length is 5")]
        [MaxLength(128, ErrorMessage = "Max length is 128")]
        public string Address
        {
            get => _address;
            set
            {
                _address = value;


                OnPropertyChanged(nameof(CanClick));
                OnPropertyChanged(nameof(Address));

                ValidationHelper.ValidateProperty(value, nameof(Address), this);
            }
        }

        [Required(ErrorMessage = "Must not be empty")]
        [MinLength(5, ErrorMessage = "Min length is 5")]
        [MaxLength(128, ErrorMessage = "Max length is 128")]
        public string Description
        {
            get => _description;
            set
            {
                _description = value;


                OnPropertyChanged(nameof(CanClick));
                OnPropertyChanged(nameof(Description));

                ValidationHelper.ValidateProperty(value, nameof(Description), this);
            }
        }

        public bool CanClick => ValidationHelper.Validate(this);

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
