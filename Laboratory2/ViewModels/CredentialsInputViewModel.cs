using Laboratory2.Models;
using Laboratory2.Navigation;
using Laboratory2.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Laboratory2.ViewModels
{
    internal class CredentialsInputViewModel : INavigatable
    {
        private Action _goToDateOfBirthInfo;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public bool IsEnabled { get { return false; } set { IsEnabled = value; } }

        private DateTime dateOfBirth = DateTime.Today;
        public DateTime DateOfBirth{
            get
            {
                return dateOfBirth;
            }
            set
            {
                dateOfBirth = value;
            }
        }

        private RelayCommand<object> acceptCommand;
        public RelayCommand<object> AcceptCommand
        {
            get
            {
                return acceptCommand ??= new RelayCommand<object>(o => GoToDateOfBirthInfo(), CanExecute);//Accept()
            }
        }

        public NavigationTypes ViewType
        {
            get
            {
                return NavigationTypes.CredentialsInput;
            }
        }

        public CredentialsInputViewModel(Action goToDateOfBirthInfo)
        {
            _goToDateOfBirthInfo = goToDateOfBirthInfo;
        }

        private bool CanExecute(Object o)
        {
            return !String.IsNullOrWhiteSpace(FirstName) && !String.IsNullOrWhiteSpace(LastName) && !String.IsNullOrWhiteSpace(Email);
        }

        public async void GoToDateOfBirthInfo()
        {
            int age = await Task.Run(() => Person.Age(DateOfBirth));
            if (age < 0)
                MessageBox.Show("Date of birth is invalid! Your age is < 0");
            else if(age >= 135)
                MessageBox.Show("Date of birth is invalid! Your age is >= 135");
            else
            {
                Person person = new Person(FirstName, LastName, Email, DateOfBirth);
                person.CalculateFields();
                PersonRelayAgent.ThePerson = person;
                _goToDateOfBirthInfo.Invoke();
            }
        }
    }
}
