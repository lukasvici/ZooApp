using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Word = Microsoft.Office.Interop.Word;

namespace ZooApp
{
    public partial class Animals : Page
    {
        public Animals()
        {
            InitializeComponent();
            DGAnimals.ItemsSource = ZooDBEntities.GetContext().Animal.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new AddEditAnimals(DGAnimals.SelectedItem as Animal));
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditAnimals(null));
        }

        private void DGAnimals_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                ZooDBEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGAnimals.ItemsSource = ZooDBEntities.GetContext().Animal.ToList();
            }
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            var AnimalsListToRemove = DGAnimals.SelectedItems.Cast<Animal>().ToList();
            if(MessageBox.Show("Вы точно хотите удалить выделенные строчки?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ZooDBEntities.GetContext().Animal.RemoveRange(AnimalsListToRemove);
                    ZooDBEntities.GetContext().SaveChanges();
                    DGAnimals.ItemsSource = ZooDBEntities.GetContext().Animal.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Export_Word(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var animalset = ((DGAnimals.SelectedItem as Animal));
            var WordApp = new Word.Application();
            Word.Document document = WordApp.Documents.Add();
            Word.Paragraph paragraph = document.Paragraphs.Add();
            Word.Shape shape;

            Word.Range userrange = paragraph.Range;
            userrange.Text = "Кличка: " + animalset.Name + "\n";
            userrange.Text += "Дата рождения: " + animalset.birthday + "\n";
            userrange.Text += "Пол: " + animalset.sex + "\n";
            userrange.Text += "Вид: " + animalset.kind + "\n";
            userrange.Text += "Порода: " + animalset.breed + "\n";
            
            WordApp.Visible = true;

        }
        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}