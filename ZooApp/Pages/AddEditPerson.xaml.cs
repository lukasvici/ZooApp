using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ZooApp.Pages
{
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
                    curPerson.Account.password = Hash(PasswordInput.Text);
                    curPerson.Account.permission = Convert.ToInt32(PermissionInput.Text);
                    ZooDBEntities.GetContext().Account.Add(curPerson.Account);
                }
                curPerson.Account.password = Hash(PasswordInput.Text);
                ZooDBEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
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
        string Hash(string pass)
        {
            var crypt = new SHA256Managed();
            byte[] Hashpass = crypt.ComputeHash(Encoding.UTF8.GetBytes(pass));
            string result = String.Empty;
            foreach (byte theByte in Hashpass)
            {
                result += theByte.ToString("x2");
            }
            return result;
        }
    }
}
