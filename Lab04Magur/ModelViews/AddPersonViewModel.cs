using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Lab04Magur.Annotations;
using Lab04Magur.Exceptions;
using Lab04Magur.Models;

namespace Lab04Magur.ModelViews
{
    internal class AddPersonViewModel : INotifyPropertyChanged
    {
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _email = string.Empty;
        private DateTime _dateOfBirth = DateTime.Today;
        private RelayCommand _proceed;
        private RelayCommand _cancelCommand;
        private readonly Action<bool> _showLoaderAction;
        private readonly Action _closeAction;
        private readonly Action _updatePeopleView;

        internal AddPersonViewModel(Action<bool> showLoader, Action closeAction, Action updatePeopleView)
        {
            _showLoaderAction = showLoader;
            _closeAction = closeAction;
            _updatePeopleView = updatePeopleView;
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddPerson
        {
            get
            {
                return _proceed ?? (_proceed = new RelayCommand(CreatePersonImpl, x =>
                               !String.IsNullOrWhiteSpace(_firstName) &&
                               !String.IsNullOrWhiteSpace(_lastName) &&
                               !String.IsNullOrWhiteSpace(_email) &&
                               _dateOfBirth != DateTime.MinValue));
            }
        }

        public RelayCommand Cancel
        {
            get { return _cancelCommand ?? (_cancelCommand = new RelayCommand(o => _closeAction.Invoke())); }
        }

        private async void CreatePersonImpl(object o)
        {
            Person newPerson = null;
            CurrentPerson.CurrUser = null;
            _showLoaderAction.Invoke(true);
            await Task.Run(() =>
            {
                try
                {
                    newPerson = new Person(_firstName, _lastName, _email, _dateOfBirth);
                    Thread.Sleep(2000);

                    CurrentPerson.CurrUser = newPerson;
                    if (CurrentPerson.CurrUser.IsBirthday)
                        MessageBox.Show("Happy birhday, buddy! Now you're closer to your death than year ago)");

                    MessageBox.Show("Person was added successfully" + Environment.NewLine + CurrentPerson.CurrUser.ToString());
                    DBAdapter.AddPerson(newPerson);
                }

                catch (InvalidNameException e)
                {
                    MessageBox.Show(e.Message);
                }
                catch (InvalidEmailException e)
                {
                    MessageBox.Show(e.Message);
                }
                catch (NegativeAgeException e)
                {
                    MessageBox.Show(e.Message);
                }
                catch (DiedPersonException e)
                {
                    MessageBox.Show(e.Message);
                }
            });

            _showLoaderAction.Invoke(false);

            if (CurrentPerson.CurrUser != null)
            {
                _closeAction.Invoke();
            }
            _updatePeopleView.Invoke();
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
