using Lab04Magur.ModelViews;
using System;
using System.Windows.Controls;

namespace Lab04Magur.Views
{
    /// <summary>
    /// Interaction logic for AddPersonView.xaml
    /// </summary>
    public partial class AddPersonView : UserControl
    {
        public AddPersonView(Action<bool> showLoaderAction, Action close, Action updatePeopleView)
        {
            InitializeComponent();
            DataContext = new AddPersonViewModel(showLoaderAction, close, updatePeopleView);
        }
    }
}
