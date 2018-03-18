using Lab04Magur.Exceptions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lab04Magur.Models
{
    [Serializable]
    public class Person : INotifyPropertyChanged
    {
        internal static readonly string[] AvailableNames = { "Daenerys", "Lianna", "Catelyn", "Tyrion", "Theon", "Cersei", "John", "Robert", "Rob", "Aria", "Sansa", "Sam", "Ned", "Jaime", "Jorah" };
        internal static readonly string[] AvailableLastNames = { "Stark", "Baelish", "Lannister", "Mormont", "Targaryen", "Snow", "Greyjoy", "Baratheon", "Tarly", "Tyrell", "Bolton" };

        internal static readonly string[] ChineeseZodiacs = { "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Sheep" };
        internal static readonly string[] WesternZodiacs = { "Aquarius", "Capricorn", "Pisces", "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio", "Sagittarius" };

        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _dateOfBirth;

        private bool _isAdult;
        private string _westernZodiac;
        private string _chineeseZodiac;
        private bool _isBirthday;

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (!new UserNameAttribute().IsValid(value))
                    throw new InvalidNameException("First name must contain only letters or -/' symbols");
                _firstName = value;
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (!new UserNameAttribute().IsValid(value))
                    throw new InvalidNameException("Last name must contain only letters or -/' symbols");
                _lastName = value;
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (!new EmailAddressAttribute().IsValid(value))
                    throw new InvalidNameException("Email address has invalid format");
                _email = value;
            }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                UpdateDateOfBirth(value);
            }
        }

        public bool IsAdult { get { return _isAdult; } }
        public string WesternZodiac { get { return _westernZodiac; } }
        public string ChineeseZodiac { get { return _chineeseZodiac; } }
        public bool IsBirthday { get { return _isBirthday; } }


        internal Person(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            _firstName = firstName;

            if (!new UserNameAttribute().IsValid(_firstName))
                throw new InvalidNameException("First name must contain only letters or -/' symbols");

            _lastName = lastName;

            if (!new UserNameAttribute().IsValid(_lastName))
                throw new InvalidNameException("Last name must contain only letters or -/' symbols");

            _email = email;

            if (!new EmailAddressAttribute().IsValid(_email))
                throw new InvalidNameException("Email address has invalid format");

            UpdateDateOfBirth(dateOfBirth);
        }


        internal Person(string firstName, string lastName, string email) : this(firstName, lastName, email, DateTime.Today)
        {
        }

        internal Person(string firstName, string lastName, DateTime dateOfBirth) : this(firstName, lastName,
            string.Empty, dateOfBirth)
        {
        }

        private void UpdateDateOfBirth(DateTime dateOfBirth)
        {
            var age = GetAge(dateOfBirth);
            if (age < 0)
                throw new NegativeAgeException("Age cannot be less than 0");
            if (age > 135)
                throw new DiedPersonException("Person's age is too big to say they are alive");

            _dateOfBirth = dateOfBirth;

            _isAdult = age >= 18;
            NotifyPropertyChanged("IsAdult");
            _westernZodiac = GetWesternZodiac();
            NotifyPropertyChanged("WesternZodiac");
            _chineeseZodiac = GetEasternZodiac();
            NotifyPropertyChanged("ChineeseZodiac");
            _isBirthday = IsBirthdayToday();
            NotifyPropertyChanged("IsBirthday");
        }

        private int GetAge(DateTime dateOfBirth)
        {
            int age = DateTime.Today.Year - dateOfBirth.Year;
            if (DateTime.Today.Month < dateOfBirth.Month)
                return age - 1;
            if (DateTime.Today.Month == dateOfBirth.Month && DateTime.Today.Day < dateOfBirth.Day)
                return age - 1;
            return age;
        }

        private bool IsBirthdayToday()
        {
            return DateTime.Today.Month.Equals(_dateOfBirth.Month) && DateTime.Today.Day.Equals(_dateOfBirth.Day);
        }

        private string GetEasternZodiac()
        {
            return ChineeseZodiacs[_dateOfBirth.Year % 12];
        }

        private string GetWesternZodiac()
        {
            switch (_dateOfBirth.Month)
            {
                case 1:
                    if (_dateOfBirth.Day >= 20)
                        return WesternZodiacs[0];
                    if (_dateOfBirth.Day <= 19)
                        return WesternZodiacs[1];
                    break;
                case 2:
                    if (_dateOfBirth.Day >= 19)
                        return WesternZodiacs[2];
                    if (_dateOfBirth.Day <= 18)
                        return WesternZodiacs[0];
                    break;
                case 3:
                    if (_dateOfBirth.Day >= 21)
                        return WesternZodiacs[3];
                    if (_dateOfBirth.Day <= 20)
                        return WesternZodiacs[2];
                    break;
                case 4:
                    if (_dateOfBirth.Day >= 20)
                        return WesternZodiacs[4];
                    if (_dateOfBirth.Day <= 19)
                        return WesternZodiacs[3];
                    break;
                case 5:
                    if (_dateOfBirth.Day >= 21)
                        return WesternZodiacs[5];
                    if (_dateOfBirth.Day <= 20)
                        return WesternZodiacs[4];
                    break;
                case 6:
                    if (_dateOfBirth.Day >= 21)
                        return WesternZodiacs[6];
                    if (_dateOfBirth.Day <= 20)
                        return WesternZodiacs[5];
                    break;
                case 7:
                    if (_dateOfBirth.Day >= 23)
                        return WesternZodiacs[7];
                    if (_dateOfBirth.Day <= 22)
                        return WesternZodiacs[6];
                    break;
                case 8:
                    if (_dateOfBirth.Day >= 23)
                        return WesternZodiacs[8];
                    if (_dateOfBirth.Day <= 22)
                        return WesternZodiacs[7];
                    break;
                case 9:
                    if (_dateOfBirth.Day >= 23)
                        return WesternZodiacs[9];
                    if (_dateOfBirth.Day <= 22)
                        return WesternZodiacs[8];
                    break;
                case 10:
                    if (_dateOfBirth.Day >= 23)
                        return WesternZodiacs[10];
                    if (_dateOfBirth.Day <= 22)
                        return WesternZodiacs[9];
                    break;
                case 11:
                    if (_dateOfBirth.Day >= 22)
                        return WesternZodiacs[11];
                    if (_dateOfBirth.Day <= 21)
                        return WesternZodiacs[10];
                    break;
                case 12:
                    if (_dateOfBirth.Day >= 22)
                        return WesternZodiacs[1];
                    if (_dateOfBirth.Day <= 21)
                        return WesternZodiacs[11];
                    break;
                default:
                    throw new ArgumentException();
            }
            return "";
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("First name: " + _firstName);
            sb.AppendLine();
            sb.Append("Last name: " + _lastName);
            sb.AppendLine();
            sb.Append("Email: " + _email);
            sb.AppendLine();
            sb.Append("Date of birth: " + _dateOfBirth);
            sb.AppendLine();
            sb.Append("Is adult?: " + _isAdult);
            sb.AppendLine();
            sb.Append("Zodiac sign (western variant): " + _westernZodiac);
            sb.AppendLine();
            sb.Append("Zodiac sign (eastern variant): " + _chineeseZodiac);
            sb.AppendLine();
            sb.Append("Is it birthday today?: " + _isBirthday);
            return sb.ToString();
        }

    }
}
