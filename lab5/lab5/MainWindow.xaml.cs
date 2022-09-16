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

namespace lab5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow _instance;
        public static MainWindow GetInstance() => _instance;
        public MainWindow()
        {
            InitializeComponent();
            _instance = this;
            frame.Navigate(new PageMain());           
        }

        private void ShowMainPage_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageMain());
        }

        private void AddCategoryPage_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageAddCategory());
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.PrintVisual(frame, "печать");
        }
    }
}
