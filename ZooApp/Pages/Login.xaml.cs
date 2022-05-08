using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            ZooDBEntities db = new ZooDBEntities();
            try
            {
                var docs = from d in db.Account where d.login == LoginInput.Text select d;
                if (docs.First().password == Hash(PassInput.Text))
                {
                    if (docs.First().permission < 1 || docs.First().permission > 3)
                    {
                        MessageBox.Show("Не правильные права в бд!");
                        return;
                    }
                    NavigationService.Navigate(new MainPage(docs.First().permission));
                    PassInput.Text = "";
                    LoginInput.Text = "";
                }
                else
                {
                    messegeLoginunvalid();
                }

            }
            catch
            {
                messegeLoginunvalid();
            }
        }
        void messegeLoginunvalid()
        {
            MessageBox.Show("Неправильный логин или пароль");
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
