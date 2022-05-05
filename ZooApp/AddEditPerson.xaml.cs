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
    /// Interaction logic for AddEditPerson.xaml
    /// </summary>
    public partial class AddEditPerson : Page
    {
        private Person curPerson = new Person();
        public AddEditPerson(Person selectedPerson)
        {
            InitializeComponent();
            if(selectedPerson != null)
            {
                curPerson = selectedPerson;
            }
            DataContext = curPerson;
        }

        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            if (curPerson.id == 0)
            {   
                
                ZooDBEntities.GetContext().Person.Add(curPerson);
            }
            try
            {
                if ((curPerson.Account == null) & LoginInput.Text != "" & PasswordInput.Text != "" & PermissionInput.Text != "")
                {
                    curPerson.Account = new Account();
                    curPerson.Account.login = LoginInput.Text;
                    curPerson.Account.password = PasswordInput.Text;
                    curPerson.Account.permission = Convert.ToInt32(PermissionInput.Text);
                    ZooDBEntities.GetContext().Account.Add(curPerson.Account);
                }
                ZooDBEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            NavigationService.GoBack();
        }

        private void Canselbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }


    }
}
