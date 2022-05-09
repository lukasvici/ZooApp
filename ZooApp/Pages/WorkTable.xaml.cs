using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ZooApp.Pages
{
    public partial class WorkTable : Page
    {
        public WorkTable()
        {
            InitializeComponent();
            DGWorkTable.ItemsSource = ZooDBEntities.GetContext().Worktable.ToList();
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
                    ZooDBEntities.GetContext().Worktable.RemoveRange(WorkTableListToRemove);
                    ZooDBEntities.GetContext().SaveChanges();
                    DGWorkTable.ItemsSource = ZooDBEntities.GetContext().Worktable.ToList();
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
                ZooDBEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGWorkTable.ItemsSource = ZooDBEntities.GetContext().Worktable.ToList();
            }
        }

        private void btnEdit_Click(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddEditWorkTable(DGWorkTable.SelectedItem as Worktable));
        }
    }
}
