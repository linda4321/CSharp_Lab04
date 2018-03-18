using Lab04Magur.Annotations;
using Lab04Magur.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Lab04Magur.ModelViews
{
    public class PeopleListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> _people;

        private RelayCommand _addPersonCommand;
        private RelayCommand _filterCommand;
        private RelayCommand _sortByCommand;
        private RelayCommand _deletePersonCommand;
        private RelayCommand _showAll;
        private readonly Action _showAddPersonAction;
        private readonly Action _showFilterAction;

        public ObservableCollection<Person> People
        {
            get => _people;
            set
            {
                _people = value;
                OnPropertyChanged();
            }
        }

        internal PeopleListViewModel(Action showAddPersonAction, Action showFilterAction)
        {
            _people = new ObservableCollection<Person>(DBAdapter.People);
            _showAddPersonAction = showAddPersonAction;
            _showFilterAction = showFilterAction;
        }

        internal void UpdatePeopleView(IEnumerable<Person> people = null)
        {
            if (people == null)
                people = DBAdapter.People;
            People = new ObservableCollection<Person>(people);
        }

        public RelayCommand DeleteItem
        {
            get { return _deletePersonCommand ?? (_deletePersonCommand = new RelayCommand(DeletePerson)); }
        }

        public RelayCommand SortBy
        {
            get { return _sortByCommand ?? (_sortByCommand = new RelayCommand(Sort)); }
        }

        public RelayCommand AddPersonCommand
        {
            get { return _addPersonCommand ?? (_addPersonCommand = new RelayCommand(o => _showAddPersonAction.Invoke())); }
        }

        public RelayCommand ShowFilter
        {
            get { return _filterCommand ?? (_filterCommand = new RelayCommand(o => _showFilterAction.Invoke())); }
        }

        public RelayCommand ShowAll
        {
            get { return _showAll ?? (_showAll = new RelayCommand(ShowAllPeople)); }
        }

        public void DeletePerson(object person)
        {
            DBAdapter.DeletePerson((Person)person);
            People.Remove((Person)person);
        }

        public void Sort(object property)
        {
            People = new ObservableCollection<Person>(_people.OrderBy(person => person.GetType().GetProperty(property.ToString()).GetValue(person)));
        }

        public void ShowAllPeople(object person)
        {
            People = new ObservableCollection<Person>(DBAdapter.People);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
