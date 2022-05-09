using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ZooApp.Pages
{
    public partial class People : Page
    {
        public People()
        {
            InitializeComponent();
            DGPeoples.ItemsSource = ZooDBEntities.GetContext().Person.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnEdit_Click(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddEditPerson(DGPeoples.SelectedItem as Person));
        }

        private void AddBtn_Click(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddEditPerson(null));
        }

        private void delBtn_Click(object sender, MouseButtonEventArgs e)
        {
            var PeopleListToRemove = DGPeoples.SelectedItems.Cast<Person>().ToList();
            if (MessageBox.Show("Вы точно хотите удалить выделенные строчки?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    if(PeopleListToRemove[0].Account != null)
                        ZooDBEntities.GetContext().Account.Remove(PeopleListToRemove[0].Account);
                    ZooDBEntities.GetContext().Person.RemoveRange(PeopleListToRemove);
                    ZooDBEntities.GetContext().SaveChanges();
                    DGPeoples.ItemsSource = ZooDBEntities.GetContext().Person.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ZooDBEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGPeoples.ItemsSource = ZooDBEntities.GetContext().Person.ToList();
            }
        }
    }
}
