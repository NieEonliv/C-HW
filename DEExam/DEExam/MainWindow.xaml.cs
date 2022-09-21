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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DEExam
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new MaterialsPage());
        }

        private void AddFormOpen_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new PageRedactorMaterial());
        }

        private void MaterialShow_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new MaterialsPage());
        }

        private void SuplaersShow_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
