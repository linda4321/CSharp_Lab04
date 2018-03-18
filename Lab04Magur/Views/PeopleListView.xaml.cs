using Lab04Magur.ModelViews;
using System;
using System.Windows;
using System.Windows.Controls;


namespace Lab04Magur.Views
{
    /// <summary>
    /// Interaction logic for UserListWindow.xaml
    /// </summary>
    public partial class PeopleListView : UserControl
    {
        public PeopleListView(Action showAddPerson, Action showFilter)
        {
            InitializeComponent();
            DataContext = new PeopleListViewModel(showAddPerson, showFilter);
        }

        private void DisplayContextMenu(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsEnabled = true;
            ((Button)sender).ContextMenu.PlacementTarget = ((Button)sender);
            ((Button)sender).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            ((Button)sender).ContextMenu.IsOpen = true;
        }
    }
}
