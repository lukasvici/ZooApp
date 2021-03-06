using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace ZooApp.Pages
{
    public partial class AddEditAnimals : Page
    {
        private Animal curAnimal = new Animal();
        byte[] ByteImage;
        public AddEditAnimals(Animal selectedAnimal)
        {
            InitializeComponent();
            if (selectedAnimal != null)
            {
                curAnimal = selectedAnimal;
                ByteImage = selectedAnimal.image;
            }
            DataContext = curAnimal;
        }
        private void Canselbtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Image files (*.emf;*.wmf;*.jpg;*.jpeg;*.jfif;*.jpe;*.png;*.bmp;*.dib;*.rle;*.emz;*.wmz;*.svg;)|*.emf;*.wmf;*.jpg;*.jpeg;*.jfif;*.jpe;*.png;*.bmp;*.dib;*.rle;*.emz;*.wmz;*.svg;|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                ByteImage = File.ReadAllBytes(fileUri.LocalPath);
                PhotoPreview.Source = LoadImage(ByteImage);
            }
        }

        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckErrors().Length > 0)
            {
                MessageBox.Show(CheckErrors().ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            curAnimal.image = ByteImage;
            if (curAnimal.id == 0)
                ZooDBEntities.GetContext().Animal.Add(curAnimal);
            try
            {
                ZooDBEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            NavigationService.GoBack();
        }
        StringBuilder CheckErrors()
        {
            StringBuilder Errors = new StringBuilder();
            if (ByteImage == null)
            {
                Errors.AppendLine("Выберите фото");
            }
            if (string.IsNullOrEmpty(SexInput.Text.Replace(" ", "")))
            {
                Errors.AppendLine("Укажите пол");
            }
            if (string.IsNullOrEmpty(KindInput.Text.Replace(" ", "")))
            {
                Errors.AppendLine("Укажите вид");
            }
            return Errors;

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
