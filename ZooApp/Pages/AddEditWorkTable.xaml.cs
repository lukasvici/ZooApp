using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ZooApp.Pages
{
    public partial class AddEditWorkTable : Page
    {
        private Worktable curWorkTable = new Worktable();
        public AddEditWorkTable(Worktable selectedWorktable)
        {
            InitializeComponent();
            if (selectedWorktable != null)
                curWorkTable = selectedWorktable;
            DataContext = curWorkTable;
            ComboWorker.ItemsSource = ZooDBEntities.GetContext().Person.ToList();
            ComboAnimal.ItemsSource = ZooDBEntities.GetContext().Animal.ToList();
        }
        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            if(CheckErrors().Length > 0)
            {
                MessageBox.Show(CheckErrors().ToString(), "Alert",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
                
            if (curWorkTable.id == 0)
            {
                ZooDBEntities.GetContext().Worktable.Add(curWorkTable);
            }

            try
            {
                ZooDBEntities.GetContext().SaveChanges();
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Canselbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        StringBuilder CheckErrors()
        {
            StringBuilder Errors = new StringBuilder();
            if(ComboWorker.SelectedIndex == -1)
                Errors.AppendLine("Выберите исполнителя");
            if (ComboAnimal.SelectedIndex == -1)
                Errors.AppendLine("Выберите Животное");
            if (DateInput.SelectedDate == null)
                Errors.AppendLine("Выберите дату");
            if (Convert.ToDateTime(DateInput.SelectedDate).ToString("HH/mm/ss") == "00.00.00")
                Errors.AppendLine("Напишите время");
            return Errors;
        }
    }
}
