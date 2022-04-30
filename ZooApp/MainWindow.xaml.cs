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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ZooDBEntities db = new ZooDBEntities();
            try
            {
                var docs = from d in db.Account where d.login.Equals(LoginInput.Text) select d;
                if (PassInput.Text == docs.First().password)
                {
                    MessageBox.Show("sucsess");
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
