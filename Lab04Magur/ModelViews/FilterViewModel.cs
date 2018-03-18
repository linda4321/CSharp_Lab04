using Lab04Magur.Annotations;
using Lab04Magur.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Lab04Magur.ModelViews
{
    class FilterViewModel : INotifyPropertyChanged
    {
        private RelayCommand _closeFilterCommand;
        private RelayCommand _closeAndApplyFilterCommand;
        private readonly Action _closeAction;
        private readonly Action<IEnumerable<Person>> _updatePeopleView;

        private string _firstNameFilter;
        private string _lastNameFilter;
        private string _emailFilter;
        private DateTime _dateOfBirthFilter;
        private string _westernZodiacFilter;
        private string _chineeseZodiacFilter;
        private bool _isAdultFilter;
        private bool _isBirthdayFilter;
        private bool _isNotAdultFilter;
        private bool _isNotBirthdayFilter;

        public string[] WesternZodiacs { get { return Person.WesternZodiacs; } }
        public string[] ChineeseZodiacs { get { return Person.ChineeseZodiacs; } }

        public string FirstNameFilter
        {
            set
            {
                _firstNameFilter = value;
                OnPropertyChanged();
            }
        }

        public string LastNameFilter
        {
            set
            {
                _lastNameFilter = value;
                OnPropertyChanged();
            }
        }

        public string EmailFilter
        {
            set
            {
                _emailFilter = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfBirthFilter
        {
            set
            {
                _dateOfBirthFilter = value;
                OnPropertyChanged();
            }
        }

        public string WesternZodiacFilter
        {
            set
            {
                _westernZodiacFilter = value;
                OnPropertyChanged();
            }
        }

        public string ChineeseZodiacFilter
        {
            set
            {
                _chineeseZodiacFilter = value;
                OnPropertyChanged();
            }
        }

        public bool IsAdultFilter
        {
            set
            {
                _isAdultFilter = value;
                OnPropertyChanged();
            }
        }

        public bool IsNotAdultFilter
        {
            set
            {
                _isNotAdultFilter = value;
                OnPropertyChanged();
            }
        }

        public bool IsBirthdayFilter
        {
            set
            {
                _isBirthdayFilter = value;
                OnPropertyChanged();
            }
        }

        public bool IsNotBirthdayFilter
        {
            set
            {
                _isNotBirthdayFilter = value;
                OnPropertyChanged();
            }
        }

        internal FilterViewModel(Action closeAction, Action<IEnumerable<Person>> updatePeopleView)
        {
            _closeAction = closeAction;
            _updatePeopleView = updatePeopleView;
        }

        public RelayCommand FilterApplyAndClose
        {
            get { return _closeAndApplyFilterCommand ?? (_closeAndApplyFilterCommand = new RelayCommand(AmplyFilters)); }
        }

        public RelayCommand FilterClose
        {
            get { return _closeFilterCommand ?? (_closeFilterCommand = new RelayCommand(FlushAndClose)); }
        }


        public void FlushAndClose(object o)
        {
            Flush();
            _closeAction.Invoke();
        }

        public void AmplyFilters(object o)
        {
            IEnumerable<Person> filteredPeople = DBAdapter.People;

            if (_firstNameFilter != null)
                filteredPeople = filteredPeople.Where(person => person.FirstName == _firstNameFilter);
            if (_lastNameFilter != null)
                filteredPeople = filteredPeople.Where(person => person.LastName == _lastNameFilter);
            if (_emailFilter != null)
                filteredPeople = filteredPeople.Where(person => person.Email == _emailFilter);
            if (_dateOfBirthFilter != DateTime.MinValue)
                filteredPeople = filteredPeople.Where(person => person.DateOfBirth.Equals(_dateOfBirthFilter));
            if (_westernZodiacFilter != null)
                filteredPeople = filteredPeople.Where(person => person.WesternZodiac == _westernZodiacFilter);
            if (_chineeseZodiacFilter != null)
                filteredPeople = filteredPeople.Where(person => person.ChineeseZodiac == _chineeseZodiacFilter);
            if (_isAdultFilter != _isNotAdultFilter)
                filteredPeople = filteredPeople.Where(person => person.IsAdult == _isAdultFilter);
            if (_isBirthdayFilter != _isNotBirthdayFilter)
                filteredPeople = filteredPeople.Where(person => person.IsBirthday == _isBirthdayFilter);

            _closeAction.Invoke();
            _updatePeopleView(filteredPeople);
            Flush();
        }

        private void Flush()
        {
            FirstNameFilter = null;
            LastNameFilter = null;
            EmailFilter = null;
            DateOfBirthFilter = DateTime.MinValue;
            WesternZodiacFilter = null;
            ChineeseZodiacFilter = null;
            IsAdultFilter = false;
            IsBirthdayFilter = false;
            IsNotAdultFilter = false;
            IsNotBirthdayFilter = false;
        }

        #region Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
