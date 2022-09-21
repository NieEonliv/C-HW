using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace DEExam
{
    /// <summary>
    /// Логика взаимодействия для PageRedactorMaterial.xaml
    /// </summary>
    public partial class PageRedactorMaterial : Page
    {
        private Material _material;
        public PageRedactorMaterial()
        {
            InitializeComponent();
            List<Supplier> suppliers = demExamEntities.GetContext().Suppliers.ToList();
            var temp = suppliers.Select(x => x.Title).ToList();
            foreach (var item in temp)
                supliers.Items.Add(item);

        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image *jpeg *png |*.jpeg;*.png";

            if (fileDialog.ShowDialog() == false)
                return;
            var c = Application.Current.StartupUri.LocalPath;
            var a = Application.Current.StartupUri;
            var b = Directory.GetCurrentDirectory();
            File.Move(fileDialog.FileName, $"pack://application:,,,/materials/{fileDialog.SafeFileName}");
        }
    }
}
