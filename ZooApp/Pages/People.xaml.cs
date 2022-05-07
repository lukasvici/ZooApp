using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZooApp.Pages
{
    /// <summary>
    /// Interaction logic for People.xaml
    /// </summary>
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

        private void Export_Word(object sender, MouseButtonEventArgs e)
        {

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
