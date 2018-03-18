using Lab04Magur.Models;
using Lab04Magur.ModelViews;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab04Magur.Views
{
    /// <summary>
    /// Interaction logic for FilterWindow.xaml
    /// </summary>
    public partial class FilterWindow : Window
    {
        public FilterWindow(Action close, Action<IEnumerable<Person>> updatePeopleView)
        {
            InitializeComponent();
            DataContext = new FilterViewModel(close, updatePeopleView);
        }
    }
}
