using Laboratory2.Exceptions;
using Laboratory2.Models;
using Laboratory2.Navigation;
using Laboratory2.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private bool CanExecute(Object o)
        {
            return !String.IsNullOrWhiteSpace(FirstName) && !String.IsNullOrWhiteSpace(LastName) && !String.IsNullOrWhiteSpace(Email);
        }

        public CredentialsInputViewModel(Action goToDateOfBirthInfo)
        {
            _goToDateOfBirthInfo = goToDateOfBirthInfo;
        }

        public void GoToDateOfBirthInfo()
        {
            Person person;
            try
            {
                person = new Person(FirstName, LastName, Email, DateOfBirth);
            }
            catch (InvalidEmailException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch(NegativeAgeException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch(TooOldExcpetion ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            person.CalculateFields();
            PersonRelayAgent.ThePerson = person;
            _goToDateOfBirthInfo.Invoke();
        }


    }
}
