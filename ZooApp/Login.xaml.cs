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
            ZooDBEntities1 db = new ZooDBEntities1();
            try
            {
                var docs = from d in db.Account where d.login.Equals(LoginInput.Text) select d;
                if (PassInput.Text == docs.First().password)
                {
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
    }
}
