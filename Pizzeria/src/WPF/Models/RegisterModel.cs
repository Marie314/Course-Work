using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using WPF.Common.Helpers;

namespace WPF.Models
{
    internal class RegisterModel : INotifyPropertyChanged
    {
        private string _userName;
        private string _name;
        private string _password;
        private string _confirmedPassword;

        public event PropertyChangedEventHandler PropertyChanged;


        [Required(ErrorMessage = "Must not be empty.")]
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


        [Required(ErrorMessage = "Name is required.")]
        [MinLength(2, ErrorMessage = "Min name length is 2")]
        [MaxLength(25, ErrorMessage = "Max name length is 25")]
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

        [Required(ErrorMessage = "Password required")]
        [MinLength(6, ErrorMessage = "Min password length is 6")]
        [MaxLength(25, ErrorMessage = "Max password length is 25")]
        [Display(Name = "Password")]
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

        [Required(ErrorMessage = "Confirmed password required")]
        [Compare("Password", ErrorMessage = "Paswords do not match")]
        [Display(Name = "Confirm password")]
        public string ConfirmedPassword
        {
            get => _confirmedPassword;

            set
            {
                _confirmedPassword = value;

                OnPropertyChanged(nameof(ConfirmedPassword));
                OnPropertyChanged(nameof(CanClick));

                ValidationHelper.ValidateProperty(value, nameof(ConfirmedPassword), this);
            }
        }

        public bool CanClick => ValidationHelper.Validate(this);

        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
