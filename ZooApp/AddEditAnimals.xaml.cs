using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for AddEditAnimals.xaml
    /// </summary>
    public partial class AddEditAnimals : Page
    {
        private Animal curAnimal = new Animal();
        byte[] ByteImage;
        public AddEditAnimals(Animal selectedAnimal)
        {
            
            InitializeComponent();
            if(selectedAnimal != null)
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
            }
        }

        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            if(CheckError().Length > 0)
            {
                MessageBox.Show(CheckError().ToString());
                return;
            }
            curAnimal.Name = NameInput.Text;
            curAnimal.birthday = DateInput.SelectedDate;
            curAnimal.image = ByteImage;
            curAnimal.sex = SexInput.Text;
            curAnimal.kind = KindInput.Text;
            curAnimal.breed = BreedInput.Text;
            if(curAnimal.id == 0)
                ZooDBEntities.GetContext().Animal.Add(curAnimal);
            try
            {
                ZooDBEntities.GetContext().SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            NavigationService.GoBack();
        }
        StringBuilder CheckError()
        {
            StringBuilder Errors = new StringBuilder();
            if(NameInput.Text == "")
            {
                Errors.AppendLine("Укажите кличку");
            }
            if (DateInput.SelectedDate == null)
            {
                Errors.AppendLine("Укажите дату рождения");
            }
            if (ByteImage == null)
            {
                Errors.AppendLine("Выберите фото");
            }
            if (SexInput.Text == "")
            {
                Errors.AppendLine("Укажите пол");
            }
            if (KindInput.Text == "")
            {
                Errors.AppendLine("Укажите вид");
            }
            if (BreedInput.Text == "")
            {
                Errors.AppendLine("Укажите породу");
            }
            return Errors;

        }
    }
}
