using FontAwesome.WPF;
using Lab04Magur.Models;
using Lab04Magur.ModelViews;
using Lab04Magur.Views;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Lab04Magur
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ImageAwesome _loader;
        private AddPersonView _addPersonView;
        private PeopleListView _peopleListView;
        private FilterWindow _filterWindow;

        public MainWindow()
        {
            InitializeComponent();
            ShowPeopleListView();
        }


        private void ShowPeopleListView()
        {
            if (_peopleListView == null)
            {
                _peopleListView = new PeopleListView(ShowAddPersonView, ShowFilterWindow);
            }

            ShowView(_peopleListView);
        }

        private void ShowFilterWindow()
        {
            if (_filterWindow == null)
            {
                _filterWindow = new FilterWindow(CloseFilterWindow, UpdatePeopleView);
            }

            _filterWindow.ShowDialog();
        }


        private void CloseFilterWindow()
        {
            if (_filterWindow != null)
            {
                _filterWindow.Visibility = Visibility.Hidden;
            }
        }

        private void ShowAddPersonView()
        {
            if (_addPersonView == null)
            {
                _addPersonView = new AddPersonView(ShowLoader, ShowPeopleListView, UpdatePeopleView);
            }

            ShowView(_addPersonView);
        }

        private void UpdatePeopleView()
        {
            ((PeopleListViewModel)_peopleListView.DataContext).UpdatePeopleView();
        }

        private void UpdatePeopleView(IEnumerable<Person> people = null)
        {
            ((PeopleListViewModel)_peopleListView.DataContext).UpdatePeopleView(people);
        }

        private void ShowView(UIElement element)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(element);
        }

        private void ShowLoader(bool isShow)
        {
            LoaderHelper.OnRequestLoader(MainGrid, ref _loader, isShow);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            DBAdapter.SaveData();
            if (_filterWindow != null)
                _filterWindow.Close();
            base.OnClosing(e);
        }
    }
}
