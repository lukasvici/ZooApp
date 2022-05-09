using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ZooApp.Pages
{
    public partial class MainPage : Page
    {
        string[] PermissionPages = { "Животные", "Расписание", "Сотрудники" };
        public MainPage(int permission = 1)
        {
            InitializeComponent();
            GenerateButtons(permission);
        }
        void GenerateButtons(int permission)
        {
            
            for (int i = 0; i < permission; i++)
            {
                Button btn = new Button();
                btn.Height = 30;
                btn.Width = 120;
                btn.Margin = new Thickness(20, 0, 0, i * 80);
                btn.Content = PermissionPages[i];
                btn.Click += (object sender, RoutedEventArgs e) =>
                {
                    var senderBtn = sender as Button;
                    int indexitem = Array.IndexOf(PermissionPages, senderBtn.Content.ToString());
                    switch (indexitem)
                    {
                        case 0:
                            NavigationService.Navigate(new Animals());
                            break;
                        case 1:
                            NavigationService.Navigate(new WorkTable());
                            break;
                        case 2:
                            NavigationService.Navigate(new People());
                            break;
                    }
                };
                ButtonsGrid.Children.Add(btn);
            }

        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
