using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using WPF.Common.Helpers;

namespace WPF.Models
{
    internal class LoginModel : INotifyPropertyChanged
    {
        private string _userName;
        private string _password;

        public event PropertyChangedEventHandler PropertyChanged;

        [Required(ErrorMessage = "Must not be empty")]
        [MinLength(5, ErrorMessage = "Min user name length is 5")]
        [MaxLength(25, ErrorMessage = "Max user name length is 25")]
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;

                OnPropertyChanged(nameof(UserName));
                OnPropertyChanged(nameof(CanClick));

                ValidationHelper.ValidateProperty(value, nameof(UserName), this);
            }
        }

        [Required(ErrorMessage = "Must not be empty")]
        [MinLength(6, ErrorMessage = "Min password length is 6")]
        [MaxLength(25, ErrorMessage = "Max password length is 25")]
        public string Password
        {
            get => _password;
            set
            {
                _password = value;


                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanClick));

                ValidationHelper.ValidateProperty(value, nameof(Password), this);
            }
        }

        public bool CanClick => ValidationHelper.Validate(this);

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
