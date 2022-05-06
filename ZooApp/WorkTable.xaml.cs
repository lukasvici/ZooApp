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
    /// Interaction logic for WorkTable.xaml
    /// </summary>
    public partial class WorkTable : Page
    {
        public WorkTable()
        {
            InitializeComponent();
            DGWorkTable.ItemsSource = ZooDBEntities1.GetContext().Worktable.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void delBtn_Click(object sender, MouseButtonEventArgs e)
        {
            var WorkTableListToRemove = DGWorkTable.SelectedItems.Cast<Worktable>().ToList();
            if (MessageBox.Show("Вы точно хотите удалить выделенные строчки?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ZooDBEntities1.GetContext().Worktable.RemoveRange(WorkTableListToRemove);
                    ZooDBEntities1.GetContext().SaveChanges();
                    DGWorkTable.ItemsSource = ZooDBEntities1.GetContext().Worktable.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void AddBtn_Click(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddEditWorkTable(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ZooDBEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGWorkTable.ItemsSource = ZooDBEntities1.GetContext().Worktable.ToList();
            }
        }

        private void btnEdit_Click(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddEditWorkTable(DGWorkTable.SelectedItem as Worktable));
        }
    }
}
