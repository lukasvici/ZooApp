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
    /// Interaction logic for AddEditWorkTable.xaml
    /// </summary>
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
    }
}
