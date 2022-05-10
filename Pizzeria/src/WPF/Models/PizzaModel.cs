using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.CompilerServices;
using WPF.Common.Helpers;

namespace WPF.Models
{
    internal class PizzaModel : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _description;
        private string _imagePath;
        private decimal _price;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        [Required(ErrorMessage = "Must not be empty.")]
        [MinLength(6, ErrorMessage = "Min user name length is 6.")]
        [MaxLength(100, ErrorMessage = "Max user name length is 100.")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;

                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(CanClick));

                ValidationHelper.ValidateProperty(value, nameof(Name), this);
            }
        }

        [Required(ErrorMessage = "Must not be empty.")]
        [MinLength(6, ErrorMessage = "Min description length is 6.")]
        [MaxLength(100, ErrorMessage = "Max description length is 100.")]
        public string Description
        {
            get => _description;
            set
            {
                _description = value;

                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(CanClick));

                ValidationHelper.ValidateProperty(value, nameof(Description), this);
            }
        }

        [Required(ErrorMessage = "Must not be empty.")]
        public string ImagePath
        {
            get => Path.Combine($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent}", _imagePath);
            
            set
            {
                _imagePath = value;

                OnPropertyChanged(nameof(ImagePath));
                OnPropertyChanged(nameof(CanClick));

                ValidationHelper.ValidateProperty(value, nameof(ImagePath), this);
            }
        }

        [Required(ErrorMessage = "Must not be empty.")]
        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;

                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(CanClick));

                ValidationHelper.ValidateProperty(value, nameof(Price), this);
            }
        }

        public string FormattedPrice
        {
            get => _price + " BYN";
        }

        public bool CanClick => ValidationHelper.Validate(this);

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
