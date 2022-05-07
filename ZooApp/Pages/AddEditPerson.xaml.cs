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
    /// Interaction logic for AddEditPerson.xaml
    /// </summary>
    public partial class AddEditPerson : Page
    {
        private Person curPerson = new Person();
        public AddEditPerson(Person selectedPerson)
        {
            InitializeComponent();
            if (selectedPerson != null)
            {
                curPerson = selectedPerson;
            }
            DataContext = curPerson;
        }
        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            if(CheckErrors().Length > 0)
            {
                MessageBox.Show(CheckErrors().ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
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
        StringBuilder CheckErrors()
        {
            StringBuilder Errors = new StringBuilder();
            if (string.IsNullOrEmpty(NameInput.Text.Replace(" ", "")))
                Errors.AppendLine("Укажите имя");
            if (string.IsNullOrEmpty(SurnameInput.Text.Replace(" ", "")))
                Errors.AppendLine("Укажите фамилию");
            if (string.IsNullOrEmpty(AgeInput.Text.Replace(" ", "")) || Convert.ToInt32(AgeInput.Text) < 18)
                Errors.AppendLine("Укажите возраст");
            if (string.IsNullOrEmpty(PositionInput.Text.Replace(" ", "")))
                Errors.AppendLine("Укажите должность");
            return Errors;
        }
    }
}
