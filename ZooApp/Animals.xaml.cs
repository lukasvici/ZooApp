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

namespace ZooApp
{
    /// <summary>
    /// Interaction logic for Animals.xaml
    /// </summary>
    public partial class Animals : Page
    {
        public Animals()
        {
            InitializeComponent();
            DGAnimals.ItemsSource = ZooDBEntities.GetContext().Animal.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditAnimals((sender as Image).DataContext as Animal));
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditAnimals(null));
        }

        private void DGAnimals_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                ZooDBEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGAnimals.ItemsSource = ZooDBEntities.GetContext().Animal.ToList();
            }
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            var AnimalsListToRemove = DGAnimals.SelectedItems.Cast<Animal>().ToList();
            if(MessageBox.Show("Вы точно хотите удалить выделенные строчки?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ZooDBEntities.GetContext().Animal.RemoveRange(AnimalsListToRemove);
                    ZooDBEntities.GetContext().SaveChanges();
                    DGAnimals.ItemsSource = ZooDBEntities.GetContext().Animal.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
