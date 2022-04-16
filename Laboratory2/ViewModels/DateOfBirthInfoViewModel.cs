using Laboratory2.Models;
using System;
using Laboratory2.Tools;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Laboratory2.Navigation;

namespace Laboratory2.ViewModels
{
    internal class DateOfBirthInfoViewModel : INotifyPropertyChanged, INavigatable
    {
        private Action _goToCredentialsInput;

        public NavigationTypes ViewType
        {
            get
            {
                return NavigationTypes.DateOfBirthInfo;
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private RelayCommand<object> backCommand;
        public RelayCommand<object> BackCommand
        {
            get
            {
                return backCommand ??= new RelayCommand<object>(o => GoToCredentialsInput());
            }
        }
        public string FirstName
        {
            get => PersonRelayAgent.ThePerson.FirstName;
        }
        public string LastName
        {
            get => PersonRelayAgent.ThePerson.LastName;
        }

        public string? Email
        {
            get => PersonRelayAgent.ThePerson.Email;
        }

        public string DateOfBirth
        {
            get => PersonRelayAgent.ThePerson.DateOfBirth.ToLongDateString();
        }

        public bool IsAdult
        {
            get => PersonRelayAgent.ThePerson.IsAdult;
        }

        public string SunSign
        {
            get => PersonRelayAgent.ThePerson.SunSign;
        }
        public string ChineseSign
        {
            get => PersonRelayAgent.ThePerson.ChineseSign;
        }

        public string IsBirthday
        {
            get
            {
                if (PersonRelayAgent.ThePerson.IsBirthday)
                    return "Yes, congratulations!";
                return "No";
            } 
        }

        public DateOfBirthInfoViewModel(Action goToCredentialsInput)
        {
            _goToCredentialsInput = goToCredentialsInput;
        }

        public void GoToCredentialsInput()
        {
            _goToCredentialsInput.Invoke();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
