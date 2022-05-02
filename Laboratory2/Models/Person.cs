using Laboratory2.Exceptions;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Laboratory2.Models
{
    internal class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }

        private DateTime dateOfBirth;
        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set => dateOfBirth = value;
        }

        private bool isAdult;
        public bool IsAdult
        {
            get
            {
                return isAdult;
            }
        }

        private string sunSign;
        public string SunSign
        {
            get
            {
                return sunSign;
            }
        }
        private string chineseSign;
        public string ChineseSign
        {
            get
            {
                return chineseSign;
            }
        }

        private bool isBirthday;
        public bool IsBirthday
        {
            get
            {
                return isBirthday;
            }
        }

        public Person(string firstName, string lastName, string? email, DateTime dateOfBirth)
        {
            if (email != null && !EmailIsValid(email))
                throw new InvalidEmailException("Email is invalid");
            if (Age(dateOfBirth) >= 135)
                throw new TooOldExcpetion("Your age is >= 135");
            if (Age(dateOfBirth) < 0)
                throw new NegativeAgeException("Your age is negative");
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
        }

        public Person(string firstName, string lastName, string email): this(firstName, lastName, email, DateTime.Today) { }

        public Person(string firstName, string lastName, DateTime dateOfBirth): this(firstName, lastName, null, dateOfBirth) { }

        public static int Age(DateTime dateOfBith)
        {
            if (DateTime.Today.Year == dateOfBith.Year)
            {
                if (DateTime.Today.Month > dateOfBith.Month || (DateTime.Today.Month == dateOfBith.Month && DateTime.Today.Day >= dateOfBith.Day))
                    return 0;
                return -1;
            }
            if (DateTime.Today.Month > dateOfBith.Month || (DateTime.Today.Month == dateOfBith.Month && DateTime.Today.Day >= dateOfBith.Day))
                return DateTime.Today.Year - dateOfBith.Year;
            return DateTime.Today.Year - dateOfBith.Year - 1;
        }

        public async Task CalculateFields()
        {
            var task1 = Task.Run(() => CalcIsAdult());
            var task2 = Task.Run(() => CalcSunSign());
            var task3 = Task.Run(() => CalcChineseSign());
            var task4 = Task.Run(() => CalcIsBirthDay());
            await task1;
            await task2;
            await task3;
            await task4;
        }

        private bool EmailIsValid(string email)
        {
            Regex regex = new Regex(@"\w+@\w+\.\w+");
            return regex.IsMatch(email);
        }

        private void CalcIsAdult()
        {
            isAdult = Age(dateOfBirth) >= 18;
        }

        private void CalcSunSign()
        {
            if (DateOfBirth.Month == 3 && DateOfBirth.Day >= 21 || DateOfBirth.Month == 4 && DateOfBirth.Day <= 20)
                    sunSign = "Aries ♈︎";
            else if (DateOfBirth.Month == 4 && DateOfBirth.Day >= 21 || DateOfBirth.Month == 5 && DateOfBirth.Day <= 20)
                sunSign = "Taurus ♉︎";
            else if (DateOfBirth.Month == 5 && DateOfBirth.Day >= 21 || DateOfBirth.Month == 6 && DateOfBirth.Day <= 21)
                sunSign = "Gemini ♊︎";
            else if (DateOfBirth.Month == 6 && DateOfBirth.Day >= 22 || DateOfBirth.Month == 7 && DateOfBirth.Day <= 22)
                sunSign = "Cancer ♋︎";
            else if (DateOfBirth.Month == 7 && DateOfBirth.Day >= 23 || DateOfBirth.Month == 8 && DateOfBirth.Day <= 22)
                sunSign = "Leo ♌︎";
            else if (DateOfBirth.Month == 8 && DateOfBirth.Day >= 23 || DateOfBirth.Month == 9 && DateOfBirth.Day <= 22)
                sunSign = "Virgo ♍︎";
            else if (DateOfBirth.Month == 9 && DateOfBirth.Day >= 23 || DateOfBirth.Month == 10 && DateOfBirth.Day <= 22)
                sunSign = "Libra ♎︎";
            else if (DateOfBirth.Month == 10 && DateOfBirth.Day >= 23 || DateOfBirth.Month == 11 && DateOfBirth.Day <= 22)
                sunSign = "Scorpio ♏︎";
            else if (DateOfBirth.Month == 11 && DateOfBirth.Day >= 23 || DateOfBirth.Month == 12 && DateOfBirth.Day <= 21)
                sunSign = "Sagittarius ♐︎";
            else if (DateOfBirth.Month == 12 && DateOfBirth.Day >= 22 || DateOfBirth.Month == 1 && DateOfBirth.Day <= 20)
                sunSign = "Capricorn ♑︎";
            else if (DateOfBirth.Month == 1 && DateOfBirth.Day >= 21 || DateOfBirth.Month == 2 && DateOfBirth.Day <= 19)
                sunSign = "Aquarius ♒︎";
            else
                sunSign = "Pisces ♓︎";
        }

        private void CalcChineseSign()
        {
                switch (DateOfBirth.Year % 12)
                {
                    case 0: chineseSign = "Monkey"; break;
                    case 1: chineseSign = "Rooster"; break;
                    case 2: chineseSign = "Dog"; break;
                    case 3: chineseSign = "Pig"; break;
                    case 4: chineseSign = "Rat"; break;
                    case 5: chineseSign = "Ox"; break;
                    case 6: chineseSign = "Tiger"; break;
                    case 7: chineseSign = "Rabbit"; break;
                    case 8: chineseSign = "Dragon"; break;
                    case 9: chineseSign = "Snake"; break;
                    case 10: chineseSign = "Horse"; break;
                    default: chineseSign = "Goat"; break;
                }
        }

        private void CalcIsBirthDay()
        {
            isBirthday = DateOfBirth.Month == DateTime.Today.Month && DateOfBirth.Day == DateTime.Today.Day;
        }
    }
}
